using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public IActionResult Index(string searchTerm = "", int pageNumber = 1, int pageSize = 5)
        {
            List<Book> allBooks = _unitofwork.Bookrepo.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                allBooks = allBooks
                    .Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int totalBooks = allBooks.Count;
            var paginatedBooks = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookModelView
                {
                    Id = b.Id,
                    Title = b.Title,
                    CatId = b.CatId,
                    PublisherId = b.PublisherId,
                    ShelfCode = b.ShelfCode,
                    Cat = b.Cat,
                    Publisher = b.Publisher,
                    ShelfCodeNavigation = b.ShelfCodeNavigation,
                    Authors = b.Authors,
                    BookLink = b.BookLink
                }).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SearchTerm = searchTerm;

            return View(paginatedBooks);
        }


        public IActionResult Delete(int id)
        {
            var book = _unitofwork.Bookrepo.GetById(id);
            if (book == null) return NotFound();
            return View(book);

        }
        public IActionResult DeleteConfirm(int id)
        {
            _unitofwork.Bookrepo.Delete(id);
            _unitofwork.SaveChanges();
            TempData["SuccessMessage"] = "Book deleted successfully.";
            return RedirectToAction("Index");
        }
        public IActionResult Add()
        {

            BookModelView model = new BookModelView
            {
                Categories = _unitofwork.Repositries<Category>().GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CatName }),
                Publishers = _unitofwork.Repositries<Publisher>().GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name}),
                Shelves = _unitofwork.Repositries<Shelf>().GetAll().Select(s => new SelectListItem { Value = s.Code, Text = s.Code}),
            };
            return View("Add",model);
        }
        [HttpPost]
        public IActionResult AddBook(BookModelView newbook)
        {
            if (!ModelState.IsValid)
            {
                newbook.Categories = _unitofwork.Repositries<Category>().GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CatName });
                newbook.Publishers = _unitofwork.Repositries<Publisher>().GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                newbook.Shelves = _unitofwork.Repositries<Shelf>().GetAll().Select(s => new SelectListItem { Value = s.Code, Text = s.Code });
                return View("Add", newbook);
            }

            Book book = new Book
            {
                Title = newbook.Title,
                CatId = newbook.CatId,
                PublisherId = newbook.PublisherId,
                ShelfCode = newbook.ShelfCode,
                BookLink=newbook.BookLink
            };

            _unitofwork.Bookrepo.Add(book);
            _unitofwork.SaveChanges();

            TempData["SuccessMessage"] = "Book added successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = _unitofwork.Bookrepo.GetById(id);

            if (book == null)
                return NotFound();

            var viewModel = new BookModelView
            {
                Id = book.Id,
                Title = book.Title,
                CatId = book.CatId,
                PublisherId = book.PublisherId,
                ShelfCode = book.ShelfCode,
                Cat = book.Cat,
                Publisher = book.Publisher,
                ShelfCodeNavigation = book.ShelfCodeNavigation,
                Authors = book.Authors,
                BookLink = book.BookLink,

                Categories = _unitofwork.Repositries<Category>().GetAll().Select(c => new SelectListItem{Value = c.Id.ToString(),Text = c.CatName}),
                Publishers = _unitofwork.Repositries<Publisher>().GetAll().Select(p => new SelectListItem{Value = p.Id.ToString(),Text = p.Name}),
                Shelves = _unitofwork.Repositries<Shelf>().GetAll().Select(s => new SelectListItem{Value = s.Code,Text = s.Code})
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult SaveEdit(BookModelView Editedbook)
        {
            if (!ModelState.IsValid)
            {
                Editedbook.Categories = _unitofwork.Repositries<Category>().GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CatName });
                Editedbook.Publishers = _unitofwork.Repositries<Publisher>().GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                Editedbook.Shelves = _unitofwork.Repositries<Shelf>().GetAll().Select(s => new SelectListItem { Value = s.Code, Text = s.Code });

                return View("Edit", Editedbook);
            }

            Book updatedBook = new Book
            {
                Id = Editedbook.Id,
                Title = Editedbook.Title,
                CatId = Editedbook.CatId,
                PublisherId = Editedbook.PublisherId,
                ShelfCode = Editedbook.ShelfCode,
                BookLink=Editedbook.BookLink
            };

            _unitofwork.Bookrepo.Update(updatedBook);
            //_unitofwork.Bookrepo.Update(Editedbook);
            _unitofwork.SaveChanges();

            TempData["SuccessMessage"] = "Book updated successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var book = _unitofwork.Bookrepo.GetById(id);
            if (book == null) return NotFound();

            var model = new BookModelView
            {
                Id = book.Id,
                Title = book.Title,
                CatId = book.CatId,
                PublisherId = book.PublisherId,
                ShelfCode = book.ShelfCode,
                Cat = book.Cat,
                Publisher = book.Publisher,
                ShelfCodeNavigation = book.ShelfCodeNavigation,
                BookLink = book.BookLink
            };
            return View("Details", model);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsTitleUnique(string title, int id = 0)
        {
            var exists = _unitofwork.Bookrepo.GetAll().Any(b => b.Title == title && b.Id != id);

            if (exists)
            {
                return Json($"The title \"{title}\" is already used.");
            }

            return Json(true);
        }
    }
}
