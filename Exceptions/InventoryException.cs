using System;

namespace InventorySystem.Exceptions
{
    public class InventoryException : Exception
    {
        public InventoryException(string message) : base(message) { }
    }
}
