using Microsoft.AspNetCore.Mvc;
using MvcITIProject.Models;
using MvcITIProject.ModelView;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Category/Index
        public IActionResult Index(string searchString, int page = 1)
        {
            const int pageSize = 5; 
            IEnumerable<Category> categories;
            int totalItems;

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = _unitOfWork.CategoryRepo.Search(searchString)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                totalItems = _unitOfWork.CategoryRepo.SearchCount(searchString);
                ViewBag.SearchString = searchString;
            }
            else
            {
                categories = _unitOfWork.CategoryRepo.GetPaginated(page, pageSize);
                totalItems = _unitOfWork.CategoryRepo.TotalCount();
            }

            var viewModels = categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                CatName = c.CatName,
                BookCount = _unitOfWork.CategoryRepo.CountBooksInCategory(c.Id)
            }).ToList();

            ViewBag.PageNumber = page;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(viewModels);
        }
        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Add(category);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // GET: Category/Edit/5
        public IActionResult Edit(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Update(category);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // GET: Category/Delete/5
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                CatName = category.CatName,
                BookCount = _unitOfWork.CategoryRepo.CountBooksInCategory(category.Id)
            };

            return View(viewModel);
        }
        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.CategoryRepo.Delete(id);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: Category/Details/5
        public IActionResult Details(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                CatName = category.CatName,
                BookCount = _unitOfWork.CategoryRepo.CountBooksInCategory(category.Id)
            };

            return View(viewModel);
        }

    }
}
