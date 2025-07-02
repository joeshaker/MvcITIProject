using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcITIProject.Models;
using MvcITIProject.ModelView.ShelfModelView;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject.Controllers
{
    public class ShelfController : Controller
    {
        UnitOfWork _UnitOfWork;
        public ShelfController(UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var allShelves = _UnitOfWork.ShelfRepo.GetAll().ToList();

            var paginatedShelves = allShelves
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)allShelves.Count / pageSize);

            return View(paginatedShelves);
        }


        public IActionResult AddViewShelf() {

            List<Floor>?floor = _UnitOfWork.Repositries<Floor>().GetAll().ToList();
            AddShelfViewModel addShelfViewModel = new AddShelfViewModel
            {
                FloorOptions = floor.Select(f => new SelectListItem
                {
                    Value = f.Number.ToString(),
                    Text = $"Floor {f.Number}"
                }).ToList()
            };
            return View(addShelfViewModel);
        }
        [HttpPost]
        public IActionResult AddShelf(AddShelfViewModel addShelfView)
        {
            if (ModelState.IsValid)
            {
                Shelf shelf = new Shelf
                {
                    Code = addShelfView.Code,
                    FloorNum = addShelfView.FloorNum
                };
                _UnitOfWork.ShelfRepo.Add(shelf);
                _UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Floor>? floor = _UnitOfWork.Repositries<Floor>().GetAll().ToList();
            addShelfView.FloorOptions = floor.Select(f => new SelectListItem
            {
                Value = f.Number.ToString(),
                Text = $"Floor {f.Number}"
            }).ToList();
            return View("AddViewShelf", addShelfView);
        }

        public IActionResult EditShelfView(string code)
        {
            List<Floor>? floor = _UnitOfWork.Repositries<Floor>().GetAll().ToList();
            Shelf shelf = _UnitOfWork.ShelfRepo.GetByCode(code);
            AddShelfViewModel addShelfViewModel = new AddShelfViewModel
            {
                Code = shelf.Code,
                FloorNum = shelf.FloorNum,
                FloorOptions = floor.Select(f => new SelectListItem
                {
                    Value = f.Number.ToString(),
                    Text = $"Floor {f.Number}"
                }).ToList()
            };
            return View(addShelfViewModel);
        }
        [HttpPost]
        public IActionResult EditShelf(AddShelfViewModel addShelfView)
        {
            if (ModelState.IsValid)
            {
                Shelf shelf = _UnitOfWork.ShelfRepo.GetByCode(addShelfView.Code);
                if (shelf == null)
                {
                    return NotFound();
                }
                shelf.FloorNum = addShelfView.FloorNum;
                _UnitOfWork.ShelfRepo.Update(shelf);
                _UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Floor>? floor = _UnitOfWork.Repositries<Floor>().GetAll().ToList();
            addShelfView.FloorOptions = floor.Select(f => new SelectListItem
            {
                Value = f.Number.ToString(),
                Text = $"Floor {f.Number}"
            }).ToList();
            return View("EditShelfView", addShelfView);
        }

        public IActionResult DeleteView(string code)
        {
            List<Floor>? floor = _UnitOfWork.Repositries<Floor>().GetAll().ToList();
            Shelf shelf = _UnitOfWork.ShelfRepo.GetByCode(code);
            AddShelfViewModel addShelfViewModel = new AddShelfViewModel
            {
                Code = shelf.Code,
                FloorNum = shelf.FloorNum,
                FloorOptions = floor.Select(f => new SelectListItem
                {
                    Value = f.Number.ToString(),
                    Text = $"Floor {f.Number}"
                }).ToList()
            };
            return View(addShelfViewModel);

        }


        public IActionResult DeleteShelf(string code) {

            Shelf? shelf = _UnitOfWork.ShelfRepo.GetByCode(code);

            if (shelf == null)
            {
                return NotFound();
            }
            _UnitOfWork.ShelfRepo.DeleteByCode(code);
            _UnitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
