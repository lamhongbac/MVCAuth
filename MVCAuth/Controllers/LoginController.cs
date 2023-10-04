using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


using Microsoft.AspNetCore.Mvc;
using MVCAuth.Models;
using System.Reflection;


namespace MVCAuth.Controllers
{
    public class LoginController : Controller
    {
        MSASignInManager accountService;
        public LoginController(MSASignInManager accountService)
        {
            this.accountService = accountService;
        }
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
            bool isSignIn = false;
            if (ModelState.IsValid)
            {
                isSignIn =await accountService.SignIn(model.Email, model.Password, model.KeepLogin);
                if (isSignIn)
                {

                    
                    //
                    // su dung session de xac dinh da login la 1 cach khac 
                    // dung no de xac dinh ==> roles
                    // sau do thuc hien authorized
                    //

                    HttpContext.Session.SetString("userName", model.Email);

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

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
