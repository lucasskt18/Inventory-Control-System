namespace InventorySystem.Models
{
    public enum InventoryMovementType { Entry, Exit }

    public class InventoryMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public InventoryMovementType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        // Relacionamentos (dependendo do ORM)
        public Product Product { get; set; }
    }
}
