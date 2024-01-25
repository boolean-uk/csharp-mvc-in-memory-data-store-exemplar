
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
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            var result = repository.DeleteProduct(id);
            return result != null ? TypedResults.Ok(result):Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            if (!repository.GetProducts().Any(x => x.Id==id))
            {
                return Results.NotFound("Product not found");
            }

            var entity = repository.UpdateProduct(id, model);

            if(entity==null)
            {
                return Results.BadRequest("Product with provided name already exists");
            }
            
            return TypedResults.Created($"/{entity.Id}", entity);
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]         
        public static async Task<IResult> GetAllProductsByCategory(IRepository repository, [FromQuery]string? category)
        {
            if(category==null)
            {
                return TypedResults.Ok(repository.GetProducts());
            }            

            if(!repository.GetProducts().Any(x => x.category.Equals(category, StringComparison.OrdinalIgnoreCase)))
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            }
            
            return TypedResults.Ok(repository.GetProducts().Where(x => x.category.Equals(category,StringComparison.OrdinalIgnoreCase)));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]

        public static async Task<IResult> GetAllProducts(IRepository repository)
        {
            return TypedResults.Ok(repository.GetProducts());
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
           
            if(repository.GetProducts().Any(x => x.name.Equals(model.name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }
            var entity = new Product() { name=model.name, price=model.price, category=model.category };
            repository.AddProduct(entity);
            return TypedResults.Created($"/{entity.Id}", entity);
        }

    }
}
