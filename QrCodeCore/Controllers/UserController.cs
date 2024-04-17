using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace QrCodeCore.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7184/api/sysusers/{businessId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<SysUsers> users = JsonConvert.DeserializeObject<List<SysUsers>>(content);
                return View(users);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }

        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(SysUsers user)
        {
            user.User_FullName = user.User_Name + " " + user.User_LastName;
            user.User_Status = true;
            user.User_BusinessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/sysusers/AddSysUser", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7184/api/sysusers/SysUserId/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                SysUsers user = JsonConvert.DeserializeObject<SysUsers>(content);
                return View(user);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(SysUsers user)
        {
            user.User_Status = true;
            user.User_FullName = user.User_Name + " "+ user.User_LastName;
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/sysusers/UpdateSysUser", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7184/api/sysusers/RemoveSysUser/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View();
            }

        }
    }
}
