using TalentDevelopers.Models;

namespace TalentDevelopers.ViewModels
{
    public class SalesViewModelGet
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int StoreId { get; set; }

        public DateTime DateSold { get; set; }

        public CustomerViewModel Customer { get; set; }

        public ProductViewModel Product { get; set; }

        public StoreViewModel Store { get; set; }
    }
}
