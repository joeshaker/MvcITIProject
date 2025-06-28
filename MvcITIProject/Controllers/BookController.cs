using Microsoft.AspNetCore.Mvc;
using MvcITIProject.Models;
using MvcITIProject.UnitOfWorks;
using System.Collections.Generic;

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
            List < Book > AllBooks= _unitofwork.Bookrepo.GetAll();
            return View(AllBooks);
        }
    }
}
