using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcITIProject.Models;
using MvcITIProject.Repositories;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject.Controllers
{
    public class AuthorController : Controller
    {
        UnitOfWork _UnitOfWork;
        public AuthorController(UnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }


        public IActionResult ShowAll()
        {
            var Authors = _UnitOfWork.authorRepo.GetAll();
          

            return View(Authors);
        }
        public IActionResult AddView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Author author)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.authorRepo.Add(author);
                _UnitOfWork.SaveChanges();

                return RedirectToAction("ShowAll", new { id = author.Id });
            }
            return View("AddView");
        }


        public IActionResult Details(int id)
        {

            var author = _UnitOfWork.authorRepo.GetById(id);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }


        public IActionResult Delete(int id)
        {
            var author = _UnitOfWork.authorRepo.GetById(id);

            if (author == null)
            {
                return NotFound();
            }
            _UnitOfWork.authorRepo.Delete(id);
            _UnitOfWork.SaveChanges();

            return RedirectToAction("ShowAll");
        }


        
    }
}
