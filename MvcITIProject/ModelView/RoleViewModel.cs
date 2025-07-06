using System.ComponentModel.DataAnnotations;

namespace MvcITIProject.ModelView
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
