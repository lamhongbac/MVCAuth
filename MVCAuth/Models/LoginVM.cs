namespace MVCAuth.Models
{
    public class LoginVM
    {
        public LoginVM()
        {
            KeepLogin = false;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public  bool KeepLogin { get; set; }
    }
}
