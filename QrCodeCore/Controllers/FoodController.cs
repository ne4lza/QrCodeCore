using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;
using QrCodeCore.ViewModels.Food;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace QrCodeCore.Controllers
{
    public class FoodController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FoodController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7184/api/food/{businessId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Foods> foods = JsonConvert.DeserializeObject<List<Foods>>(content);
                return View(foods);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }
        [HttpGet]
        public ActionResult AddFood()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFood(Foods food)
        {
            // HttpClientFactory'den HttpClient örneği alın
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7184/api/food/");

            // Food nesnesini JSON'a dönüştürün
            var json = JsonConvert.SerializeObject(food);

            // JSON verisini içeren StringContent oluşturun
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // POST isteği gönderin ve yanıtı alın
            var response = await httpClient.PostAsync("AddFood", content);

            // Yanıtı kontrol edin
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Food");
            }
            else
            {
                return View();
            }
        }
        //[HttpGet]
        //public ActionResult UpdateFood(int id)
        //{
        //    int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
        //    var model = _qrCodeCoreDbContext.TBL_FOODS.Where(x => x.Food_Id == id).FirstOrDefault();

        //    return PartialView("_UpdateFoodModal", model);
        //}
        //[HttpPost]
        //public ActionResult UpdateFood(Foods foods)
        //{
        //    int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
        //    foods.Type_Name = "%";
        //    foods.Food_Business = businessId;
        //    _qrCodeCoreDbContext.Update(foods);
        //    _qrCodeCoreDbContext.SaveChanges();
        //    return RedirectToAction(nameof(FoodController.Index));

        //}
    }
}

