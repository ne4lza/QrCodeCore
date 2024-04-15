using System.ComponentModel.DataAnnotations;

namespace QrCodeCore.Models
{
    public class Business
    {
        [Key]
        public int Business_Id { get; set; }
        public string? Business_Name { get; set; }
        public int Business_Type { get; set; }
        public string? Business_Address { get; set; }
        public DateTime Business_CreatedDate { get; set; }
        public bool Business_Status { get; set; }
        public string? Business_Photo { get; set; }
    }
}
