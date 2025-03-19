namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public interface IProductRepository
    {
        Product[] GetAllProducts();
        Product GetOneProductById(int id);
        void UpdateProductStocks(int productId, int quantityToRemove);
    }
}
