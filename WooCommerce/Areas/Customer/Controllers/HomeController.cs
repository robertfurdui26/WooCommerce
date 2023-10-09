using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WooCommerce.Models;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productsList);
        }

        public IActionResult Details(int productId)
        {
            ShoopingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(x => x.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };

         return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoopingCart shopcart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shopcart.ApplicationUserId = userId;

            ShoopingCart cartDb = _unitOfWork.ShoopingCart.Get(x => x.ApplicationUserId == userId &&
            x.ProductId == shopcart.ProductId);

            if(cartDb != null)
            {
                //shopping cart exists
                cartDb.Count += shopcart.Count;
                _unitOfWork.ShoopingCart.Update(cartDb);


            }
            else
            {
                //add a cart
                _unitOfWork.ShoopingCart.Add(shopcart);

            }
            TempData["success"] = "Order Place  successfully";
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}