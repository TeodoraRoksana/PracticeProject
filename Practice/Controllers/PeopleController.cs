using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.Services.DBService;

namespace Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly IDBService dbService;

        public PeopleController(IDBService dbService)
        {
            this.dbService = dbService;
        }

       
        public IActionResult Index()
        {
            return View(dbService.getPeopleWithIncludesToList());
        }    

        /*[HttpPost]
        public IEnumerable<People> Post(People person)
        {
            if (person == null) { return dbService.getPeopleToList(); }

            var newPerson = person;
            dbService.addPersonToDB(newPerson);
            dbService.saveChengesInDB();

            return dbService.getPeopleToList();
        }

       [HttpPut("{id}")]
        public IEnumerable<People> Put(int id, People person)
        {
            if (person == null) { return dbService.getPeopleToList(); }

            var oldperson = dbService.searchPersonByID(id);

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

                dbService.saveChengesInDB();
            }

            return dbService.getPeopleToList();
        }*/

        [Route("/People/{id}")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var person = dbService.searchPersonByID(id);

            if (person != null)
            {
                var listPairs = dbService.getPairWithIncludesToList().Where(p => p.FirstPersonId == id || p.SecondPersonId == id).ToList();

                if(listPairs.Count > 0)
                {
                    foreach (var pair in listPairs)
                    {
                        dbService.removePairFromDB(pair);
                    }
                }

                dbService.removePersonFromDB(person);
                dbService.saveChengesInDB();
            }

            return View("Index", dbService.getPeopleWithIncludesToList());
        }
    }
}
