using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Reflection;

namespace exercise.tests;

public class Tests
{
    [Test]
    public async Task ProductEndpointStatus()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/products");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }
    [Test]
    public async Task AddProductTest()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        ProductPost model = new ProductPost() { name = "Banana", category = "Fruit", price = 100 };
        string json = JsonConvert.SerializeObject(model);
        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/products", httpContent);

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created);
    }
}