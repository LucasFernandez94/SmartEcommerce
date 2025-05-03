using Order.Domain.Entity;

namespace Order.Aplication.Models
{
    public class OrderModel
    {
        public DateTime OrderDate { get; set; }
        public List<ProductModel> Products { get; set; }
        public CustomerModel Customer { get; set; }
        public decimal PurchaseTotal { get; set; }

        public bool IsValid(out string message) {
            message = null;

            if (this.Products.Count == 0) { message = "No se ingreso ningún Producto"; return false; }
            else {
                foreach (var prod in this.Products)
                {
                    if (!prod.IsValid(out message))
                    {
                        return false;
                    }
                }
            }

            if (!Customer.IsValid(out message)) { return false; }

            return true;
        }
    }
}
