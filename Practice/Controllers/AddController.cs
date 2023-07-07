using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice.Models;

namespace Practice.Controllers
{
    public class AddController : Controller
    {
        private readonly PracticeContext dbContext;

        public AddController(PracticeContext dbContext)
        {
            this.dbContext = dbContext;
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
            dbContext.People.Add(newPerson);
            dbContext.SaveChanges();

            return Redirect("/People");
        }
    }
}
