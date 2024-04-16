using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QrCodeCore.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace QrCodeCore.Controllers
{
    public class FoodTypeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FoodTypeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7184/api/foodType/{businessId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<FoodTypes> foodTypes = JsonConvert.DeserializeObject<List<FoodTypes>>(content);
                return View(foodTypes);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }
        [HttpGet]
        public  IActionResult AddFoodType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFoodType(FoodTypes foodTypes)
        {
            foodTypes.FoodType_BusinessId = (int)HttpContext.Session.GetInt32("Business_Id");
            foodTypes.FoodType_CreatedDate = DateTime.Now;
            foodTypes.FoodType_Status = true;
            var json = JsonConvert.SerializeObject(foodTypes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/foodType/AddFoodType", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FoodType");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFoodType(int id)
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7184/api/foodType/FoodTypeId/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                FoodTypes foodTypes = JsonConvert.DeserializeObject<FoodTypes>(content);
                return View(foodTypes);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFoodType(FoodTypes foodTypes)
        {
            var json = JsonConvert.SerializeObject(foodTypes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/foodType/UpdateFoodType", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FoodType");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> RemoveFoodType(int id)
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7184/api/foodType/RemoveFoodType/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FoodType");
            }
            else
            {
                return View();
            }

        }
    }
}
