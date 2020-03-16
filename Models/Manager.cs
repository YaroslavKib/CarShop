using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string UserId { get; set; }        
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
    }
}
