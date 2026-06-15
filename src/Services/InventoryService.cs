namespace Inventory.Services;

using Inventory.Factories;
using Inventory.Infrastructure;
using Inventory.Models;
using Inventory.Repositories;

/// <summary>
/// Facade service that orchestrates inventory logic.
/// Integrates repository storage, disk persistence, product creation, and reports.
/// </summary>
public class InventoryService
{
    private readonly InMemoryProductRepository _repository;
    private readonly JsonInventoryStorage _storage;
    private readonly string _filePath;

    public InventoryService(string filePath = "inventory.json")
    {
        _repository = new InMemoryProductRepository();
        _storage = new JsonInventoryStorage();
        _filePath = filePath;

        LoadInventory();
    }

    // ══════════════════════════════════════════════════════════════════
    // PRIVATE METHODS
    // ══════════════════════════════════════════════════════════════════

    private void LoadInventory()
    {
        var products = _storage.Load(_filePath);
        foreach (var product in products)
        {
            _repository.Add(product);
        }

        if (products.Count > 0)
        {
            Console.WriteLine($"Loaded {products.Count} products from {_filePath}");
        }
    }

    private void Persist()
    {
        _storage.CreateBackup(_filePath);
        var products = _repository.GetAll().ToList();
        _storage.Save(products, _filePath);
    }

    // ══════════════════════════════════════════════════════════════════
    // CRUD OPERATIONS
    // ══════════════════════════════════════════════════════════════════

    public void AddProduct(string name, decimal price, int quantity, ProductCategory category)
    {
        var product = ProductFactory.Create(name, price, quantity, category);
        _repository.Add(product);
        Persist();
    }

    public IEnumerable<Product> GetAll()
    {
        return _repository.GetAll();
    }

    public Product? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public bool Update(int id, string name, decimal price, int quantity, ProductCategory category)
    {
        var product = _repository.GetById(id);
        if (product == null) return false;

        product.Name = name;
        product.Price = price;
        product.Quantity = quantity;
        product.Category = category;

        _repository.Update(product);
        Persist();
        return true;
    }

    public bool Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (deleted)
        {
            Persist();
        }
        return deleted;
    }

    // ══════════════════════════════════════════════════════════════════
    // SEARCHES
    // ══════════════════════════════════════════════════════════════════

    public IEnumerable<Product> FindByCategory(ProductCategory category)
    {
        return _repository.FindByCategory(category);
    }

    public IEnumerable<Product> FindByName(string name)
    {
        return _repository.FindByName(name);
    }

    public IEnumerable<Product> GetLowStock(int minimum = 5)
    {
        return _repository.GetLowStock(minimum);
    }

    // ══════════════════════════════════════════════════════════════════
    // STATISTICS
    // ══════════════════════════════════════════════════════════════════

    public decimal GetTotalInventoryValue()
    {
        return _repository.GetTotalInventoryValue();
    }

    public decimal GetAveragePrice()
    {
        return _repository.GetAveragePrice();
    }

    public Product? GetMostExpensiveProduct()
    {
        return _repository.GetMostExpensiveProduct();
    }

    public int GetProductCount()
    {
        return _repository.Count;
    }

    // ══════════════════════════════════════════════════════════════════
    // REPORTS
    // ══════════════════════════════════════════════════════════════════

    public string GenerateSummary()
    {
        var generator = new ReportGenerator(_repository.GetAll());
        return generator.GenerateSummary();
    }

    public string GenerateLowStockReport(int threshold = 5)
    {
        var generator = new ReportGenerator(_repository.GetAll());
        return generator.GenerateLowStockReport(threshold);
    }

    public string GenerateTopProducts(int count = 5)
    {
        var generator = new ReportGenerator(_repository.GetAll());
        return generator.GenerateTopProducts(count);
    }

    public string ExportCsv()
    {
        var generator = new ReportGenerator(_repository.GetAll());
        return generator.ExportCsv();
    }

    public string ExportSummaryJson()
    {
        var generator = new ReportGenerator(_repository.GetAll());
        return generator.ExportSummaryJson();
    }
}
