using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Service.DTO
{
    public class OrderResponseDTO
    {
        public OrderResponseDTO()
        {
            Products = new List<ProductDTO>();
        }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public int Customer { get; set; }
        [Required]
        public List<ProductDTO> Products { get; set; }
        [Required]
        public decimal PurchaseTotal { get; set; }
    }
}
