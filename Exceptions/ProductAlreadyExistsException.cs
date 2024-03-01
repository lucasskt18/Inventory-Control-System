namespace InventorySystem.Exceptions
{
    public class ProductAlreadyExistsException : InventoryException
    {
        public ProductAlreadyExistsException(string message) : base(message) { }
    }
}
