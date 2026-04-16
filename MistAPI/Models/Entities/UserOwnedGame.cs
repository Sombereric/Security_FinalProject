// FILE : UserOwnedGame.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Owned games by the logged-in user

using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models.Entities
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
