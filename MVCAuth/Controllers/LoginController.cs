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
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == "lamhong.bac@gmail.com" && model.Password == "123")
                {

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,"Lam Hong Bac"),
                    new Claim(ClaimTypes.Role,"admin;cms"),
                    new Claim(ClaimTypes.Email,model.Email),
                    new Claim("CongTy","MSA"),
                    new Claim("Address","123")
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = model.KeepLogin
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity), properties);

                    return RedirectToAction("Index", "Home");

                }

            }
            ViewData["validateMessage"] = "User or password is not matched";
            return View(model);

        }

        public IActionResult Register()
        {
            RegisterVM model = new RegisterVM();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                //kiem tra valid va ghi vao CSDL
            }
            return View(model);
        }
    }
}
