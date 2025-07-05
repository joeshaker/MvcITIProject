using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcITIProject.ModelView;
using System.Threading.Tasks;

namespace MvcITIProject.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel RoleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = RoleVM.RoleName;
                IdentityResult result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return View("Create");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Create", RoleVM);
        }
        [Authorize(Roles = "admin")]
        public IActionResult List()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
    }
}
