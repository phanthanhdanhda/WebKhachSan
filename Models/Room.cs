using System.ComponentModel.DataAnnotations;

namespace WebCK.Models
{
    public class Room
    {public int Id { get; set; }
        [Required] public string RoomNumber { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
