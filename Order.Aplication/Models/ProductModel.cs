namespace Order.Aplication.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? MessageError { get; set; }

        public bool IsValid(out string message) {
            message = null;

            if (String.IsNullOrEmpty(this.Name)) { message = "EL nombre del producto el obligatorio."; return false; }
            if (String.IsNullOrEmpty(this.Description)) { message = "La descripción del producto el obligatorio."; return false; }
            if (this.Price < 0 ) { message = "El prcio del producto el obligatorio."; return false; }

            return true;
        }
    }
}
