namespace Inventory.Infrastructure;

using System.Text;
using System.Text.Json;
using Inventory.Models;

/// <summary>
/// Generates reports in multiple formats.
/// Uses StringBuilder for efficient string composition.
/// </summary>
public class ReportGenerator
{
    private readonly IEnumerable<Product> _products;

    public ReportGenerator(IEnumerable<Product> products)
    {
        _products = products;
    }

    /// <summary>
    /// General inventory summary.
    /// </summary>
    public string GenerateSummary()
    {
        var sb = new StringBuilder();
        var products = _products.ToList();

        sb.AppendLine("╔══════════════════════════════════════╗");
        sb.AppendLine("║          INVENTORY SUMMARY           ║");
        sb.AppendLine("╚══════════════════════════════════════╝");
        sb.AppendLine();
        sb.AppendLine($"  Total products:        {products.Count}");
        sb.AppendLine($"  Total value:           ${products.Sum(product => product.TotalValue):F2}");

        if (products.Count > 0)
        {
            sb.AppendLine($"  Average price:         ${products.Average(product => product.Price):F2}");
            sb.AppendLine();
            sb.AppendLine("  By category:");

            var byCategory = products
                .GroupBy(product => product.Category)
                .OrderByDescending(g => g.Count());

            foreach (var group in byCategory)
            {
                var value = group.Sum(product => product.TotalValue);
                sb.AppendLine($"    - {group.Key,-15} {group.Count(),3} products  ${value,10:F2}");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Products with low stock.
    /// </summary>
    public string GenerateLowStockReport(int minimum = 5)
    {
        var sb = new StringBuilder();
        var lowStock = _products
            .Where(product => product.Quantity < minimum)
            .OrderBy(product => product.Quantity)
            .ToList();

        sb.AppendLine($"╔══════════════════════════════════════╗");
        sb.AppendLine($"║       LOW STOCK ALERT (< {minimum})         ║");
        sb.AppendLine($"╚══════════════════════════════════════╝");
        sb.AppendLine();

        if (!lowStock.Any())
        {
            sb.AppendLine("  There are no products with low stock.");
            return sb.ToString();
        }

        foreach (var product in lowStock)
        {
            var alert = product.Quantity == 0 ? "OUT OF STOCK" : $"{product.Quantity} units";
            sb.AppendLine($"  {product.Id,3}. {product.Name,-20} {alert,-15} ${product.Price:F2}");
        }

        sb.AppendLine();
        sb.AppendLine($"  Total: {lowStock.Count} product(s) require attention");

        return sb.ToString();
    }

    /// <summary>
    /// Top N products by total value.
    /// </summary>
    public string GenerateTopProducts(int count = 5)
    {
        var sb = new StringBuilder();
        var top = _products
            .OrderByDescending(product => product.TotalValue)
            .Take(count)
            .ToList();

        sb.AppendLine($"╔══════════════════════════════════════╗");
        sb.AppendLine($"║      TOP {count} PRODUCTS BY VALUE          ║");
        sb.AppendLine($"╚══════════════════════════════════════╝");
        sb.AppendLine();

        if (!top.Any())
        {
            sb.AppendLine("  There are no products available.");
            return sb.ToString();
        }

        var position = 1;
        foreach (var product in top)
        {
            sb.AppendLine($"  {position}. {product.Name,-20} ${product.TotalValue,10:F2}");
            sb.AppendLine($"     ({product.Quantity} x ${product.Price:F2})");
            position++;
        }

        return sb.ToString();
    }

    /// <summary>
    /// Exports products to CSV.
    /// </summary>
    public string ExportCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Name,Price,Quantity,Category,Status,TotalValue");

        foreach (var product in _products.OrderBy(product => product.Id))
        {
            sb.AppendLine($"{product.Id},\"{product.Name}\",{product.Price:F2},{product.Quantity},{product.Category},{product.Status},{product.TotalValue:F2}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Exports a summary to JSON for APIs or integrations.
    /// </summary>
    public string ExportSummaryJson()
    {
        var products = _products.ToList();

        var summary = new
        {
            GeneratedAt = DateTime.Now,
            TotalProducts = products.Count,
            TotalInventoryValue = products.Sum(product => product.TotalValue),
            AveragePrice = products.Count > 0 ? products.Average(product => product.Price) : 0,
            ProductsByCategory = products
                .GroupBy(product => product.Category)
                .Select(g => new { Category = g.Key.ToString(), Count = g.Count(), Value = g.Sum(product => product.TotalValue) }),
            Top5Products = products
                .OrderByDescending(product => product.TotalValue)
                .Take(5)
                .Select(product => new { product.Id, product.Name, product.Quantity, product.TotalValue })
        };

        return JsonSerializer.Serialize(summary, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
