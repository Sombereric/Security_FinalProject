// FILE : User.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// a user within the system

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
        /// <summary>
        /// for building a new user. does not store password
        /// </summary>
        /// <param name="userName">users picked name</param>
        /// <param name="email">the email now tied to the account</param>
        public User (string userName, string email)
        {
            UserName = userName;
            UserEmail = email;
        }
    }
}
