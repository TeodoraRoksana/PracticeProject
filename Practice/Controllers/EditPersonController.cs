using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.Services.DBService;

namespace Practice.Controllers
{
    public class EditPersonController : Controller
    {
        private readonly IDBService dbService;

        public EditPersonController(IDBService dbService)
        {
            this.dbService = dbService;
        }

        /*public IActionResult Edit()
        {
            return View();
        }*/
        [Route("/Edit/Edit/{id}")]
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var person = dbService.searchPersonByID(id);
            return View(person);
        }

        [Route("/Edit/Put/{id}")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, People person)
        {
            if (!ModelState.IsValid) { return View("Edit", person); }

            var oldperson = dbService.searchPersonByID(id);

            if (oldperson != null)
            {
                oldperson.FirstName = person.FirstName;
                oldperson.LastName = person.LastName;
                oldperson.Email = person.Email;
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

            return Redirect("/People");
        }


    }
}
