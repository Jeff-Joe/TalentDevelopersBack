using TalentDevelopers.Models;

namespace TalentDevelopers.Interfaces
{
    public interface IStoreRepository
    {
        Task<ICollection<Store>> GetStores();
        Task<Store> GetStore(int id);
        bool StoreExists(int id);
        Task<bool> CreateStore(Store store);
        Task<bool> UpdateStore(Store store);
        Task<bool> DeleteStore(Store store);
        Task<bool> Save();
    }
}
