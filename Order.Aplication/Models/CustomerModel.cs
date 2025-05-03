
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Order.Aplication.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }

        public bool IsValid(out string message) {
            message = null;

            if (string.IsNullOrEmpty(this.Name)) { message = "El nombre del usuario es obligatorio."; return false; }
            if (string.IsNullOrEmpty(this.Email)) { message = "El mail del usuario es obligatorio."; return false; }
            if (string.IsNullOrEmpty(this.Addres)) { message = "La dirección del usuario es obligatorio."; return false; }
            else
            {
                if (!Regex.IsMatch(this.Email, @"^[\w.-]+@[\w.-]+\.com$"))
                {
                    message = "El mail no contiene el formato correcto.";
                    return false;
                }
            }

            return true;
        }
    }
}
