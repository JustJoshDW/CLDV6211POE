using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ST10313014_P2.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<CartRepository> logger)
        {
            _db = db;
            this._userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<int> AddItem(int ProductId, int qty)
        {
            // cart - saved
            // cartDetail -> error
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserID = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                //cart detail section
                var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == ProductId);
                if (cartItem != null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var product = _db.Products.Find(ProductId);
                    cartItem = new CartDetail
                    {
                        ProductId = ProductId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice= product.Price //new line after update xyz
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCardItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveItem(int ProductId)
        {

            //using var transaction = _db.Database.BeginTransaction();
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Cart is empty");
                //cart detail section
                var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == ProductId);
                if (cartItem is null)
                    throw new Exception("no items in cart");
                else if (cartItem.Quantity == 1)
                    _db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _db.SaveChanges();
                
            }
            catch (Exception ex)
            {
               
            }
            var cartItemCount = await GetCardItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
            {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var ShoppingCart = await _db.ShoppingCarts
                .Include(a => a.CartDetails)
                .ThenInclude(a => a.Product)
                .ThenInclude(a => a.Category)
                .Where(a => a.UserID == userId).FirstOrDefaultAsync();
            return ShoppingCart;
            }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserID == userId);
            return cart;
        }

        public async Task<int> GetCardItemCount(string userId="")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in _db.ShoppingCarts
                              join CartDetail in _db.CartDetails
                              on cart.Id equals CartDetail.ShoppingCartId
                              select new { CartDetail.Id }
                              ).ToListAsync();
            return data.Count;
        }

        public async Task<bool> DoCheckout()
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                _logger.LogInformation("Starting checkout process...");

                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid Cart");

                var cartDetail = _db.CartDetails.Where(a => a.ShoppingCartId == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");
                var order = new Order
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    OrderStatusId = 1 
                };
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Order details added successfully.");

                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        Price = item.UnitPrice
                    };
                    _db.OrderDetails.Add(orderDetail);
                }
                await _db.SaveChangesAsync();

                _db.CartDetails.RemoveRange(cartDetail);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Cart details removed successfully.");

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during checkout process.");
                await transaction.RollbackAsync();
                return false;
            }
        }


        private string GetUserId()
        {
            ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
