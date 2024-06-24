namespace ST10313014_P2
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int CategoryId = 0);
        Task<IEnumerable<Category>> Categories();
    }
}