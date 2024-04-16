﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;
using QrCodeCore.ViewModels.Food;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using QrCodeCoreApi.DTO.UserDto;
using QrCodeCoreApi.DTO.FoodDto;
using NuGet.Packaging.Signing;

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
        public async Task<IActionResult> AddFood()
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
        [HttpPost]
        public async Task<IActionResult> AddFood(Foods food)
        {
            food.Food_Business = (int)HttpContext.Session.GetInt32("Business_Id");
            food.Food_CreatedDate = DateTime.Now;
            var json = JsonConvert.SerializeObject(food);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/food/AddFood", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Food");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFood(int id)
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7184/api/food/FoodId/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Foods foods = JsonConvert.DeserializeObject<Foods>(content);
                return View(foods);
            }
            else
            {
                // İstek başarısız olduysa burada bir hata işleme mekanizması ekleyebilirsiniz
                throw new HttpRequestException("Error: Unable to retrieve foods");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFood(Foods foods)
        {
            foods.Food_Business = (int)HttpContext.Session.GetInt32("Business_Id");
            var json = JsonConvert.SerializeObject(foods);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:7184/api/food/UpdateFood", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Food");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> RemoveFood(int id)
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7184/api/food/RemoveFood/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Food");
            }
            else
            {
                return View();
            }

        }
    }
}

