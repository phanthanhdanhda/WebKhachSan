using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCK.Models
{
    public class DepositBill
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime DateCreated { get; set;} = DateTime.Now;
        public int TotalFee { get; set; } = 0;
        public int? FormId { get; set; }
        [ForeignKey("FormId")]
        public BookingForm? BookingForm { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
