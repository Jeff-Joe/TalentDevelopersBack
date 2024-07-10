using Microsoft.EntityFrameworkCore;
using TalentDevelopers.Interfaces;
using TalentDevelopers.Models;

namespace TalentDevelopers.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TalentDevelopersContext _context;

        public CustomerRepository(TalentDevelopersContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            return await Save();
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(x => x.Id == id);
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return await Save();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return await Save();
        }
    }
}
