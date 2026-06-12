// INVENTORY SYSTEM - Module 1 Complete
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║      INVENTORY MANAGEMENT SYSTEM     ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine($"Version: {version}");
Console.WriteLine($"NET: {Environment.Version}");
Console.WriteLine($"Platform: {Environment.OSVersion.Platform}");
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
