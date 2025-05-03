using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer.Domain.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Addres { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}