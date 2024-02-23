public class UserSignUpRequest
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string userName { get; set; }
    public string password { get; set; }
    public string confirmPassword { get; set; }
    public string referredBy { get; set; }
    public string referredByEmail { get; set; }
    public string countryCode { get; set; }
    public long createdAt { get; set; }

}