using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreateStore(Store store)
        {
            await _context.AddAsync(store);
            return await Save();
        }

        public bool StoreExists(int id)
        {
            return _context.Stores.Any(x => x.Id == id);
        }

        public async Task<bool> DeleteStore(Store store)
        {
            _context.Remove(store);
            return await Save();
        }

        public async Task<Store?> GetStore(int id)
        {
            if(!StoreExists(id))
            {
                return null;
            }
            return await _context.Stores.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ICollection<Store>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateStore(Store store)
        {
            _context.Update(store);
            return await Save();
        }
    }
}
