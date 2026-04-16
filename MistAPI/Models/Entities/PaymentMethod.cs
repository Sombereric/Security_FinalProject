using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MistAPI.Models.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CardHolderName { get; set; } = string.Empty;

        [Required]
        [MaxLength(19)]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string CardSecurityNumber { get; set; } = string.Empty;

        [Required]
        public DateTime CardExpireDate { get; set; }

        // 🔗 Relationship
        [ForeignKey("UserID")]
        public User? User { get; set; }
    }
}
