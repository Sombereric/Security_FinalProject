// FILE : AuthenticationResponse.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// A response body used for authentication methods

namespace MistAPI.Models.Responses
{
    public class AuthenticationResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AuthenticationResponse() { }
        /// <summary>
        /// a constructor that builds the response body for the api
        /// </summary>
        /// <param name="userId">id of the user</param>
        /// <param name="userName">users name for the account</param>
        /// <param name="userEmail">email tied to the account</param>
        /// <param name="message">result of the authentication</param>
        public AuthenticationResponse(int userId, string userName, string userEmail, string message)
        {
            UserID = userId;
            UserName = userName;
            UserEmail = userEmail;
            Message = message;
        }
    }
}