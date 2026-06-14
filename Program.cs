// INVENTORY SYSTEM - Module 1 Complete
using System.Reflection;
using Inventory.Models;
using Inventory.Factories;
using Inventory.Repositories;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;
var repository = new InMemoryProductRepository();


// int productCount = 0; // Placeholder for product count
// decimal totalValue = 0m; // Placeholder for total inventory value
// bool isInventoryInitialized = true; //




// Load initial products (placeholder)
var products = new List<Product>();

showBanner();
Console.WriteLine("Available commands: list, add, search, exit");
Console.WriteLine();


bool  shouldContinue = true;
bool isInventoryInitialized = false;
while (shouldContinue)
{

    var command = ReadEntry("inventory> ");
    shouldContinue = ProcessCommand(command);
}


if (args.Length > 0)
{
    var arg = args[0].ToLower();
    if (arg == "--help" || arg == "-h")
    {
        showHelp();
        Environment.Exit(0);
        return;
    }
    else if (arg == "--version" || arg == "-v")
    {
        Console.WriteLine($"Version: {version}");
        return;
    }
    else if (arg == "--structure")
    {
        showStructure();
        Environment.Exit(0);
        return;
    }
    else if (arg == "q")
    {
        isInventoryInitialized = false;
        Console.WriteLine("Exiting program...");
        Environment.Exit(0);
        return;
    }

    else
    {
        Console.WriteLine("Unknown option. Use --help for usage information.");
        Environment.Exit(2);
        return;
    }
}
else
{
    // Initialize inventory (placeholder)
    isInventoryInitialized = true;
}




while (isInventoryInitialized)
{
    Console.Write("> ");
    var input = Console.ReadLine()?.Trim().ToLower();

    switch (input)
    {
        case "list":
            Console.WriteLine("Listing products... (placeholder)");
            break;
        case "add":
            Console.WriteLine("Adding product... (placeholder)");
            break;
        case "search":
            Console.WriteLine("Searching products... (placeholder)");
            break;
        case "exit":
            Console.WriteLine("Exiting program...");
            return;
        default:
            Console.WriteLine("Unknown command. Type 'help' for options.");
            break;
    }
	
	if(isInventoryInitialized)
	   Console.WriteLine();
}

Environment.Exit(0);



void showStructure()
{
    Console.WriteLine();
    Console.WriteLine("📁 Structure:");
    Console.WriteLine("   ✓ .csproj configuration");
    Console.WriteLine("   ✓ src/Models/ structure");
    Console.WriteLine("   ✓ .gitignore configured");
    Console.WriteLine("   ✓ README.md documented");
    Console.WriteLine();
    Console.WriteLine("═══════════════════════════════════════");
    Console.WriteLine("  ✓ MODULE 1 COMPLETE");
    Console.WriteLine("  → Next: Module 2 - Interactive CLI");
    Console.WriteLine("═══════════════════════════════════════");

}


void showHelp() 
{
    Console.WriteLine("USAGE: dotnet run [options]");
    Console.WriteLine();
    Console.WriteLine("OPTIONS:");
    Console.WriteLine("  --help, -h       Shows this help");
    Console.WriteLine("  --version, -v    Shows the version");
    Console.WriteLine("  --structure      Shows the project structure");
    Console.WriteLine();
    Console.WriteLine("INTERACTIVE COMMANDS:");
    Console.WriteLine("  list             Lists inventory products");
    Console.WriteLine("  add              Adds a new product");
    Console.WriteLine("  search           Searches products");
    Console.WriteLine("  exit             Exits the program");
    Console.WriteLine();
    Console.WriteLine("EXAMPLES:");
    Console.WriteLine("  dotnet run");
    Console.WriteLine("  dotnet run --version");


}





bool ProcessCommand(string comando)
{
    switch (comando)
    {
        case "exit":
        case "q":
            Console.WriteLine("See you later!");
            return false;

        case "list":
            ListProducts();
            break;

        case "add":
            AddProduct();
            break;

        case "search":
            SearchProduct();
            break;

        case "":
            break;

        default:
            Console.WriteLine($"❌ Command '{comando}' not recognized");
            Console.WriteLine("   Use: list, add, search, exit");
            break;
    }

    Console.WriteLine();
    return true;
}

void showBanner()
{
    
   Console.WriteLine("╔══════════════════════════════════════╗");
   Console.WriteLine("║      INVENTORY MANAGEMENT SYSTEM     ║");
   Console.WriteLine("╚══════════════════════════════════════╝");
   Console.WriteLine();
   Console.WriteLine($"Version: {version}");
   Console.WriteLine($"NET: {Environment.Version}");
   Console.WriteLine($"Platform: {Environment.OSVersion.Platform}");
   Console.WriteLine();

    
}

string ReadEntry(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine()?.Trim().ToLower() ?? "";
}

void ListProducts()
{
    if (products.Count == 0)
    {
        Console.WriteLine("📦 There are no products in the inventory.");
        return;
    }

    Console.WriteLine("\n=== products ===");
    foreach (var p in products)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Name} | ${p.Price:F2} | Qty: {p.Quantity} | Total: ${p.TotalValue:F2}");
    }
    Console.WriteLine($"\nTotal: {products.Count} product(s)");
}


