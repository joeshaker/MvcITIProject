using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcITIProject.Models;
using MvcITIProject.ModelView;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject.Controllers
{
    public class PublisherController : Controller
    {
        UnitOfWork _unitofwork;

        public PublisherController(UnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        //public IActionResult Index()
        //{
        //    var viewModels = _unitofwork.Publisherrepo.GetAll().Select(c => new PublisherModelView
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        BookCount = _unitofwork.Publisherrepo.GetBookCount(c.Id)
        //    }).ToList();

        //    return View(viewModels);
        //}
        public IActionResult Index(string searchString, int page = 1)
        {
            const int pageSize = 5;
            IEnumerable<Publisher> Publishers;
            int totalItems;

            if (!string.IsNullOrEmpty(searchString))
            {
                Publishers = _unitofwork.Publisherrepo.Search(searchString)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                totalItems = _unitofwork.Publisherrepo.SearchCount(searchString);
                ViewBag.SearchString = searchString;
            }
            else
            {
                Publishers = _unitofwork.Publisherrepo.GetPaginated(page, pageSize);
                totalItems = _unitofwork.Publisherrepo.TotalCount();
            }

            var viewModels = Publishers.Select(c => new PublisherModelView
            {
                Id = c.Id,
                Name = c.Name,
                BookCount = _unitofwork.Publisherrepo.GetBookCount(c.Id),
                Address = c.Address,
            }).ToList();

            ViewBag.PageNumber = page;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(viewModels);
        }
        // GET: publisher/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: publisher/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Publisherrepo.Add(publisher);
                _unitofwork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }
        // GET: publisher/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var publisher = _unitofwork.Publisherrepo.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }
        // POST: publisher/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Publisherrepo.Update(publisher);
                _unitofwork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }
        // GET: publisher/Delete/5
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var publisher = _unitofwork.Publisherrepo.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            var viewModel = new PublisherModelView
            {
                Id = publisher.Id,
                Name = publisher.Name,
                BookCount = _unitofwork.Publisherrepo.GetBookCount(publisher.Id),
                Address=publisher.Address,
            };

            return View(viewModel);
        }
        // POST: publisher/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitofwork.Publisherrepo.Delete(id);
            _unitofwork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: publisher/Details/5
        public IActionResult Details(int id)
        {
            var books = _unitofwork.Publisherrepo.GetBooksByPublisherId(id);
            var publisher = _unitofwork.Publisherrepo.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            var viewModel = new PublisherModelView
            {
                Id = publisher.Id,
                Name = publisher.Name,
                BookCount = _unitofwork.Publisherrepo.GetBookCount(publisher.Id),
                Books = books,
                Address = publisher.Address,
            };

            return View(viewModel);
        }
    }
}
