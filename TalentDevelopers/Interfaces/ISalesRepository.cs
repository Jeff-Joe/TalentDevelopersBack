using TalentDevelopers.Models;

namespace TalentDevelopers.Interfaces
{
    public interface ISalesRepository
    {
        Task<ICollection<Sales>> GetSales();
        Task<Sales> GetSale(int id);
        bool SalesExists(int id);
        Task<bool> CreateSales(Sales sales);
        Task<bool> UpdateSales(Sales sales);
        Task<bool> DeleteSales(Sales sales);
        Task<bool> Save();
    }
}