void AddProduct()
{
    Console.WriteLine("\n--- Add Product ---");

    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Price: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        Console.WriteLine("⚠ Invalid price.");
        return;
    }

    Console.Write("Quantity : ");
    if (!int.TryParse(Console.ReadLine(), out int quantity))
    {
        Console.WriteLine("⚠ Invalid quantity.");
        return;
    }

    Console.WriteLine("\nCategories: Electronics, Clothing, Food, Home, Sports, Books, Others");
    Console.Write("Category: ");
    string catStr = Console.ReadLine() ?? "Others";

    if (!Enum.TryParse<ProductCategory>(catStr, true, out var category))
    {
        category = ProductCategory.Other;
    }

    try
    {
        var product = ProductFactory.Create(name, price, quantity, category);
        products.Add(product);
        Console.WriteLine($"\n✓ Product '{product.Name}' added with ID {product.Id}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n⚠ Error: {ex.Message}");
    }
}
void SearchProduct()
{
    Console.WriteLine("🔍 Search Function (to be implemented in Module 4)");
    
    Console.Write("\nSearch by name: ");
    string searchTerm = Console.ReadLine() ?? "";

    var matches = products
        .Where(product => product.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine($"No products were found with '{searchTerm}'");
        return;
    }

    Console.WriteLine($"\n=== {matches.Count} result(s) ===");
    foreach (var product in matches)
    {
        Console.WriteLine($"ID: {product.Id} | {product.Name} | ${product.Price:F2}");
    }
}
void ShowStatistics()
{
    Console.WriteLine("\n=== ESTADÍSTICAS ===");
    Console.WriteLine($"products totales: {repository.Count}");
    Console.WriteLine($"Valor total inventario: ${repository.GetTotalInventoryValue():F2}");
    Console.WriteLine($"Precio promedio: ${repository.GetAveragePrice():F2}");

    var masCaro = repository.GetMostExpensiveProduct();
    if (masCaro != null)
    {
        Console.WriteLine($"Más caro: {masCaro.Name} (${masCaro.Price:F2})");
    }

    Console.WriteLine("\nPor categoría:");
    foreach (var kvp in repository.CountByCategory())
    {
        Console.WriteLine($"  {kvp.Key}: {kvp.Value} product(s)");
    }
}


void ShowStockLow()
{
    var lowStockProducts = repository.GetLowStock(5).ToList();

    if (lowStockProducts.Count == 0)
    {
        Console.WriteLine("\n✓ There are no products with low stock.");
        return;
    }

    Console.WriteLine("\n=== ALERT: LOW STOCK (< 5) ===");
    foreach (var p in lowStockProducts)
    {
        Console.WriteLine($"⚠ {p.Name} | Stock: {p.Quantity} | ${p.Price:F2}");
    }
}


void ExportCsv()
{
    Console.WriteLine("\n=== EXPORT CSV ===");
    Console.WriteLine("Id,Name,Price,Quantity,Category,TotalValue");

    foreach (var p in repository.GetAll().OrderBy(p => p.Id))
    {
        Console.WriteLine($"{p.Id},{p.Name},{p.Price:F2},{p.Quantity},{p.Category},{p.TotalValue:F2}");
    }

    Console.WriteLine("\n(In module 5: we will save this to a file)");
}


void SearchProductById()
{
    Console.Write("\nID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("⚠ ID inválido.");
        return;
    }

    var product = repository.GetById(id);

    if (product == null)
    {
        Console.WriteLine($"⚠ There is no product with ID {id}");
        return;
    }

    Console.WriteLine($"\n--- Product #{product.Id} ---");
    Console.WriteLine($"Name: {product.Name}");
    Console.WriteLine($"Price: ${product.Price :F2}");
    Console.WriteLine($"Quantity: {product.Quantity}");
    Console.WriteLine($"Total Value: ${product.TotalValue:F2}");
    Console.WriteLine($"Category: {product.Category}");
    Console.WriteLine($"Estado: {product.Status }");
}

void DeleteProduct()
{
    Console.Write("\nID a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("⚠ ID inválido.");
        return;
    }

    var product = repository.GetById(id);
    if (product == null)
    {
        Console.WriteLine($"⚠ No existe product con ID {id}");
        return;
    }

    Console.Write($"¿Eliminar '{product.Name}'? (s/n): ");
    if (Console.ReadLine()?.ToLower() == "s")
    {
        repository.Delete(id);
        Console.WriteLine("✓ Product deleted.");
    }
}

void SearchProductsByCategory()
{
    Console.WriteLine("\nCategories: Electronica, Ropa, Alimentos, Hogar, Deportes, Libros, Otros");
    Console.Write("Category: ");
    string catStr = Console.ReadLine() ?? "";

    if (!Enum.TryParse<ProductCategory>(catStr, true, out var category))
    {
        Console.WriteLine("⚠ Category invalid.");
        return;
    }

    var products = repository.FindByCategory(category).ToList();

    if (products.Count == 0)
    {
        Console.WriteLine($"\nNo hay products en {category.ToString().ToUpper()}.");
        return;
    }

    Console.WriteLine($"\n=== productS EN {category.ToString().ToUpper()} ===");
    foreach (var p in products)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Name} | ${p.Price:F2} | Cant: {p.Quantity}");
    }
}