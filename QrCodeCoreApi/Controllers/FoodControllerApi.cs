using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;
using QrCodeCoreApi.DTO.FoodDto;

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
        public ActionResult AddFood(AddFoodDto addFoodDto)
        {
            Foods food = new Foods()
            {
                Food_Business = addFoodDto.Food_Business,
                Food_CreatedDate = DateTime.Now,
                Food_Description = addFoodDto.Food_Description,
                Food_Type = addFoodDto.Food_Type,
                Food_Name = addFoodDto.Food_Name,
                Food_Photo = addFoodDto.Food_Photo,
                Food_Price = addFoodDto.Food_Price,

            };
            _qrCodeCoreDbContext.TBL_FOODS.Add(food);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }
        [HttpGet("FoodId/{foodId}")]
        public Foods GetFoodsById(int foodId)
        {
            var query = _qrCodeCoreDbContext.TBL_FOODS.Where(
                x => x.Food_Id == foodId
                ).FirstOrDefault();

            return query;
        }
        [HttpPost("UpdateFood")]
        public ActionResult UpdateFood(UpdateFoodDto updateFoodDto)
        {
            Foods food = new Foods()
            {
                Food_Id = updateFoodDto.Food_Id,
                Food_Business = updateFoodDto.Food_Business,
                Food_CreatedDate = updateFoodDto.Food_CreatedDate,
                Food_Description = updateFoodDto.Food_Description,
                Food_Type = updateFoodDto.Food_Type,
                Food_Name = updateFoodDto.Food_Name,
                Food_Photo = updateFoodDto.Food_Photo,
                Food_Price = updateFoodDto.Food_Price,

            };
            _qrCodeCoreDbContext.TBL_FOODS.Update(food);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }
        [HttpGet("RemoveFood/{id}")]
        public ActionResult RemoveFood(int id)
        {
            var food = _qrCodeCoreDbContext.TBL_FOODS.Where(x=>x.Food_Id == id).FirstOrDefault();
            _qrCodeCoreDbContext.TBL_FOODS.Remove(food);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();
        }
    }
}
