using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcITIProject.Models;
using MvcITIProject.ModelView;
using System.Threading.Tasks;

namespace MvcITIProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel RegisterVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = RegisterVM.UserName,
                    Email = RegisterVM.Email,
                };
                IdentityResult result = await userManager.CreateAsync(user, RegisterVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(RegisterVM);
        }

        [Authorize(Roles = "admin")]
        public IActionResult RegisterForAdmins()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterForAdmins(RegisterUserViewModel RegisterVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = RegisterVM.UserName,
                    Email = RegisterVM.Email,
                };
                IdentityResult result = await userManager.CreateAsync(user, RegisterVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(RegisterVM);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    loginVM.UserName,
                    loginVM.Password,
                    loginVM.RememberMe,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Logged in successfully!";
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
