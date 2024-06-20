using TalentDevelopers.Models;

namespace TalentDevelopers.Interfaces
{
    public interface ISalesRepository
    {
        ICollection<Sales> GetSales();
        Sales GetSale(int id);
        bool SalesExists(int id);
        bool CreateSales(Sales sales);
        bool UpdateSales(Sales sales);
        bool DeleteSales(Sales sales);
        bool Save();
    }
}
