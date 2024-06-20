using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;

namespace TalentDevelopers.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly TalentDevelopersContext _context;

        public StoreRepository(TalentDevelopersContext context)
        {
            _context = context;
        }
        public bool CreateStore(Store store)
        {
            _context.Add(store);
            return Save();
        }

        public bool StoreExists(int id)
        {
            return _context.Stores.Any(x => x.Id == id);
        }

        public bool DeleteStore(Store store)
        {
            _context.Remove(store);
            return Save();
        }

        public Store GetStore(int id)
        {
            return _context.Stores.Where(x => x.Id == id).SingleOrDefault();
        }

        public ICollection<Store> GetStores()
        {
            return _context.Stores.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateStore(Store store)
        {
            _context.Update(store);
            return Save();
        }
    }
}
