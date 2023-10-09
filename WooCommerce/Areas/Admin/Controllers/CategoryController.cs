using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WooCommerce.Data;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;
using WooCommerce.Utility;

namespace WooCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategory = _unitOfWork.Category.GetAll().ToList();
            return View(objCategory);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");

            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created succesfuly";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? catDb = _unitOfWork.Category.Get(x => x.Id == id);

            if (catDb == null)
            {
                return NotFound();
            }

            return View(catDb);
        }


        [HttpPost]

        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated succesfuly";

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? cateDb = _unitOfWork.Category.Get(x => x.Id == id);


            if (cateDb == null)
            {
                return NotFound();
            }

            return View(cateDb);
        }


        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";


            return RedirectToAction("Index");
        }

    }
}
