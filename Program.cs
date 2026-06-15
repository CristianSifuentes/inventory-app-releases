// INVENTORY SYSTEM - Module 4 Interactive CLI
using System.Reflection;
using Inventory.Factories;
using Inventory.Models;
using Inventory.Repositories;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;
var repository = new InMemoryProductRepository();

if (args.Length > 0)
{
    var arg = args[0].ToLower();

    if (arg == "--help" || arg == "-h")
    {
        ShowHelp();
        return;
    }

    if (arg == "--version" || arg == "-v")
    {
        Console.WriteLine($"Version: {version}");
        return;
    }

    if (arg == "--structure")
    {
        ShowStructure();
        return;
    }

    Console.WriteLine("Unknown option. Use --help for usage information.");
    Environment.Exit(2);
    return;
}

ShowBanner();
ShowAdvancedMenu();
Console.WriteLine();

var shouldContinue = true;
while (shouldContinue)
{
    var command = ReadCommand("inventory> ");
    shouldContinue = ProcessCommand(command);
}

bool ProcessCommand(string command)
{
    switch (command)
    {
        case "":
            break;

        case "help":
        case "menu":
            ShowAdvancedMenu();
            break;

        case "list":
            ListProducts();
            break;

        case "add":
            AddProduct();
            break;

        case "search":
            SearchProductsByName();
            break;

        case "find":
        case "id":
            SearchProductById();
            break;

        case "category":
            SearchProductsByCategory();
            break;

        case "price":
            SearchProductsByPriceRange();
            break;

        case "delete":
        case "remove":
            DeleteProduct();
            break;

        case "stats":
            ShowStatistics();
            break;

        case "low-stock":
            ShowLowStock();
            break;

        case "sort-price":
            ShowProductsOrderedByPrice();
            break;

        case "top-price":
            ShowTopProductsByPrice();
            break;

        case "export":
        case "csv":
            ExportCsv();
            break;

        case "structure":
            ShowStructure();
            break;

        case "clear":
            Console.Clear();
            ShowBanner();
            break;

        case "exit":
        case "q":
            Console.WriteLine("Exiting program...");
            return false;

        default:
            Console.WriteLine($"Command '{command}' was not recognized.");
            Console.WriteLine("Type 'help' to show the available commands.");
            break;
    }

    Console.WriteLine();
    return true;
}

void ShowBanner()
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

void ShowAdvancedMenu()
{
    Console.WriteLine("Available commands:");
    Console.WriteLine();
    Console.WriteLine("  Core");
    Console.WriteLine("    list          Lists all products");
    Console.WriteLine("    add           Adds a new product");
    Console.WriteLine("    search        Searches products by name");
    Console.WriteLine("    find          Finds a product by ID");
    Console.WriteLine("    delete        Deletes a product by ID");
    Console.WriteLine();
    Console.WriteLine("  Filters and Views");
    Console.WriteLine("    category      Lists products by category");
    Console.WriteLine("    price         Lists products by price range");
    Console.WriteLine("    sort-price    Lists products ordered by price");
    Console.WriteLine("    top-price     Shows the most expensive products");
    Console.WriteLine("    low-stock     Shows products with low stock");
    Console.WriteLine();
    Console.WriteLine("  Reports");
    Console.WriteLine("    stats         Shows inventory statistics");
    Console.WriteLine("    export        Prints inventory data as CSV");
    Console.WriteLine("    structure     Shows the project structure");
    Console.WriteLine();
    Console.WriteLine("  Utility");
    Console.WriteLine("    help          Shows this menu");
    Console.WriteLine("    clear         Clears the console");
    Console.WriteLine("    exit          Exits the program");
}

void ShowHelp()
{
    Console.WriteLine("USAGE: dotnet run [options]");
    Console.WriteLine();
    Console.WriteLine("OPTIONS:");
    Console.WriteLine("  --help, -h       Shows this help");
    Console.WriteLine("  --version, -v    Shows the version");
    Console.WriteLine("  --structure      Shows the project structure");
    Console.WriteLine();
    ShowAdvancedMenu();
    Console.WriteLine();
    Console.WriteLine("EXAMPLES:");
    Console.WriteLine("  dotnet run");
    Console.WriteLine("  dotnet run -- --version");
}

