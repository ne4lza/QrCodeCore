using System.ComponentModel.DataAnnotations;

namespace QrCodeCore.Models
{
    public class Types
    {
        [Key]
        public int Type_Id { get; set; }
        public string? Type_Name { get; set; }
        public bool Type_Status { get; set; }
    }
}
