
using System.ComponentModel.DataAnnotations;

namespace Order.Domain.DTO
{
    public class OrderProducts
    {
        public OrderProducts()
        {
            Products = new List<ProductDTO>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<ProductDTO> Products { get; set; }
        public decimal PurchaseTotal { get; set; }
    }
}
