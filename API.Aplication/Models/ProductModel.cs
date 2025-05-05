namespace API.Aplication.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public bool IsValid(out string message) {
            message = string.Empty;
            if (string.IsNullOrEmpty(this.Name)) { message = "El nombre es obligatorio."; return false; }
            if (string.IsNullOrEmpty(this.Description)) { message = "La descripción es obligatorio."; return false; }
            if (this.Price <= 0) { message = "El Precio es obligatorio."; return false; }
            if (this.Stock <= 0) { message = "El Stock nok puede ser menor a 0."; return false; }
            return true;
        }
    }
}
