namespace AspNetCoreHero.Boilerplate.Application.DTOs.Identity
{
    public class TokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}