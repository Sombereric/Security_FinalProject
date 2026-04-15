using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models
{
    public class UserOwnedGame
    {
        [Required]
        public int GameID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        // Navigation properties
        public GameInApp? Game { get; set; }
        public User? User { get; set; }
    }
}
