
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
    }
}
