using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TalentDevelopers.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly TalentDevelopersContext _context;

        public SalesRepository(TalentDevelopersContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSales(Sales sales)
        {
            await _context.AddAsync(sales);
            return await Save();
        }

        public bool SalesExists(int id)
        {
            return _context.SalesTable.Any(x => x.Id == id);
        }

        public async Task<bool> DeleteSales(Sales sales)
        {
            _context.Remove(sales);
            return await Save();
        }

        public async Task<Sales?> GetSale(int id)
        {
            if(!SalesExists(id))
            {
               return null;
            }

            return await _context.SalesTable.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ICollection<Sales>> GetSales()
        {
            return await _context.SalesTable.Include(x => x.Customer).Include(x => x.Product).Include(x => x.Store).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateSales(Sales sales)
        {
            _context.Update(sales);
            return await Save();
        }
    }
}
