using TalentDevelopers.Models;

namespace TalentDevelopers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> GetCustomers();
        Task<Customer?> GetCustomer(int id);
        bool CustomerExists(int id);
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Customer customer);
        Task<bool> Save();
    }
}
