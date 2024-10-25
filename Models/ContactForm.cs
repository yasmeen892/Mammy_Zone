using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebApplication1.Models.CommonProp;

namespace WebApplication1.Models
{
    public class ContactForm:SharedProp
    {

        public int ContactFormId { get; set; }

        [Required(ErrorMessage = " Phone required")]

        [DisplayName("Clint Phone")]
        public string ClintPhone { get; set; }

        [Required(ErrorMessage = " Message required")]

        [DisplayName("Clint Message")]
        public string ClintMessage { get; set; }

        [Required(ErrorMessage = " ClintEmail")]

        [DisplayName("Clint Email")]
        public string ClintEmail { get; set; }
    }
}

  

