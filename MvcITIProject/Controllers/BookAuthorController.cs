using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcITIProject.Models;
using MvcITIProject.ModelView;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject.Controllers
{
    public class BookAuthorController : Controller
    {
        UnitOfWork unitOfWork;
        public BookAuthorController(UnitOfWork _unitOfWork) 
        {
            this.unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddView(int? authorId = null)
        {
            var model = new BookAuthorModelView
            {
                Books = unitOfWork.Bookrepo.GetAll()
                    .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title }),

                Authors = unitOfWork.authorRepo.GetAll()
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name,
                        Selected = authorId != null && a.Id == authorId // pre-select the passed author
                    }),

                SelectedAuthorIds = authorId != null ? new List<int> { authorId.Value } : new List<int>()
            };

            return View("AddView", model);
        }


        [HttpPost]
        public IActionResult AddAuthorsToBook(BookAuthorModelView model)
        {
            if (!ModelState.IsValid)
            {
                model.Books = unitOfWork.Bookrepo.GetAll().Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
                model.Authors = unitOfWork.authorRepo.GetAll().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View("AddView", model);
            }

            var book = unitOfWork.Bookrepo.GetById(model.BookId);
            if (book == null) return NotFound();

            // Optional: clear existing authors if needed
            book.Authors = new List<Author>();

            foreach (var authorId in model.SelectedAuthorIds)
            {
                var author = unitOfWork.authorRepo.GetById(authorId);
                if (author != null)
                {
                    book.Authors.Add(author); // This will populate Book_Author table
                }
            }

            unitOfWork.Bookrepo.Update(book);
            unitOfWork.SaveChanges();

            TempData["SuccessMessage"] = "Authors assigned to book successfully.";
            return RedirectToAction("ShowAll", "Author");
        }


    }
}
