public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class InventoryMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public InventoryMovementType Type { get; set; }
}

public enum InventoryMovementType
{
    Entry,
    Exit
}

public void AddProduct(Product newProduct)
{
    if (ProductExists(newProduct.Name))
    {
        throw new Exception("Product already exists.");
    }

    if (!CategoryExists(newProduct.CategoryId) || !SupplierExists(newProduct.SupplierId))
    {
        throw new Exception("Category or supplier not found.");
    }

    dbContext.Products.Add(newProduct);
    dbContext.SaveChanges();
}

public void UpdateProduct(Product updatedProduct)
{
    var existingProduct = dbContext.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
    if (existingProduct == null)
    {
        throw new Exception("Product not found.");
    }

    if (!CategoryExists(updatedProduct.CategoryId) || !SupplierExists(updatedProduct.SupplierId))
    {
        throw new Exception("Category or supplier not found.");
    }

    existingProduct.Name = updatedProduct.Name;
    existingProduct.Price = updatedProduct.Price;
    existingProduct.QuantityInStock = updatedProduct.QuantityInStock;
    existingProduct.CategoryId = updatedProduct.CategoryId;
    existingProduct.SupplierId = updatedProduct.SupplierId;

    dbContext.SaveChanges();
}

public void RecordInventoryMovement(InventoryMovement movement)
{
    var product = dbContext.Products.FirstOrDefault(p => p.Id == movement.ProductId);
    if (product == null)
    {
        throw new Exception("Product not found.");
    }

    if (movement.Type == InventoryMovementType.Entry)
    {
        if (movement.Quantity <= 0)
        {
            throw new Exception("Invalid entry quantity.");
        }
        product.QuantityInStock += movement.Quantity;
    }
    else if (movement.Type == InventoryMovementType.Exit)
    {
        if (movement.Quantity <= 0 || movement.Quantity > product.QuantityInStock)
        {
            throw new Exception("Invalid exit quantity.");
        }
        product.QuantityInStock -= movement.Quantity;
    }

    dbContext.InventoryMovements.Add(movement);
    dbContext.SaveChanges();
}

private bool ProductExists(string name)
{
    return dbContext.Products.Any(p => p.Name == name);
}

private bool CategoryExists(int categoryId)
{
    return dbContext.Categories.Any(c => c.Id == categoryId);
}

private bool SupplierExists(int supplierId)
{
    return dbContext.Suppliers.Any(s => s.Id == supplierId);
}






