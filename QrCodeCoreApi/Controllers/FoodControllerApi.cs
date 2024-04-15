using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;

namespace QrCodeCoreApi.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodControllerApi : ControllerBase
    {
        private readonly QrCodeCoreDbContext _qrCodeCoreDbContext;

        public FoodControllerApi(QrCodeCoreDbContext qrCodeCoreDbContext)
        {
            _qrCodeCoreDbContext = qrCodeCoreDbContext;
        }
        [HttpGet("{business}")]
        public ActionResult<List<Foods>> GetFoods(int business)
        {
            int businessId = business;

            var result = (from p in _qrCodeCoreDbContext.TBL_FOODS
                          join c in _qrCodeCoreDbContext.TBL_FOOD_TYPES on p.Food_Type equals c.FoodType_Id
                          where c.FoodType_BusinessId == businessId
                          select new Foods
                          {
                              Food_Id = p.Food_Id,
                              Food_Name = p.Food_Name,
                              Food_Price = p.Food_Price,
                              Food_Description = p.Food_Description,
                              Food_CreatedDate = p.Food_CreatedDate,
                              Food_Photo = p.Food_Photo,
                              Type_Name = c.FoodType_Name
                          })
                          .OrderByDescending(x => x.Food_Id)
                          .ToList();

            return Ok(result);
        }
        [HttpPost("AddFood")]
        public ActionResult AddFood(Foods food)
        {
            food.Food_Business = (int)HttpContext.Session.GetInt32("Business_Id");
            food.Food_CreatedDate = DateTime.Now;
            _qrCodeCoreDbContext.TBL_FOODS.Add(food);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }
    }
}
