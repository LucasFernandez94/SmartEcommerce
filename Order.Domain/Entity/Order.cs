using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entity
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            Products = new List<OrderProduct>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public int Customer { get; set; }
        public List<OrderProduct> Products { get; set; }
        public decimal PurchaseTotal { get; set; }
    }
}