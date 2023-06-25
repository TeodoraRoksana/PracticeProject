using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;

namespace Practice.Controllers
{
    public class EditController : Controller
    {
        private readonly PracticeContext dbContext;

        public EditController(PracticeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /*public IActionResult Edit()
        {
            return View();
        }*/
        [Route("/Edit/Edit/{id}")]
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var person = dbContext.People.Where(x => x.PersonId == id).FirstOrDefault();
            return View(person);
        }


       /* [HttpPost]
        public IActionResult Post(People person)
        {
            if (!ModelState.IsValid) { return View("Add", person); }

            var newPerson = person;
            dbContext.People.Add(newPerson);
            dbContext.SaveChanges();

            return Redirect("/People");
        }*/

        [HttpPut]
        public IActionResult Put(int id, People person)
        {
            if (!ModelState.IsValid) { return View("Edit", person); }

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

            return Redirect("/People");
        }
    }
}
