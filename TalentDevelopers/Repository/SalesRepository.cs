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
        public bool CreateSales(Sales sales)
        {
            _context.Add(sales);
            return Save();
        }

        public bool SalesExists(int id)
        {
            return _context.SalesTable.Any(x => x.Id == id);
        }

        public bool DeleteSales(Sales sales)
        {
            _context.Remove(sales);
            return Save();
        }

        public Sales GetSale(int id)
        {
            return _context.SalesTable.Where(x => x.Id == id).SingleOrDefault();
        }

        public ICollection<Sales> GetSales()
        {
            return _context.SalesTable.Include(x => x.Customer).Include(x => x.Product).Include(x => x.Store).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSales(Sales sales)
        {
            _context.Update(sales);
            return Save();
        }
    }
}
