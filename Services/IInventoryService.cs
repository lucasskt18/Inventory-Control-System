using InventorySystem.Models;
using System.Threading.Tasks;

namespace InventorySystem.Services
{
    public interface IInventoryService
    {
        Task RecordInventoryMovementAsync(InventoryMovement movement);
    }
}
