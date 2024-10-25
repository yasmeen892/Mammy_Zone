
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.CommonProp;

namespace WebApplication1.Models
{
    public class Event : SharedProp
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "EventName required")]
        [DisplayName("Event Name")]
        public string EventName { get; set; }

        [DisplayName("Event Description")]
        public string EventDescription { get; set; }

        public decimal EventPrice { get; set; }

        [DisplayName("Event Hours")]
        public string EventHours { get; set; }

        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

       

        public string TargetGroup { get; set; }

        public decimal TicketPrice { get; set; }
        public bool IsFree { get; set; }


     
    }
}
