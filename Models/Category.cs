using System.ComponentModel.DataAnnotations;

namespace WebCK.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public List<Room>? Categories { get; set;}
    }
}
