using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserPasswordHash { get; set; }

        // 🔗 Relationships
        public List<UserOwnedGames> OwnedGames { get; set; }
        public List<PaymentMethods> PaymentMethodsList { get; set; }
    }
}
