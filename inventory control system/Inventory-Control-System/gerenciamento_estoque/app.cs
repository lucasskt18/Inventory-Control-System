public interface IProductService
{
    Task AddProductAsync(Product newProduct);
    Task UpdateProductAsync(Product updatedProduct);
}

public interface IInventoryService
{
    Task RecordInventoryMovementAsync(InventoryMovement movement);
}

public class ProductService : IProductService
{
    private readonly IDbContext dbContext;

    public ProductService(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddProductAsync(Product newProduct)
    {
        if (await ProductExistsAsync(newProduct.Name))
        {
            throw new ProductAlreadyExistsException("Product already exists.");
        }

        await EnsureCategoryAndSupplierExistAsync(newProduct.CategoryId, newProduct.SupplierId);

        dbContext.Products.Add(newProduct);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product updatedProduct)
    {
        var existingProduct = await dbContext.Products.FindAsync(updatedProduct.Id);
        if (existingProduct == null)
        {
            throw new ProductNotFoundException("Product not found.");
        }

        await EnsureCategoryAndSupplierExistAsync(updatedProduct.CategoryId, updatedProduct.SupplierId);

        // Update product properties
        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.QuantityInStock = updatedProduct.QuantityInStock;
        existingProduct.CategoryId = updatedProduct.CategoryId;
        existingProduct.SupplierId = updatedProduct.SupplierId;

        await dbContext.SaveChangesAsync();
    }

    private async Task EnsureCategoryAndSupplierExistAsync(int categoryId, int supplierId)
    {
        if (!await CategoryExistsAsync(categoryId) || !await SupplierExistsAsync(supplierId))
        {
            throw new CategoryOrSupplierNotFoundException("Category or supplier not found.");
        }
    }

    private async Task<bool> ProductExistsAsync(string name)
    {
        return await dbContext.Products.AnyAsync(p => p.Name == name);
    }

    private async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await dbContext.Categories.AnyAsync(c => c.Id == categoryId);
    }

    private async Task<bool> SupplierExistsAsync(int supplierId)
    {
        return await dbContext.Suppliers.AnyAsync(s => s.Id == supplierId);
    }
}

public class InventoryService : IInventoryService
{
    private readonly IDbContext dbContext;

    public InventoryService(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task RecordInventoryMovementAsync(InventoryMovement movement)
    {
        var product = await dbContext.Products.FindAsync(movement.ProductId);
        if (product == null)
        {
            throw new ProductNotFoundException("Product not found.");
        }

        if (movement.Type == InventoryMovementType.Entry)
        {
            if (movement.Quantity <= 0)
            {
                throw new InvalidEntryQuantityException("Invalid entry quantity.");
            }

            product.QuantityInStock += movement.Quantity;
        }
        else if (movement.Type == InventoryMovementType.Exit)
        {
            if (movement.Quantity <= 0 || movement.Quantity > product.QuantityInStock)
            {
                throw new InvalidExitQuantityException("Invalid exit quantity.");
            }

            product.QuantityInStock -= movement.Quantity;
        }

        dbContext.InventoryMovements.Add(movement);
        await dbContext.SaveChangesAsync();
    }
}

public class ProductAlreadyExistsException : Exception
{
    public ProductAlreadyExistsException(string message) : base(message)
    {
    }
}

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string message) : base(message)
    {
    }
}

public class CategoryOrSupplierNotFoundException : Exception
{
    public CategoryOrSupplierNotFoundException(string message) : base(message)
    {
    }
}

public class InvalidEntryQuantityException : Exception
{
    public InvalidEntryQuantityException(string message) : base(message)
    {
    }
}

public class InvalidExitQuantityException : Exception
{
    public InvalidExitQuantityException(string message) : base(message)
    {
    }
}
