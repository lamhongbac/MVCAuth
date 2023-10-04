namespace MVCAuth.Models
{
    public class AccountService
    {
        List<Account> _accounts = new List<Account>()
        {
            new Account()
            {
                 Email="lamhong.bac@gmail.com",
                  FullName="Lam Hong Bac",
                   MobileNo="0913660575",
                   Password="123",
                   Roles=new List<string>() {"Admin","CS"}
            },
            new Account()
            {
                 Email="lhb@gmail.com",
                  FullName="Le Hoang Bac",
                   MobileNo="0913660686",
                   Password="123",
                   Roles=new List<string>() {"IT","HR"}
            },
        };
        public AccountService() { 
        
        }
        public Account GetAccount(string email)
        {
            return _accounts.FirstOrDefault(x => x.Email == email);
        }
    }
}
