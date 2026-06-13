// INVENTORY SYSTEM - Module 1 Complete
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

int productCount = 0; // Placeholder for product count
decimal totalValue = 0m; // Placeholder for total inventory value
bool isInventoryInitialized = true; // Flag to indicate if inventory is initialized

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

showBanner();
Console.WriteLine("Available commands: list, add, search, exit");
Console.WriteLine();


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


