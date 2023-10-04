using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using System.Security.Claims;

namespace MVCAuth.Models
{
    public class MSASignInManager
    {
        AccountService accountService;
        HttpContext httpContext;
        public MSASignInManager(IHttpContextAccessor httpContextAccessor,AccountService accountService)
        {
            this.accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<bool> SignIn(string email, string password, bool keepLogined)
        {
            bool isSignedIn = false;

            Account account = accountService.GetAccount(email);
            isSignedIn = (account != null && account.Password == password);
            if (isSignedIn) 
            {

                List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,account.FullName),
                    new Claim("Roles", string.Join(",", account.Roles)),
                    new Claim(ClaimTypes.Email,account.Email),
                    new Claim(ClaimTypes.MobilePhone,account.MobileNo)
                    //new Claim(ClaimTypes.StreetAddress,account.Address)
                    };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = keepLogined
                };

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
               
            }
            return isSignedIn;
        }
        public string UserName 
        {
            get
            {
                var x = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return x;
            }

        }
        public ClaimsPrincipal? User
        {
            get
            {
                var x = _httpContextAccessor.HttpContext?.User;
                if (x != null)
                {
                    return x;
                }
                return null;
            }

        }
        IHttpContextAccessor _httpContextAccessor;
        
        public bool IsSignedIn()
        {
            ClaimsPrincipal claimsPrincipal = _httpContextAccessor.HttpContext.User;
            return claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated;

        }
    }
}
