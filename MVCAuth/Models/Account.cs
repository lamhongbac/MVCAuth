namespace MVCAuth.Models
{
    public class Account
    {
        public Account()
        {
            Roles = new List<string>();
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public List<string> Roles { get; set; }
        public string Password { get; set; }
    }
}
