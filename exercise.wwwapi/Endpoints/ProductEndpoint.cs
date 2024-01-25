
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            //products.MapGet("/", GetAllProducts);
            products.MapGet("/", GetAllProductsByCategory);
            products.MapPost("/", AddProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id)
        {
            var result = repository.Delete(id);
            return result != null ? TypedResults.Ok(result):Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IRepository<Product> repository, int id, ProductPut model)
        {
            throw new NotImplementedException();

            //var target = repository.GetById(id);
            //if (target==null)
            //{
            //    return Results.NotFound("Product not found");
            //}

            //var entity = repository.Update(id, model);

            //if(entity==null)
            //{
            //    return Results.BadRequest("Product with provided name already exists");
            //}
            
            //return TypedResults.Created($"/{entity.Id}", entity);
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]         
        public static async Task<IResult> GetAllProductsByCategory(IRepository<Product> repository, [FromQuery]string? category)
        {
            if (category == null)
            {
                return TypedResults.Ok(repository.Get().ToList());
            }

            if (!repository.Get().Any(x => x.category.Equals(category, StringComparison.OrdinalIgnoreCase)))
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            }

            return TypedResults.Ok(repository.Get().Where(x => x.category.Equals(category, StringComparison.OrdinalIgnoreCase)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public static async Task<IResult> GetAllProducts(IRepository<Product> repository)
        {
            return TypedResults.Ok(repository.Get());
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct(IRepository<Product> repository, ProductPost model)
        {
           
            if(repository.Get().Any(x => x.name.Equals(model.name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }
            var entity = new Product() { name=model.name, price=model.price, category=model.category };
            repository.Insert(entity);
            return TypedResults.Created($"/{entity.Id}", entity);
        }

    }
}
