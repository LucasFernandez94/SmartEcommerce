
namespace Customer.Aplication.Models
{
    public class CustomerModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }
        public DateTime Created { get; set; }

        public bool IsValid(out string message) {
            message = string.Empty;
            if (string.IsNullOrEmpty(this.Name)) { message = "El nombre es obligatorio."; return false; }
            if (string.IsNullOrEmpty(this.Email)) { message = "El Email es obligatorio."; return false; }
            if (string.IsNullOrEmpty(this.Addres)) { message = "La dirección es obligatorio."; return false; }
            return true;
        }
    }
}
