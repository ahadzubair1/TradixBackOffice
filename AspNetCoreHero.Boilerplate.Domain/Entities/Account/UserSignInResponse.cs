public class UserSignInResponse 
{
        public string token { get; set; }
        public UInt64 expiresAt { get; set; }
        public bool success { get; set; }
}