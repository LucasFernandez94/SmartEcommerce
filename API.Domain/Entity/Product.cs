using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public bool IsValid(out string message) {
            message = string.Empty;
            if (this.Id <= 0) { message = "El Id es obligatorio."; return false; }      
            if (string.IsNullOrEmpty(this.Name)) { message = "El Nombre es obligatorio."; return false; }      
            if (string.IsNullOrEmpty(this.Description)) { message = "La descripción es obligatoria."; return false; }      
            if (this.Price <= 0) { message = "El Precio es obligatorio."; return false; }      
            if (this.Stock <= 0) { message = "El Stock no puede ser menor a 0."; return false; }
            return true;
            
        }
    }
}
