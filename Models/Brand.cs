using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public HashSet<Model> Models { get; set; }

        public Brand()
        {
            Models = new HashSet<Model>();
        }
    }
}
