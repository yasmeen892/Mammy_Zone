using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
      
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Mobile { get; set; }
     


    }
}
