namespace MistAPI.Models.Responses
{
    public class AuthenticationResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
