using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using CustomerManagementMVC.Models;

namespace CustomerManagementMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CustomerMangementApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("api/customers");
                return View(customers);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception (not shown here)
                return View("Error", new ErrorViewModel { Message = "Error retrieving customers" });
            }
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("api/customers", customer);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
               ModelState.AddModelError(string.Empty, "Error creating customer");
                return View(customer);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/customers/{id}");
                return View(customer);
            }
            catch (HttpRequestException ex)
            {
                return View("Error", new ErrorViewModel { Message = "Error retrieving customer" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {

            try
            {
                await _httpClient.PutAsJsonAsync($"api/customers/{customer.Id}", customer);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, "Error updating customer");
                return View(customer);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await _httpClient.GetFromJsonAsync<Customer>($"api/customers/{id}");
                return View(customer);
            }
            catch (HttpRequestException ex)
            {
                return View("Error", new ErrorViewModel { Message = "Error retrieving customer" });
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/customers/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
               return View("Error", new ErrorViewModel { Message = "Error deleting customer" });
            }
        }
    }


}

