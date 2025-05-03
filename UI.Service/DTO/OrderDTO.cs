
using System.ComponentModel.DataAnnotations;

namespace UI.Service.DTO
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            Products = new List<ProductDTO>();
        }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public CustomerDTO Customer { get; set; }
        [Required]
        public List<ProductDTO> Products { get; set; }
        [Required]
        public decimal PurchaseTotal { get; set; }
    }
}