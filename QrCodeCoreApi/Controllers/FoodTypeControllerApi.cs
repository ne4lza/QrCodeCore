using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;
using QrCodeCoreApi.DTO.FoodDto;
using QrCodeCoreApi.DTO.FoodTypeDto;

namespace QrCodeCoreApi.Controllers
{
    [Route("api/foodType")]
    [ApiController]
    public class FoodTypeControllerApi : ControllerBase
    {
        private readonly QrCodeCoreDbContext _qrCodeCoreDbContext;

        public FoodTypeControllerApi(QrCodeCoreDbContext qrCodeCoreDbContext)
        {
            _qrCodeCoreDbContext = qrCodeCoreDbContext;
        }

        [HttpGet("{business}")]
        public List<FoodTypes> GetFoodTypes(int business)
        {
            int businessId = business;
            var result = _qrCodeCoreDbContext.TBL_FOOD_TYPES.Where(x => x.FoodType_BusinessId == businessId).ToList();
            return result;
        }
        [HttpPost("AddFoodType")]
        public ActionResult AddFoodType(AddFoodTypeDto addFoodTypeDto)
        {
            FoodTypes foodType = new FoodTypes()
            {
                FoodType_BusinessId = addFoodTypeDto.FoodType_BusinessId,
                FoodType_CreatedDate = addFoodTypeDto.FoodType_CreatedDate,
                FoodType_Name = addFoodTypeDto.FoodType_Name,
                FoodType_Status = addFoodTypeDto.FoodType_Status

            };
            _qrCodeCoreDbContext.TBL_FOOD_TYPES.Add(foodType);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }

        [HttpGet("FoodTypeId/{foodTypeId}")]
        public FoodTypes GetFoodTypeById(int foodTypeId)
        {
            var query = _qrCodeCoreDbContext.TBL_FOOD_TYPES.Where(
                x => x.FoodType_Id == foodTypeId
                ).FirstOrDefault();

            return query;
        }

        [HttpPost("UpdateFoodType")]
        public ActionResult UpdateFoodType(UpdateFoodTypeDto updateFoodTypeDto)
        {
            FoodTypes foodTypes = new FoodTypes 
            {
                FoodType_Id = updateFoodTypeDto.FoodType_Id,
                FoodType_BusinessId = updateFoodTypeDto.FoodType_BusinessId,
                FoodType_CreatedDate = updateFoodTypeDto.FoodType_CreatedDate,
                FoodType_Name = updateFoodTypeDto.FoodType_Name,
                FoodType_Status = updateFoodTypeDto.FoodType_Status
            };

            _qrCodeCoreDbContext.TBL_FOOD_TYPES.Update(foodTypes);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }
        [HttpGet("RemoveFoodType/{id}")]
        public ActionResult RemoveFoodType(int id)
        {
            var foodType = _qrCodeCoreDbContext.TBL_FOOD_TYPES.Where(x => x.FoodType_Id == id).FirstOrDefault();
            _qrCodeCoreDbContext.TBL_FOOD_TYPES.Remove(foodType);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();
        }
    }
}
