using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCK.Models
{
    public class BookingForm
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime BookingDate { get; set; } = DateTime.Now;
        public int NumberOfGuests { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int BookingFee { get; set; } = 0;
        public int Deposit { get; set; } = 0;
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public int? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }
    }
}
