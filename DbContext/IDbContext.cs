using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;

namespace InventorySystem.DbContext
{
    public interface IDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<InventoryMovement> InventoryMovements { get; set; }

        // Métodos adicionais para operações do DbContext, como SaveChangesAsync
    }
}
