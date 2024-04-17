using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetroRealm.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RetroRealm.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string returnUrl = "")
        {
            return View(new LoginVM() { ReturnURL = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            User user = new()
            {
                UserName = model.Username
            };

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password,
                isPersistent: model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnURL) &&
                    Url.IsLocalUrl(model.ReturnURL))
                {
                    return Redirect(model.ReturnURL);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            User user = new User() { UserName = model.UserName };

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, model.Password.ToString());
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, model.Password,
                    isPersistent: false, lockoutOnFailure: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
