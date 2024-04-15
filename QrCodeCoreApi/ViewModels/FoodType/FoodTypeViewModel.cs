using QrCodeCore.Models;

namespace QrCodeCore.ViewModels.FoodType
{
    public class FoodTypeViewModel
    {
        public FoodTypes selectedFoodType { get; set; }
        public List<FoodTypes> foodTypeList { get; set; }
    }
}
