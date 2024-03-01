using InventorySystem.Models;
using System.Threading.Tasks;

namespace InventorySystem.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product newProduct);
        Task UpdateProductAsync(Product updatedProduct);
    }
}
