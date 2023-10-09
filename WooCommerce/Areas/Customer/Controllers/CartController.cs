using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WooCommerce.Models;
using WooCommerce.Models.ViewModels;
using WooCommerce.Repository.IRepository;

namespace WooCommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
         public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoopingCartsList = _unitOfWork.ShoopingCart.GetAll(x =>x.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };


            foreach( var cart in ShoppingCartVM.ShoopingCartsList )
            {
                cart.Price = GetPriceBaseOnQuantity( cart );
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoopingCartsList = _unitOfWork.ShoopingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"),
                OrderHeader = new()
            };


            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u=> u.Id == userId);



            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAdress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAdress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cart in ShoppingCartVM.ShoopingCartsList)
            {
                cart.Price = GetPriceBaseOnQuantity(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }



            return View(ShoppingCartVM);
        }



        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoopingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoopingCart.Update(cartFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
         var cartFromDb = _unitOfWork.ShoopingCart.Get(u =>u.Id == cartId);

            if(cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoopingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoopingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

            

        }


        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoopingCart.Get(u => u.Id == cartId);
            
                //reomve that from cart
                _unitOfWork.ShoopingCart.Remove(cartFromDb);
          
            
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        private double GetPriceBaseOnQuantity(ShoopingCart shoopingCart)
        {
            if(shoopingCart.Count <= 50)
            {
                return shoopingCart.Product.Price;
            }
            else
            {
                if(shoopingCart.Count <= 100)
                {
                    return shoopingCart.Product.Price50;
                }
                else
                {
                    return shoopingCart.Product.Price100;
                }
            }
        }
    }
}
