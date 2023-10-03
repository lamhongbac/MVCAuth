using System.Net;
using System.Security.Claims;
namespace MVCAuth.Models
{
    public class UserManager
    {
        MSASignInManager signInManager;
        public UserManager(MSASignInManager signInManager)
        {
            this.signInManager = signInManager;
        }
        public UpdateProfileVM GetProfile(string email)
        {
            int id = new Random().Next(100, DateTime.Now.Year);
            UpdateProfileVM model = new UpdateProfileVM()
            {
                Email = email,
                DOB = DateTime.Now.AddYears(-18),
                FullName = "Lam Hong Bac",
                MobileNo = "0913660575",
                ID = id
            };
            return model;
        }
        public UpdateProfileVM GetProfile()
        {
            UpdateProfileVM model = new UpdateProfileVM();
            if (signInManager.IsSignedIn())
            {
                ClaimsPrincipal? User =signInManager.User;
                if (User != null)
                {
                    string email = User.FindFirstValue(ClaimTypes.Email);
                    string addess = User.FindFirstValue("Address");
                    model = GetProfile(email);
                }
            }
           
            return model;
        }
        /// <summary>
        /// return error if Update failed
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateProfile(UpdateProfileVM model)
        {
            return string.Empty;
        }
    }
}
