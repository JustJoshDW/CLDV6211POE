using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ST10313014_P2.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo) 
        {
            this._cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int ProductId, int qty=1, int redirect=0)
        {
            var cartCount = await _cartRepo.AddItem(ProductId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            //else
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> RemoveItem(int ProductId)
        {
            var cartCount = await _cartRepo.RemoveItem(ProductId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItem = await _cartRepo.GetCardItemCount();
            return Ok(cartItem);
        }

        public async Task<IActionResult> Checkout()
        {
            bool isCheckedOut = await _cartRepo.DoCheckout();
            if (!isCheckedOut)
            {
                // Log the error or display an appropriate message to the user
                TempData["ErrorMessage"] = "An error occurred during checkout. Please try again.";
                return RedirectToAction("GetUserCart");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
