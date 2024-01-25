using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Connections;
using System.Linq;

namespace exercise.wwwapi.Repository
{
    public class Repository : IRepository
    {
       private DataContext _dataContext;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Product AddProduct(Product product)
        {
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
            return product;
           
        }

        public Product DeleteProduct(int id)
        {
            var entity = _dataContext.Products.FirstOrDefault(x => x.Id == id);
            if(entity==null)
            {
                return null;
            }
            _dataContext.Products.Remove(entity);
            _dataContext.SaveChanges();
            return entity;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dataContext.Products;
        }

        public Product UpdateProduct(int id, ProductPut model)
        {
            var entity = _dataContext.Products.FirstOrDefault(x => x.Id == id);
            if (model.name != null)
            {
                if(_dataContext.Products.Any(x=> x.Id!=id && x.name==model.name))
                {
                    entity.name = model.name != null ? model.name : entity.name;

                }
            }
            

            entity.price = model.price != null ? (int) model.price : entity.price;
            entity.category = model.category != null ? model.category : entity.category;
            
            return entity;
        }
    }
}
