using System.ComponentModel.DataAnnotations;

namespace WebCK.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public List<Room>? Categories { get; set;}
    }
}
