using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrCodeCore.Models
{
    public class Foods
    {
        [Key]
        public int Food_Id { get; set; }
        public string? Food_Name { get; set; }
        public string? Food_Price { get; set; }
        public DateTime Food_CreatedDate { get; set; }
        public int Food_Type { get; set; }
        public int Food_Business { get; set; }
        public string? Food_Description { get; set; }
        public string? Food_Photo { get; set; }

        [NotMapped]
        public string Type_Name { get; set; }
    }
}
