namespace Inventory.Factories;

using Inventory.Models;

/// <summary>
/// Factory for creating products with centralized validation.
/// Generates IDs automatically.
/// </summary>
public static class ProductFactory
{
    private static int _nextId = 1;

    /// <summary>
    /// Creates a validated product with an automatic ID.
    /// </summary>
    public static Product Create(
        string name,
        decimal price,
        int quantity,
        ProductCategory category = ProductCategory.Other)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));

        return new Product
        {
            Id = _nextId++,
            Name = name,
            Price = price,
            Quantity = quantity,
            Category = category,
            Status = ProductStatus.Active,
            RegistrationDate = DateTime.Now
        };
    }

    /// <summary>
    /// Creates a product that requires initial stock greater than zero.
    /// </summary>
    public static Product CreateWithStock(
        string name,
        decimal price,
        int quantity,
        ProductCategory category)
    {
        if (quantity <= 0)
            throw new ArgumentException("CreateWithStock requires quantity greater than zero.", nameof(quantity));

        return Create(name, price, quantity, category);
    }
}
