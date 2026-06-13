namespace Inventory.Models;

/// <summary>
/// Product status in the inventory.
/// </summary>
public enum ProductStatus
{
    /// <summary>Product available for sale.</summary>
    Active,
    
    /// <summary>Product temporarily unavailable.</summary>
    Inactive,
    
    /// <summary>Product that is no longer sold.</summary>
    Discontinued
}
