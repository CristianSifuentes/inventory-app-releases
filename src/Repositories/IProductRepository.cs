namespace Inventory.Repositories;

using Inventory.Models;

/// <summary>
/// Contract for the product repository.
/// Defines the basic storage operations.
/// </summary>
public interface IProductRepository
{
    /// <summary>Adds a product to the repository.</summary>
    void Add(Product product);

    /// <summary>Gets a product by its ID.</summary>
    Product? GetById(int id);

    /// <summary>Gets all products.</summary>
    IEnumerable<Product> GetAll();

    /// <summary>Updates an existing product.</summary>
    bool Update(Product product);

    /// <summary>Deletes a product by its ID.</summary>
    bool Delete(int id);

    /// <summary>Total product count.</summary>
    int Count { get; }
}
