using TalentDevelopers.Models;

namespace TalentDevelopers.Interfaces
{
    public interface IStoreRepository
    {
        ICollection<Store> GetStores();
        Store GetStore(int id);
        bool StoreExists(int id);
        bool CreateStore(Store store);
        bool UpdateStore(Store store);
        bool DeleteStore(Store store);
        bool Save();
    }
}
