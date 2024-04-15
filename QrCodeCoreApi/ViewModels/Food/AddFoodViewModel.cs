using QrCodeCore.Models;
namespace QrCodeCore.ViewModels.Food
{
    public class AddFoodViewModel
    {
        public List<FoodTypes> foodTypeList {  get; set; }
        public List<Foods> foods { get; set; }
        public Foods selectedFood { get; set; }
    }
}
