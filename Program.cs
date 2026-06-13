// INVENTORY SYSTEM - Module 1 Complete
using System.Reflection;
using Inventory.Models;
using Inventory.Factories;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;


int productCount = 0; // Placeholder for product count
decimal totalValue = 0m; // Placeholder for total inventory value
bool isInventoryInitialized = true; //

// Load initial products (placeholder)
var products = new List<Product>();

showBanner();
Console.WriteLine("Available commands: list, add, search, exit");
Console.WriteLine();


bool  shouldContinue = true;
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



void SearchProduct()
{
    Console.WriteLine("🔍 Función buscar (se implementará completamente en Módulo 4)");
    
    Console.Write("\nBuscar por nombre: ");
    string termino = Console.ReadLine() ?? "";

    var encontrados = products
        .Where(p => p.Name.Contains(termino, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (encontrados.Count == 0)
    {
        Console.WriteLine($"No se encontraron products con '{termino}'");
        return;
    }

    Console.WriteLine($"\n=== {encontrados.Count} resultado(s) ===");
    foreach (var p in encontrados)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Name} | ${p.Price:F2}");
    }
}


bool ProcessCommand(string comando)
{
    switch (comando)
    {
        case "salir":
        case "exit":
        case "q":
            Console.WriteLine("¡Hasta luego!");
            return false;

        case "list":
            ListarProducts();
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
            Console.WriteLine($"❌ Comando '{comando}' no reconocido");
            Console.WriteLine("   Use: listar, agregar, buscar, salir");
            break;
    }

    Console.WriteLine();
    return true;
}


string ReadEntry(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine()?.Trim().ToLower() ?? "";
}

void ListarProducts()
{
    if (products.Count == 0)
    {
        Console.WriteLine("📦 No hay products en el inventario.");
        return;
    }

    Console.WriteLine("\n=== products ===");
    foreach (var p in products)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Name} | ${p.Price:F2} | Cant: {p.Quantity} | Total: ${p.TotalValue:F2}");
    }
    Console.WriteLine($"\nTotal: {products.Count} producto(s)");
}


void AddProduct()
{
        Console.WriteLine("\n--- Add Product ---");

    Console.Write("Name: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Price: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
    {
        Console.WriteLine("⚠ Invalid price.");
        return;
    }

    Console.Write("Quantity : ");
    if (!int.TryParse(Console.ReadLine(), out int cantidad))
    {
        Console.WriteLine("⚠ Invalid quantity.");
        return;
    }

    Console.WriteLine("\nCategories: Electronics, Clothing, Food, Home, Sports, Books, Others");
    Console.Write("Category: ");
    string catStr = Console.ReadLine() ?? "Others";

    if (!Enum.TryParse<ProductCategory>(catStr, true, out var categoria))
    {
        categoria = ProductCategory.Other;
    }

    try
    {
        var producto = ProductFactory.Create(nombre, precio, cantidad, categoria);
        products.Add(producto);
        Console.WriteLine($"\n✓ Producto '{producto.Name}' agregado con ID {producto.Id}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n⚠ Error: {ex.Message}");
    }
}