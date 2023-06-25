using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        
        [HttpPost]
        public IActionResult Post(People person)
        {
            if (!ModelState.IsValid) { return View("Add", person); }

            var newPerson = person;
            dbContext.People.Add(newPerson);
            dbContext.SaveChanges();

            return Redirect("/People");
        }
    }
}
