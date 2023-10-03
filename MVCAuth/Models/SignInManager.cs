using System.Security.Claims;

namespace MVCAuth.Models
{
    public class MSASignInManager
    {
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
        public MSASignInManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsSignedIn()
        {
            ClaimsPrincipal claimsPrincipal = _httpContextAccessor.HttpContext.User;
            return claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated;

        }
    }
}
