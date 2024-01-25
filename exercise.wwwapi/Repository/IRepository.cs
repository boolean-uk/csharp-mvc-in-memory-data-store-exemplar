using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository
    {
        Product AddProduct(Product product);
        IEnumerable<Product> GetProducts();
        Product UpdateProduct(int id, ProductPut product);
    }
}
