using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


using Microsoft.AspNetCore.Mvc;
using MVCAuth.Models;


namespace MVCAuth.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            LoginVM model = new LoginVM();
            return View(model);
        }

        [HttpPost]
        public async IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email != null && model.Password!=null) {
                
                    List<Claim> claims = new List<Claim>()
                    { new Claim(ClaimTypes.NameIdentifier,model.Email),
                    new Claim("Roles","admin;cms")}; 
                    ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties() { 
                     AllowRefresh = true,
                     IsPersistent = model.KeepLogin,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity), properties);
                    return RedirectToAction("Index", "Home");

                }
            }
            ViewData["validateMessage"] = "user not found";
            return View();

        }
    }
}
