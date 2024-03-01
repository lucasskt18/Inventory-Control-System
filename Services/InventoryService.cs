using InventorySystem.Models;
using InventorySystem.DbContext;
using System.Threading.Tasks;
using System; // Para ArgumentNullException

namespace InventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IDbContext _dbContext;

        public InventoryService(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task RecordInventoryMovementAsync(InventoryMovement movement)
        {
            /// Falta implementar
        }
    }
}
