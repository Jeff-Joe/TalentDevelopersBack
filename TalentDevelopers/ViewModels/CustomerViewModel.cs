using System.ComponentModel.DataAnnotations;

namespace TalentDevelopers.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [MaxLength(255, ErrorMessage = "Maximum length for name is 255 characters")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Address must be at least 2 characters long")]
        [MaxLength(1000, ErrorMessage = "Maximum length for address is 1000 characters")]
        public string Address { get; set; }
    }
}
