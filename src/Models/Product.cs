namespace Inventory.Models;

/// <summary>
/// Represents a product in the inventory.
/// Includes setter validation using guard clauses.
/// </summary>
public class Product
{
    private string _name = "";
    private decimal _price;
    private int _quantity;

    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            _name = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");
            _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative.");
            _quantity = value;
        }
    }

    public ProductCategory Category { get; set; } = ProductCategory.Other;
    public ProductStatus Status { get; set; } = ProductStatus.Active;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Calculated property: Price times Quantity.
    /// </summary>
    public decimal TotalValue => Price * Quantity;
}
