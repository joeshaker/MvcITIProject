using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcITIProject.Models;
using MvcITIProject.ModelView;

namespace MvcITIProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                model.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = roles
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new RoleAssignmentViewModel
            {
                UserId = user.Id,
                AllRoles = _roleManager.Roles.Select(r => r.Name).ToList(),
                SelectedRoles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleAssignmentViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            foreach (var role in allRoles)
            {
                if (model.SelectedRoles.Contains(role))
                {
                    if (!userRoles.Contains(role))
                        await _userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    if (userRoles.Contains(role))
                        await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
