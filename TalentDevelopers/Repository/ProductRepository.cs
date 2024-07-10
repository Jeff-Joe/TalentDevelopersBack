using Microsoft.EntityFrameworkCore;
using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;

namespace TalentDevelopers.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly TalentDevelopersContext _context;

        public ProductRepository(TalentDevelopersContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            await _context.AddAsync(product);
            return await Save();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(x => x.Id == id);
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Remove(product);
            return await Save();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _context.Update(product);
            return await Save();
        }
    }
}
