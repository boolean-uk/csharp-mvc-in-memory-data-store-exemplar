using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Reflection;

namespace exercise.tests;

public class Tests
{
    private WebApplicationFactory<Program> factory;
    private HttpClient client;
    [SetUp]
    public void Init()
    {
        factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        client = factory.CreateClient();
    }


    [Test]
    public async Task ProductEndpointStatus()
    {
        // Arrange


        // Act
        var response = await client.GetAsync("/products");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }
    [Test]
    public async Task AddProductTest()
    {
        // Arrange
            ProductPost model = new ProductPost() { name = "Banana", category = "Fruit", price = 100 };
        string json = JsonConvert.SerializeObject(model);
        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        // Act
        var response = await client.PostAsync("/products", httpContent);

        // Assert
        Assert.IsTrue(response.StatusCode==System.Net.HttpStatusCode.Created);
    }
    [Test]
    public async Task AddDuplicateProductTest()
    {
        // Arrange
       
        ProductPost model = new ProductPost() { name = "Banana", category = "Fruit", price = 100 };
        string json = JsonConvert.SerializeObject(model);
        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Act
        var responseFirst = await client.PostAsync("/products", httpContent);
        var responseSecond = await client.PostAsync("/products", httpContent);

        // Assert
        Assert.That(responseSecond.StatusCode == (System.Net.HttpStatusCode.BadRequest));
    }
    [Test]
    public async Task DeleteProductTest()
    {
        // Arrange
       
        ProductPost model = new ProductPost() { name = "Banana", category = "Fruit", price = 100 };
        string json = JsonConvert.SerializeObject(model);
        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Act
        var responseFirst = await client.PostAsync("/products", httpContent);
        var responseSecond = await client.DeleteAsync("/products/1");

        // Assert
        Assert.That(responseSecond.StatusCode == (System.Net.HttpStatusCode.OK));
    }
}