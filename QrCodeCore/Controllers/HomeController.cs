using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QrCodeCore.Models;
using QrCodeCoreApi.DTO.UserDto;
using QrCodeCoreEntities.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace QrCodeCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string User_UserName, string User_Password)
        {
            var authDto = new AuthDto
            {
                UserName = User_UserName,
                Password = User_Password
            };
            var json = JsonConvert.SerializeObject(authDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/auth", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var sessionData = JsonConvert.DeserializeObject<SessionData>(responseData);

                // Oturum bilgilerini saklamak için uygun bir yöntem kullanýn, örneðin HttpContext.Session
                HttpContext.Session.SetString("Business_Name", sessionData.BusinessName);
                HttpContext.Session.SetString("User_FullName", sessionData.UserFullName);
                HttpContext.Session.SetInt32("Business_Id", sessionData.BusinessId);

                // Baþarýlý giriþ
                return RedirectToAction("Index", "Main");
            }
            else
            {
                // Baþarýsýz giriþ
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }
        }
    }
}
