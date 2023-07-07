using Microsoft.EntityFrameworkCore;
using Practice.Models;
using System;

namespace Practice.Services
{
    public class DBService : IDBService
    {
        private PracticeContext context;

        public DBService(PracticeContext context) { this.context = context; }

        public void addPersonToDB(People person) { context.People.Add(person); }

        public void addPairToDB(Pair pair) { context.Pairs.Add(pair); }

        public void saveChengesInDB() { context.SaveChanges(); }

        public void removePersonFromDB(People person) { context.People.Remove(person); }

        public void removePairFromDB(Pair pair) { context.Pairs.Remove(pair); }

        public People? searchPersonByID(int id)
        {
            return context.People.Where(p => p.PersonId == id).FirstOrDefault();
        }

        public Pair? searchPairByID(int id)
        {
            return context.Pairs.Where(p => p.PairsId == id).FirstOrDefault();
        }

        public List<People> getPeopleToList() { return context.People.ToList(); }

        public List<Pair> getPairWithIncludesToList()
        {
            return context.Pairs.Include(x => x.FirstPerson).Include(x => x.SecondPerson).ToList();
        }

        public List<People> getPeopleWithIncludesToList()
        {
            return context.People.Include(x => x.PairFirstPeople).Include(x => x.PairSecondPeople).ToList();
        }
    }
}
