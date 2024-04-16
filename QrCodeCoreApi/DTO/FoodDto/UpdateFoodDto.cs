namespace QrCodeCoreApi.DTO.FoodDto
{
    public class UpdateFoodDto
    {
        public int Food_Id { get; set; }
        public string? Food_Name { get; set; }
        public string? Food_Price { get; set; }
        public DateTime Food_CreatedDate { get; set; }
        public int Food_Type { get; set; }
        public int Food_Business { get; set; }
        public string? Food_Description { get; set; }
        public string? Food_Photo { get; set; }
    }
}
