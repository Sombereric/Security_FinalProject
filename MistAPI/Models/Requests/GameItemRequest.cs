// FILE : GameItemRequest.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// a model for handling purhcasing games and downloading games requests

using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models.Requests
{
    public class GameItemRequest
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int GameID { get; set; }
    }
}
