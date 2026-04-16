// FILE : RegisterRequest.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// This is a object file that holds creating new accounts
using System.ComponentModel.DataAnnotations;

namespace MistAPI.Models.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        [MinLength(10)]
        public string Password { get; set; } = string.Empty;
    }
}
