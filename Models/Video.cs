using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Video
	{

		public int VideoId { get; set; }

        [DisplayName("Video Path")]
        public string? VideoPath { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "An introductory video file is required.")]
        public IFormFile VideoFile { get; set; }
        public string Title { get; set; } // عنوان الفيديو
		public int WorkShopId { get; set; }
		public WorkShop WorkShop { get; set; }


    }
}