void ShowStructure()
{
    Console.WriteLine();
    Console.WriteLine("Structure:");
    Console.WriteLine("  InventoryApp.csproj");
    Console.WriteLine("  Program.cs");
    Console.WriteLine("  src/Models/");
    Console.WriteLine("  src/Factories/");
    Console.WriteLine("  src/Repositories/");
}

string ReadCommand(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine()?.Trim().ToLower() ?? "";
}

void ListProducts()
{
    var products = repository.GetAll()
        .OrderBy(product => product.Id)
        .ToList();

    if (products.Count == 0)
    {
        Console.WriteLine("There are no products in the inventory.");
        return;
    }

    PrintProductTable(products, "Products");
    Console.WriteLine($"\nTotal: {repository.Count} product(s)");
}

void AddProduct()
{
    Console.WriteLine("\n--- Add Product ---");

    Console.Write("Name: ");
    var name = Console.ReadLine() ?? "";

    Console.Write("Price: ");
    if (!decimal.TryParse(Console.ReadLine(), out var price))
    {
        Console.WriteLine("Invalid price.");
        return;
    }

    Console.Write("Quantity: ");
    if (!int.TryParse(Console.ReadLine(), out var quantity))
    {
        Console.WriteLine("Invalid quantity.");
        return;
    }

    Console.WriteLine("\nCategories: Electronics, Clothing, Food, Home, Sports, Books, Other");
    Console.Write("Category: ");
    var categoryText = Console.ReadLine() ?? "Other";

    if (!Enum.TryParse<ProductCategory>(categoryText, true, out var category))
    {
        category = ProductCategory.Other;
    }

    try
    {
        var product = ProductFactory.Create(name, price, quantity, category);
        repository.Add(product);
        Console.WriteLine($"\nProduct '{product.Name}' added with ID {product.Id}.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nError: {ex.Message}");
    }
}

void SearchProductsByName()
{
    Console.Write("\nSearch by name: ");
    var searchTerm = Console.ReadLine() ?? "";

    var matches = repository.FindByName(searchTerm).ToList();

    if (matches.Count == 0)
    {
        Console.WriteLine($"No products were found with '{searchTerm}'.");
        return;
    }

    PrintProductTable(matches, $"{matches.Count} result(s)");
}

void SearchProductById()
{
    Console.Write("\nID: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid ID.");
        return;
    }

    var product = repository.GetById(id);
    if (product == null)
    {
        Console.WriteLine($"There is no product with ID {id}.");
        return;
    }

    Console.WriteLine($"\n--- Product #{product.Id} ---");
    Console.WriteLine($"Name: {product.Name}");
    Console.WriteLine($"Price: ${product.Price:F2}");
    Console.WriteLine($"Quantity: {product.Quantity}");
    Console.WriteLine($"Total Value: ${product.TotalValue:F2}");
    Console.WriteLine($"Category: {product.Category}");
    Console.WriteLine($"Status: {product.Status}");
    Console.WriteLine($"Registration Date: {product.RegistrationDate:g}");
}

void SearchProductsByCategory()
{
    Console.WriteLine("\nCategories: Electronics, Clothing, Food, Home, Sports, Books, Other");
    Console.Write("Category: ");
    var categoryText = Console.ReadLine() ?? "";

    if (!Enum.TryParse<ProductCategory>(categoryText, true, out var category))
    {
        Console.WriteLine("Invalid category.");
        return;
    }

    var products = repository.FindByCategory(category).ToList();
    if (products.Count == 0)
    {
        Console.WriteLine($"No products were found in {category}.");
        return;
    }

    PrintProductTable(products, $"Products in {category}");
}

void SearchProductsByPriceRange()
{
    Console.Write("\nMinimum price: ");
    if (!decimal.TryParse(Console.ReadLine(), out var min))
    {
        Console.WriteLine("Invalid minimum price.");
        return;
    }

    Console.Write("Maximum price: ");
    if (!decimal.TryParse(Console.ReadLine(), out var max))
    {
        Console.WriteLine("Invalid maximum price.");
        return;
    }

    if (min > max)
    {
        Console.WriteLine("Minimum price cannot be greater than maximum price.");
        return;
    }

    var products = repository.FindByPriceRange(min, max).ToList();
    if (products.Count == 0)
    {
        Console.WriteLine($"No products were found between ${min:F2} and ${max:F2}.");
        return;
    }

    PrintProductTable(products, $"Products between ${min:F2} and ${max:F2}");
}

