namespace Inventory.Repositories;

using Inventory.Models;

/// <summary>
/// In-memory implementation of the product repository.
/// Uses a dictionary for O(1) access by ID.
/// Includes LINQ methods for searches and aggregations.
/// </summary>
public class InMemoryProductRepository : IProductRepository
{
    private readonly Dictionary<int, Product> _products = new();

    public int Count => _products.Count;

    // ══════════════════════════════════════════════════════════════════
    // BASIC CRUD
    // ══════════════════════════════════════════════════════════════════

    public void Add(Product product)
    {
        _products[product.Id] = product;
    }

    public Product? GetById(int id)
    {
        return _products.GetValueOrDefault(id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.Values;
    }

    public bool Update(Product product)
    {
        if (!_products.ContainsKey(product.Id))
            return false;

        _products[product.Id] = product;
        return true;
    }

    public bool Delete(int id)
    {
        return _products.Remove(id);
    }

    // ══════════════════════════════════════════════════════════════════
    // LINQ SEARCHES
    // ══════════════════════════════════════════════════════════════════

    public IEnumerable<Product> FindByCategory(ProductCategory category)
    {
        return _products.Values.Where(product => product.Category == category);
    }

    public IEnumerable<Product> FindByName(string name)
    {
        return _products.Values
            .Where(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Product> FindByPriceRange(decimal min, decimal max)
    {
        return _products.Values
            .Where(product => product.Price >= min && product.Price <= max);
    }

    public IEnumerable<string> GetNames()
    {
        return _products.Values.Select(product => product.Name);
    }

    public bool HasLowStock(int minimum = 5)
    {
        return _products.Values.Any(product => product.Quantity < minimum);
    }

    // ══════════════════════════════════════════════════════════════════
    // ADVANCED LINQ
    // ══════════════════════════════════════════════════════════════════

    public IEnumerable<Product> GetOrderedByPrice(bool descending = false)
    {
        return descending
            ? _products.Values.OrderByDescending(product => product.Price)
            : _products.Values.OrderBy(product => product.Price);
    }

    public IEnumerable<Product> GetTopByPrice(int count = 5)
    {
        return _products.Values
            .OrderByDescending(product => product.Price)
            .Take(count);
    }

    public Dictionary<ProductCategory, List<Product>> GroupByCategory()
    {
        return _products.Values
            .GroupBy(product => product.Category)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public Dictionary<ProductCategory, int> CountByCategory()
    {
        return _products.Values
            .GroupBy(product => product.Category)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public decimal GetTotalInventoryValue()
    {
        return _products.Values.Sum(product => product.Price * product.Quantity);
    }

    public decimal GetAveragePrice()
    {
        return _products.Values.Any()
            ? _products.Values.Average(product => product.Price)
            : 0;
    }

    public Product? GetMostExpensiveProduct()
    {
        return _products.Values
            .OrderByDescending(product => product.Price)
            .FirstOrDefault();
    }

    public Dictionary<ProductCategory, decimal> GetValueByCategory()
    {
        return _products.Values
            .GroupBy(product => product.Category)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(product => product.Price * product.Quantity)
            );
    }

    public IEnumerable<Product> GetLowStock(int minimum = 5)
    {
        return _products.Values
            .Where(product => product.Quantity < minimum)
            .OrderBy(product => product.Quantity);
    }
}
