using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ST10313014_P2.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this._db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged in");
            var orders = await _db.Orders
                                .Include(x=>x.OrderStatus)
                                .Include(x=>x.OrderDetail)
                                .ThenInclude(x=>x.Product)
                                .ThenInclude(x=>x.Category)
                                .Where(a=> a.UserId==userId)
                                .ToListAsync();
            return orders;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync() 
        {
            var orders = await _db.Orders
                                .Include(x => x.OrderStatus)
                                .Include(x => x.OrderDetail)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Category)
                                .ToListAsync();
            return orders;
        }

        private string GetUserId()
        {
            ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
