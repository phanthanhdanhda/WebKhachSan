using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCK.Models
{
    public class BookingForm
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime BookingDate { get; set; } = DateTime.Now;
        [Required] public string FullName { get; set; }
        public string? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int BookingFee { get; set; } = 0;
        public int Deposit { get; set; } = 0;
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
