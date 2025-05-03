using System.ComponentModel.DataAnnotations;

namespace Order.Domain.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }
        public DateTime Created { get; set; }
    }
}
