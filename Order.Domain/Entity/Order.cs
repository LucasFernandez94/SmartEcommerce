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

        public bool IsValid(out string message) {
            message = string.Empty;
            if (this.Id < 0) { message = "El id no puede ser menor a 0."; return false; }
            if (this.Customer < 0) { message = "El id del usuario no puede ser menor a 0."; return false; }
            if (this.PurchaseTotal < 0) { message = "El Precio final no puede ser menor a 0."; return false; }
            foreach (var product in this.Products)
            {
                if (!product.IsValid(out message))
                {
                    return false;
                }
            }
            return true;
        }
    }
}