using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcITIProject.IRepositories;
using MvcITIProject.Models;
using MvcITIProject.ModelView;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject.Controllers
{
    public class FloorController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public FloorController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Floor
        public IActionResult Index()
        {
            var floors = _unitOfWork.Floors.GetAll();
            var floorViewModels = floors.Select(f => new FloorViewModel
            {
                Number = f.Number,
                NumBlocks = f.NumBlocks,
                HiringDate = f.HiringDate,
                ShelvesCount = f.Shelves?.Count ?? 0
            }).ToList();

            return View(floorViewModels);
        }

        // GET: Floor/Details/5
        public IActionResult Details(int id)
        {
            var floor = _unitOfWork.Floors.GetById(id);
            if (floor == null)
            {
                return NotFound();
            }

            var floorViewModel = new FloorDetailsViewModel
            {
                Number = floor.Number,
                NumBlocks = floor.NumBlocks,
                HiringDate = floor.HiringDate,
                Shelves = floor.Shelves?.ToList() ?? new List<Shelf>()
            };

            return View(floorViewModel);
        }

        // GET: Floor/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View(new FloorCreateViewModel());
        }

        // POST: Floor/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FloorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var floor = new Floor
                {
                    Number = model.Number,
                    NumBlocks = model.NumBlocks,
                    HiringDate = model.HiringDate
                };

                _unitOfWork.Floors.Add(floor);
                _unitOfWork.SaveChanges();

                TempData["SuccessMessage"] = "Floor created successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Floor/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var floor = _unitOfWork.Floors.GetById(id);
            if (floor == null)
            {
                return NotFound();
            }

            var model = new FloorEditViewModel
            {
                Number = floor.Number,
                NumBlocks = floor.NumBlocks,
                HiringDate = floor.HiringDate
            };

            return View(model);
        }

        // POST: Floor/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FloorEditViewModel model)
        {
            if (id != model.Number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var floor = _unitOfWork.Floors.GetById(id);
                if (floor == null)
                {
                    return NotFound();
                }

                floor.NumBlocks = model.NumBlocks;
                floor.HiringDate = model.HiringDate;

                _unitOfWork.Floors.Update(floor);
                _unitOfWork.SaveChanges();

                TempData["SuccessMessage"] = "Floor updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Floor/Delete/5
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var floor = _unitOfWork.Floors.GetById(id);
            if (floor == null)
            {
                return NotFound();
            }

            var model = new FloorViewModel
            {
                Number = floor.Number,
                NumBlocks = floor.NumBlocks,
                HiringDate = floor.HiringDate,
                ShelvesCount = floor.Shelves?.Count ?? 0
            };

            return View(model);
        }

        // POST: Floor/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _unitOfWork.Floors.Delete(id);
                _unitOfWork.SaveChanges();

                TempData["SuccessMessage"] = "Floor deleted successfully!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Cannot delete floor. It may have associated shelves.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}