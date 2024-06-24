namespace ST10313014_P2.Repositories
{
    public interface ICartRepository
    {
         Task<int> AddItem(int ProductId, int qty);
        Task<int> RemoveItem(int ProductId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCardItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout();
    }
}