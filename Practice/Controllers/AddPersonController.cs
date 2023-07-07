using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.Services;

namespace Practice.Controllers
{
    public class AddPersonController : Controller
    {
        private IDBService dbService;

        public AddPersonController(IDBService dbService)
        {
            this.dbService = dbService;
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
            if (!ModelState.IsValid) { return View("Add", person); }

            DateTime dateTime = DateTime.Now;
            int age = (dateTime.Year - person.Birthday.Year - 1) +
                      (((dateTime.Month > person.Birthday.Month) ||
                      ((dateTime.Month == person.Birthday.Month) && 
                      (dateTime.Day >= person.Birthday.Day))) ? 1 : 0);

            person.Age = age;
            var newPerson = person;
            dbService.addPersonToDB(newPerson);
            dbService.saveChengesInDB();

            return Redirect("/People");
        }
    }
}
