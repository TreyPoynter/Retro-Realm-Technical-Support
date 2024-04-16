namespace RetroRealm.Models
{
    public class LoginVM
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ReturnURL { get; set; }
        public bool RememberMe { get; set; }
    }
}
