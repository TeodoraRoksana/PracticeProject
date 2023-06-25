using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /*[HttpGet(Name = "Index")]*/
        public IActionResult Index()
        {
            /*return dbContext.People != null ?
                          View("Index", await dbContext.People.Include(x => x.PairFirstPeople).Include(x => x.PairSecondPeople).ToListAsync()) :
                          Problem("Entity set 'PersonContext.Person'  is null.");*/
            return View(dbContext.People.Include(x => x.PairFirstPeople).Include(x => x.PairSecondPeople).ToList());
        }
        /*[HttpGet("Add")]*/
        /*public IActionResult Add()
        {
            return View();
        }*/

        /*[HttpPost]
        public async Task<IActionResult> Add(People person)
        {
            if (person == null) { return dbContext.People.ToList(); }

            var newPerson = person;
            dbContext.People.Add(newPerson);
            dbContext.SaveChanges();

            return dbContext.People != null ?
                          View("Index", await dbContext.People.Include(x => x.PairFirstPeople).Include(x => x.PairSecondPeople).ToListAsync()) :
                          Problem("Entity set 'PersonContext.Person'  is null.");
        }*/



        /*[HttpGet]
        public IEnumerable<People> Get()
        {
            //var res = dbContext.People.Include(x => x.PairFirstPeople).Include(y => y.PairFirstPeople).ToList(); //code in the future
            
            return dbContext.People.ToList();
        }*/

       /* [HttpGet("{id}")]
        public IEnumerable<People> Get(int id)
        {
            return dbContext.People.Where(x => x.PersonId == id).ToList();
        }*/

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

            var oldperson = dbContext.People.Where(p => p.PersonId == id).FirstOrDefault();

            if (oldperson != null)
            {
                oldperson.FirstName = person.FirstName;
                oldperson.LastName = person.LastName;
                oldperson.Email = person.Email;
                oldperson.Password = person.Password;
                oldperson.Sex = person.Sex;
                oldperson.Age = person.Age;
                oldperson.Birthday = person.Birthday;
                oldperson.AboutMe = person.AboutMe;
                oldperson.Likes = person.Likes;
                oldperson.Dislikes = person.Dislikes;
                oldperson.PairFirstPeople = person.PairFirstPeople;
                oldperson.PairSecondPeople = person.PairSecondPeople;

                dbContext.SaveChanges();
            }

            return dbContext.People.ToList();
        }

        /*[HttpDelete("{id}")]
        public IEnumerable<People> Delete(int id)
        {
            var person = dbContext.People.Where(p => p.PersonId == id).FirstOrDefault();

            if (person != null)
            {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
            }

            return dbContext.People.ToList();
        }*/
        [Route("/People/{id}")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = dbContext.People.Where(p => p.PersonId == id).FirstOrDefault();

            if (person != null)
            {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
            }

            return View("Index", dbContext.People.Include(x => x.PairFirstPeople).Include(x => x.PairSecondPeople).ToList());
        }
    }
}
