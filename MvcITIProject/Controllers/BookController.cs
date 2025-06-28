using Microsoft.AspNetCore.Mvc;
using MvcITIProject.Models;
using MvcITIProject.ModelView;
using MvcITIProject.UnitOfWorks;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace MvcITIProject.Controllers
{
    public class BookController : Controller
    {
        UnitOfWork _unitofwork;
        public BookController(UnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public IActionResult Index()
        {
            List <Book> AllBooks= _unitofwork.Bookrepo.GetAll();
            var viewModel = AllBooks.Select(b => new BookModelView
            {
                Title = b.Title,
                CatId = b.CatId,
                PublisherId = b.PublisherId,
                ShelfCode = b.ShelfCode,
                Cat = b.Cat,
                Publisher = b.Publisher,
                ShelfCodeNavigation = b.ShelfCodeNavigation,
                Authors = b.Authors
            }).ToList();
            return View(viewModel);
        }
        public IActionResult Delete(int id)
        {
            _unitofwork.Bookrepo.Delete(id);
            return RedirectToAction("index");
        }
    }
}
