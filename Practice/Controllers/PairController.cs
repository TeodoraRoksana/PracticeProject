using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.Models.Mapper;

namespace Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PairController : Controller
    {
        private readonly PracticeContext dbContext;

        public PairController(PracticeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.Pairs.Include(x => x.FirstPerson).Include(x => x.SecondPerson).ToList());
        }


       

        [Route("/Pair/{id}")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pair = dbContext.Pairs.Where(p => p.PairsId == id).FirstOrDefault();

            if (pair != null)
            {
                dbContext.Pairs.Remove(pair);
                dbContext.SaveChanges();
            }

            return View("Index", dbContext.Pairs.Include(x => x.FirstPerson).Include(x => x.SecondPerson).ToList());
        }

        /*[HttpPut("{id}")]
        public IEnumerable<Pair> Put(int id, PairDTO pair)
        {
            if (pair == null) { return dbContext.Pairs.ToList(); }

            var oldpair = dbContext.Pairs.Where(p => p.PairsId == id).FirstOrDefault();

            PairMapper mapper = new PairMapper();
            var newPair = mapper.Unmap(pair);

            if (oldpair != null && newPair != null)
            {
                oldpair.FirstPersonId = newPair.FirstPersonId;
                oldpair.SecondPersonId = newPair.SecondPersonId;
                oldpair.Data = newPair.Data;
                oldpair.FirstPersonComment = newPair.FirstPersonComment;
                oldpair.SecondPersonComment = newPair.SecondPersonComment;
                oldpair.FirstPerson = newPair.FirstPerson;
                oldpair.SecondPerson = newPair.SecondPerson;

                dbContext.SaveChanges();
            }

            return dbContext.Pairs.ToList();
        }*/

        /*[HttpPost]
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
        }*/
    }
}
