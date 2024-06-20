using System.ComponentModel.DataAnnotations;

namespace TalentDevelopers.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [MaxLength(255, ErrorMessage = "Maximum length for name is 255 characters")]
        public string Name { get; set; }

        public float Price { get; set; }
    }
}
