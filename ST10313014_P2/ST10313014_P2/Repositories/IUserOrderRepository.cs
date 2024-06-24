namespace ST10313014_P2.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders();
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}