namespace Inventory.Models;

/// <summary>
/// Represents a supplier (record - immutable by default).
/// Example of when to use a record instead of a class.
/// </summary>
public record Supplier(
    int Id,
    string Name,
    string Email,
    string Phone
);