void DeleteProduct()
{
    Console.Write("\nID to delete: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid ID.");
        return;
    }

    var product = repository.GetById(id);
    if (product == null)
    {
        Console.WriteLine($"There is no product with ID {id}.");
        return;
    }

    Console.Write($"Delete '{product.Name}'? (y/n): ");
    if (Console.ReadLine()?.Trim().ToLower() != "y")
    {
        Console.WriteLine("Delete canceled.");
        return;
    }

    repository.Delete(id);
    Console.WriteLine("Product deleted.");
}

void ShowStatistics()
{
    Console.WriteLine("\n=== Statistics ===");
    Console.WriteLine($"Total products: {repository.Count}");
    Console.WriteLine($"Total inventory value: ${repository.GetTotalInventoryValue():F2}");
    Console.WriteLine($"Average price: ${repository.GetAveragePrice():F2}");

    var mostExpensiveProduct = repository.GetMostExpensiveProduct();
    if (mostExpensiveProduct != null)
    {
        Console.WriteLine($"Most expensive product: {mostExpensiveProduct.Name} (${mostExpensiveProduct.Price:F2})");
    }

    Console.WriteLine("\nProducts by category:");
    foreach (var item in repository.CountByCategory().OrderBy(item => item.Key))
    {
        Console.WriteLine($"  {item.Key}: {item.Value} product(s)");
    }

    Console.WriteLine("\nValue by category:");
    foreach (var item in repository.GetValueByCategory().OrderBy(item => item.Key))
    {
        Console.WriteLine($"  {item.Key}: ${item.Value:F2}");
    }
}

void ShowLowStock()
{
    Console.Write("\nLow stock threshold (default 5): ");
    var input = Console.ReadLine();
    var threshold = string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out var parsedThreshold)
        ? 5
        : parsedThreshold;

    var lowStockProducts = repository.GetLowStock(threshold).ToList();
    if (lowStockProducts.Count == 0)
    {
        Console.WriteLine($"There are no products with stock below {threshold}.");
        return;
    }

    PrintProductTable(lowStockProducts, $"Low stock products (< {threshold})");
}

void ShowProductsOrderedByPrice()
{
    Console.Write("\nDescending order? (y/n): ");
    var descending = Console.ReadLine()?.Trim().ToLower() == "y";

    var products = repository.GetOrderedByPrice(descending).ToList();
    if (products.Count == 0)
    {
        Console.WriteLine("There are no products to sort.");
        return;
    }

    PrintProductTable(products, descending ? "Products by highest price" : "Products by lowest price");
}

void ShowTopProductsByPrice()
{
    Console.Write("\nHow many products? (default 5): ");
    var input = Console.ReadLine();
    var count = string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out var parsedCount)
        ? 5
        : parsedCount;

    var products = repository.GetTopByPrice(count).ToList();
    if (products.Count == 0)
    {
        Console.WriteLine("There are no products to rank.");
        return;
    }

    PrintProductTable(products, $"Top {count} products by price");
}

void ExportCsv()
{
    Console.WriteLine("\n=== Export CSV ===");
    Console.WriteLine("Id,Name,Price,Quantity,Category,Status,TotalValue");

    foreach (var product in repository.GetAll().OrderBy(product => product.Id))
    {
        Console.WriteLine($"{product.Id},{product.Name},{product.Price:F2},{product.Quantity},{product.Category},{product.Status},{product.TotalValue:F2}");
    }

    Console.WriteLine("\n(Module 5 will save this output to a file.)");
}

void PrintProductTable(IEnumerable<Product> products, string title)
{
    Console.WriteLine($"\n=== {title} ===");
    foreach (var product in products)
    {
        Console.WriteLine($"ID: {product.Id} | {product.Name} | ${product.Price:F2} | Qty: {product.Quantity} | Category: {product.Category} | Total: ${product.TotalValue:F2}");
    }
}
