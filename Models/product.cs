namespace InventorySystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        // Método para atualizar propriedades do produto
        public void UpdateFrom(Product updatedProduct)
        {
            Name = updatedProduct.Name;
            Price = updatedProduct.Price;
            QuantityInStock = updatedProduct.QuantityInStock;
            CategoryId = updatedProduct.CategoryId;
            SupplierId = updatedProduct.SupplierId;
        }
    }
}
