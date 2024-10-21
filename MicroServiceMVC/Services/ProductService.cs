
using MicroServiceMVC.Models;
using System.Net.Http.Headers;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Set your actual API base URL
        _httpClient.BaseAddress = new Uri("http://ec2-50-18-232-183.us-west-1.compute.amazonaws.com:5001/api/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task CreateProductAsync(Product product)
    {
        // Send a POST request to the "products" endpoint with the product data
        var response = await _httpClient.PostAsJsonAsync("products", product);

        // Check if the request was successful
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error creating product: {response.StatusCode}");
        }
    }

    // Additional methods for retrieving, updating, and deleting products can be added here
}
