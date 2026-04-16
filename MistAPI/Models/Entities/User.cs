using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models.Entities
{
    public class User
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
        public List<UserOwnedGame> OwnedGames { get; set; } = new();
        public List<PaymentMethod> PaymentMethodsList { get; set; } = new();

        public User() { }

        public User (string userName, string email)
        {
            UserName = userName;
            UserEmail = email;
        }
    }
}
