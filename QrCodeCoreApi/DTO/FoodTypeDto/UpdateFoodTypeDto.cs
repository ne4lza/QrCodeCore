namespace QrCodeCoreApi.DTO.FoodTypeDto
{
    public class UpdateFoodTypeDto
    {
        public int FoodType_Id { get; set; }
        public string? FoodType_Name { get; set; }
        public bool FoodType_Status { get; set; }
        public int FoodType_BusinessId { get; set; }
        public DateTime FoodType_CreatedDate { get; set; }
    }
}
