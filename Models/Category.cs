using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
