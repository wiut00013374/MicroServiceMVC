using MicroServiceMVC.Models; // Adjust based on your actual namespace
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MicroServiceMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor
        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://ec2-50-18-232-183.us-west-1.compute.amazonaws.com:5001/"); // Set your API base URL
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(data);
                return View(products);
            }
            return View(new List<Product>());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(data);
                return View(product);
            }
            return NotFound();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/products", product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(data);
                return View(product);
            }
            return NotFound();
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(data);
                return View(product);
            }
            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            // Optionally log the error or show an error message
            ModelState.AddModelError("", "Unable to delete product. Please try again.");
            return NotFound();
        }

    }
}
