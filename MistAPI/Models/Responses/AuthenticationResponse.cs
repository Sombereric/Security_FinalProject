using MistAPI.Models.Entities;
using MistAPI.Models.Responses;

namespace MistAPI.Models.Responses
{
    public class AuthenticationResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AuthenticationResponse() { }
        public AuthenticationResponse(int userId, string userName, string userEmail, string message)
        {
            UserID = userId;
            UserName = userName;
            UserEmail = userEmail;
            Message = message;
        }

    }
}