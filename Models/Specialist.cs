
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebApplication1.Models.CommonProp;

namespace WebApplication1.Models
{
    public class Specialist:SharedProp
    {
  
    
        public int SpecialistId { get; set; }


        [Required(ErrorMessage = "Enter Name")]

        [DisplayName("Specialist Name")]
        public string SpecialistName { get; set; }


        [Required(ErrorMessage = "Specialist Certificate reqired")]

        [DisplayName("Specialist Certificate")]
        public string SpecialistCertificate { get; set; }


        public string Experience { get; set; }



        public string SpecialistFacebookLinck { get; set; }
        public string SpecialistInstgramLinck { get; set; }
        public string SpecialistTwiterLinck { get; set; }



        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }


    }
}
