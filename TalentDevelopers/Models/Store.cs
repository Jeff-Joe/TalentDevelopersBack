using System.ComponentModel.DataAnnotations;

namespace TalentDevelopers.Models
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Sales> ProductSold { get; set; }
    }
}
