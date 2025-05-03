using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class CustomerModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Addres { get; set; }
    }
}
