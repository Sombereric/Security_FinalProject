// FILE : UserOwnedGame.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// Owned games by the logged-in user

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public User? User { get; set; }
        public UserOwnedGame() { }
        /// <summary>
        /// used to create a new game owned by a user
        /// </summary>
        /// <param name="userID">whom bought the game</param>
        /// <param name="gameID">the game purchased</param>
        /// <param name="dateAdded">the date of purchase</param>
        public UserOwnedGame(int userID, int gameID, DateTime dateAdded) { 
            UserID = userID;
            GameID = gameID;
            DateAdded = dateAdded;
        }
    }
}