// FILE : LoginRequest.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// This is a object file that holds login attempts from the webpages

using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
    }
}
