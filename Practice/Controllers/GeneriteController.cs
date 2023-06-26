using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;

namespace Practice.Controllers
{
    public class GeneriteController : Controller
    {
        private readonly PracticeContext dbContext;

        public GeneriteController(PracticeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Generite()
        {
            return View("Index");
        }

        
        [HttpPost]
        public IActionResult Post()
        {
            var listPeople = dbContext.People.ToList();

            Random rnd = new Random();
            while (listPeople.Count > 1) 
            {
                int firstPerson = rnd.Next(0, listPeople.Count);
                var FirstP = listPeople[firstPerson];
                listPeople.RemoveAt(firstPerson);

                int secondPerson = rnd.Next(0, listPeople.Count);
                var SecondP = listPeople[secondPerson];
                listPeople.RemoveAt(secondPerson);

                var newPair = new Pair
                {
                    PairsId = 0,
                    FirstPersonId = FirstP.PersonId,
                    SecondPersonId = SecondP.PersonId,
                    Data = DateTime.Now,
                    FirstPersonComment = "Nothing yet",
                    SecondPersonComment = "Nothing yet",
                    FirstPerson = null, 
                    SecondPerson = null
                };

                dbContext.Pairs.Add(newPair);
            }

            dbContext.SaveChanges();

            return Redirect("/Pair"); ;
        }

    }
}
