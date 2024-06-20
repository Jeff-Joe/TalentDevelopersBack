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
        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(x => x.Id == id);
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return Save();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }
    }
}
