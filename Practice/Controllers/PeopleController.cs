using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;

namespace Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : Controller
    {
        private readonly PracticeContext dbContext;

        public PeopleController(PracticeContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        public IEnumerable<People> Get()
        {
            using (var context = new PracticeContext())
            {
                return context.People.ToList();
            }
        }

        

        [HttpPost]
        public IEnumerable<People> Post(People person)
        {
            if (person == null) { return dbContext.People.ToList(); }

            var newPerson = person;
            dbContext.People.Add(newPerson);
            dbContext.SaveChanges();

            return dbContext.People.ToList();
        }

        [HttpPut("{id}")]
        public IEnumerable<People> Put(int id, People person)
        {
            if (person == null) { return dbContext.People.ToList(); }

            var oldperson = dbContext.People.Where(p => p.PeopleId == id).FirstOrDefault();
            
            oldperson.Name = person.Name;
            oldperson.Birthday = person.Birthday;
            oldperson.Description = person.Description;
            oldperson.Gender = person.Gender;

            dbContext.SaveChanges();

            return dbContext.People.ToList();
        }

        [HttpDelete("{id}")]
        public IEnumerable<People> Delete(int id)
        {
            var person = dbContext.People.Where(p => p.PeopleId == id).FirstOrDefault();
            
            if (person != null)
            {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
            }

            return dbContext.People.ToList();
        }
    }
}
