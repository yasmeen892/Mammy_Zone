

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
              
                UserName = model.UserName
            };

            var r = await userManager.CreateAsync(user, model.Password);
            if (r.Succeeded)

            {
                return RedirectToAction("Login");
            }
            foreach (var err in r.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View(model);



        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Dashboard" });
                }
                ModelState.AddModelError("", "Invalid User or password");
                return View(model);

            }
            return View(model);
        }

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
