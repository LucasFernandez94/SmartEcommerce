
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entity
{
    [Table("OrderProduct")]
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductoId { get; set; }

        public bool IsValid(out string message) {
            message = string.Empty;
            if (this.OrderId < 0) { message = "el Id de la orden no puede ser menor a 0."; return false; }
            if (this.ProductoId < 0) { message = "el Id del producto no puede ser menor a 0."; return false; }
            return true;
        }
    }
}
