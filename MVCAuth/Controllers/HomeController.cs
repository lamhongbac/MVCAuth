using Microsoft.AspNetCore.Mvc;
using MVCAuth.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MVCAuth.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;        
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager userManager;
        public HomeController(IHttpContextAccessor httpContextAccessor, 
            ILogger<HomeController> logger,
            UserManager userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            _logger = logger;
            var x = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier); //get in the constructor
        }
        [MSAAuthorizeAttribute(Roles = "Admin,IT")]
        public IActionResult Index()
        {
            // you could also get in your method
            var x = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [MSAAuthorizeAttribute(Roles = "IT,HR")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("UserName");


            return RedirectToAction("Login","Login");
        }
        public IActionResult UpdateProfile()
        {
            UpdateProfileVM vm = userManager.GetProfile();
            return View(vm);
        }
        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileVM model)
        {
            string error = userManager.UpdateProfile(model);

            if (error != null)
            {
                ModelState.AddModelError("", error);
                
            }
            return View(model);
        }
    }
}