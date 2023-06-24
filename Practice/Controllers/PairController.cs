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

        /*[HttpGet(Name = "Index")]
        public async Task<IActionResult> Index()
        {
            return dbContext.People != null ?
                          View("Index", await dbContext.People.ToListAsync()) :
                          Problem("Entity set 'PersonContext.Person'  is null.");
        }*/

        [HttpGet]
        public IEnumerable<Pair> Get()
        {
            //var res =dbContext.Pairs.Include(v => v.FirstPerson).ToList();

            return dbContext.Pairs.ToList();
        }

        [HttpGet("{id}")]
        public IEnumerable<Pair> Get(int id)
        {
            

            return dbContext.Pairs.Where(x => x.PairsId == id).ToList();
        }

        [HttpPost]
        public IEnumerable<Pair> Post(PairDTO pair)
        {
            if (pair == null) { return dbContext.Pairs.ToList(); }

            PairMapper mapper = new PairMapper();
            
            Pair newPair = mapper.Unmap(pair);
            if (newPair != null)
            {
                dbContext.Pairs.Add(newPair);
                dbContext.SaveChanges();
            }
            
            return dbContext.Pairs.ToList();
        }

       [HttpPut("{id}")]
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
        }

        [HttpDelete("{id}")]
        public IEnumerable<Pair> Delete(int id)
        {
            var pair = dbContext.Pairs.Where(p => p.PairsId == id).FirstOrDefault();

            if (pair != null)
            {
                dbContext.Pairs.Remove(pair);
                dbContext.SaveChanges();
            }

            return dbContext.Pairs.ToList();
        }
    }
}
