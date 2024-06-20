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
        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(x => x.Id == id);
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(x => x.Id == id).SingleOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
