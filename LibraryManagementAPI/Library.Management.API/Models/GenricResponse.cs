
namespace Library.Management.API.Models
{
    public class GenricResponse
    {
        public string Message { get; set; }
        public bool IsSuccesful { get; set; }
    }

    public class AuthenticationResponse
    {
        public AuthenticationResponse()
        {
            Token = new Token();
        }
        public string Message { get; set; }
        public bool IsSuccesful { get; set; }

        public Token Token { get; set; }

    }
    public class Token
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
    }
}
