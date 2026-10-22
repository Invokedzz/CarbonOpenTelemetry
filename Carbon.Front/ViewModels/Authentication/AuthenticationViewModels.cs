namespace Carbon.Front.ViewModels.Authentication
{
    public class RegisterRequestViewModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterResponseViewModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    
    public class LoginRequestViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}