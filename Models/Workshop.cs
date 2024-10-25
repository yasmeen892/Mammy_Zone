using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using WebApplication1.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class WorkShop : SharedProp
    {
        public int WorkShopId { get; set; }

        [Required(ErrorMessage = "WorkShop Name is required.")]
        [DisplayName("WorkShop Name")]
        public string? WorkShopName { get; set; }

        [Required(ErrorMessage = "WorkShop Time is required.")]
        [DisplayName("WorkShop Time")]
        public DateTime WorkShopTime { get; set; }

        [Required(ErrorMessage = "WorkShop price is required.")]
        [DisplayName("WorkShop Price")]
        public decimal WorkShopPrice { get; set; }

        [Required(ErrorMessage = "WorkShop Hours are required.")]
        [DisplayName("WorkShop Hours")]
        public int WorkShopHours { get; set; }

        public string Summary { get; set; }
        public string TargetGroup { get; set; }

        [DisplayName("Image Path")]
        public string? Image { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "An image file is required")]
        public IFormFile? ImageFile { get; set; }

        public int SpecialistId { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }
        public Specialist? Specialist { get; set; }

        [DisplayName("Intro Video Path")]
        public string? IntroVideoPath { get; set; }

        [NotMapped]
        public IFormFile? IntroVideoFile { get; set; }

        public ICollection<Video>? Videos { get; set; }

    }
}
