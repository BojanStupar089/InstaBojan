namespace InstaBojan.Core.Security
{
    public class JwtToken
    {
        public string SecretKey { get; set; }
        public int TokenExpirationMinutes { get; set; }
    }
}
