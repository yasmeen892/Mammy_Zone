using WebApplication1.Models.CommonProp;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Article:SharedProp
    {

        public int ArticleId { get; set; }
        [Required(ErrorMessage = " Enter Article Title")]
        [DisplayName("Article Title")]
        public string ArticleTitle { get; set; }


        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string ArticleContent { get; set; }

    



    }
}
