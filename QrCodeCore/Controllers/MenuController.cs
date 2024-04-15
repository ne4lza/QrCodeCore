using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;
using QrCodeCore.ViewModels.FoodType;

namespace QrCodeCore.Controllers
{
    public class MenuController : Controller
    {
        private readonly QrCodeCoreDbContext _qrCodeCoreDbContext;

        public MenuController(QrCodeCoreDbContext qrCodeCoreDbContext)
        {
            _qrCodeCoreDbContext = qrCodeCoreDbContext;
        }

        public ActionResult Index()
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            FoodTypeViewModel foodTypeViewModel = new FoodTypeViewModel()
            {
                foodTypeList = _qrCodeCoreDbContext.TBL_FOOD_TYPES.Where(x => x.FoodType_BusinessId == businessId).ToList(),

            };
            return View(foodTypeViewModel);
        }
        [HttpGet]
        public ActionResult AddFoodType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFoodType(FoodTypes foodType)
        {
            foodType.FoodType_Status = true;
            foodType.FoodType_BusinessId =(int)HttpContext.Session.GetInt32("Business_Id");
            foodType.FoodType_CreatedDate = DateTime.Now;
            if (foodType.FoodType_Name == null)
            {
                ViewBag.error = "Lütfen Bir Yemek Türü Giriniz";
                return View();
            }
            _qrCodeCoreDbContext.TBL_FOOD_TYPES.Add(foodType);
            _qrCodeCoreDbContext.SaveChanges();
            return RedirectToAction(nameof(MenuController.Index));
        }
        [HttpGet]
        public ActionResult UpdateFoodType(int id)
        {
            var model = _qrCodeCoreDbContext.TBL_FOOD_TYPES.Where(x => x.FoodType_Id == id).FirstOrDefault();
            return PartialView("_UpdateModal", model);
        }
        [HttpPost]
        public ActionResult UpdateFoodType(FoodTypes foodType)
        {
            if (foodType.FoodType_Status)
            {
                foodType.FoodType_Status = true;
            }
            else
            {
                foodType.FoodType_Status = false;
            }
            _qrCodeCoreDbContext.Update(foodType);
            _qrCodeCoreDbContext.SaveChanges();
            return RedirectToAction(nameof(MenuController.Index));
        }
        public ActionResult UpdateFoodTypeStatus(int id)
        {
            var model = _qrCodeCoreDbContext.TBL_FOOD_TYPES.Where(x => x.FoodType_Id == id).FirstOrDefault();
            if (model.FoodType_Status)
            {
                model.FoodType_Status = false;

            }
            else
            {
                model.FoodType_Status = true;
            }
            _qrCodeCoreDbContext.Update(model);
            _qrCodeCoreDbContext.SaveChanges();
            return RedirectToAction(nameof(MenuController.Index));
        }
    }
}
