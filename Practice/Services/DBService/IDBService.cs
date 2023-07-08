using Practice.Models;

namespace Practice.Services.DBService
{
    public interface IDBService
    {
        public void addPersonToDB(People person);

        public void addPairToDB(Pair pair);

        public void saveChengesInDB();

        public void removePersonFromDB(People person);

        public void removePairFromDB(Pair pair);

        public People? searchPersonByID(int id);

        public Pair? searchPairByID(int id);

        public List<People> getPeopleToList();

        public List<Pair> getPairWithIncludesToList();

        public List<People> getPeopleWithIncludesToList();
    }
}
