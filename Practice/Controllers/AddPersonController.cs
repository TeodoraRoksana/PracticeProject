using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.Services.DBService;
using Practice.Services.HashService;

namespace Practice.Controllers
{
    public class AddPersonController : Controller
    {
        private IDBService dbService;
        private IHashService hashService;

        public AddPersonController(IDBService dbService, IHashService hashService)
        {
            this.dbService = dbService;
            this.hashService = hashService;
        }

        public IActionResult Add()
        {
            string[] sex = { "Female", "Male" };
            SelectList selectLists = new SelectList(sex, sex[0]);
            ViewBag.SelectList = selectLists;

            return View("Add");
        }

        
        [HttpPost]
        public IActionResult Post(People person)
        {
            if(dbService.getPeopleToList().Any(p => p.Email == person.Email))
                ModelState.AddModelError("email", "Email is already use!");
            
            if (!ModelState.IsValid) { return View("Add", person); }

            DateTime dateTime = DateTime.Now;
            int age = (dateTime.Year - person.Birthday.Year - 1) +
                      (((dateTime.Month > person.Birthday.Month) ||
                      ((dateTime.Month == person.Birthday.Month) && 
                      (dateTime.Day >= person.Birthday.Day))) ? 1 : 0);

            person.Age = age;

            person.Password = hashService.HashPassword(person.Password);

            var newPerson = person;
            dbService.addPersonToDB(newPerson);
            dbService.saveChengesInDB();

            return Redirect("/LogInPerson/LogIn");
        }

        
    }
}
