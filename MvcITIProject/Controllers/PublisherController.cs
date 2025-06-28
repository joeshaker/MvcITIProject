using Microsoft.AspNetCore.Mvc;

namespace MvcITIProject.Controllers
{
    public class PublisherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
