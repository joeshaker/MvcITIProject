namespace MvcITIProject.ModelView
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }

    public class RoleAssignmentViewModel
    {
        public string UserId { get; set; }
        public List<string> AllRoles { get; set; } = new List<string>();
        public List<string> SelectedRoles { get; set; } = new List<string>();
    }
}
