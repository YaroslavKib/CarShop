using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        public Brand Brand { get; set; }
    }
}
