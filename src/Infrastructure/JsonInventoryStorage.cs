namespace Inventory.Infrastructure;

using System.Text.Json;
using System.Text.Json.Serialization;
using Inventory.Models;

/// <summary>
/// JSON inventory persistence.
/// Uses System.Text.Json, the native .NET serializer.
/// </summary>
public class JsonInventoryStorage
{
    private readonly FileManager _fileManager;
    private readonly JsonSerializerOptions _options;

    public JsonInventoryStorage()
    {
        _fileManager = new FileManager();

        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };
    }

    /// <summary>
    /// Serializes a product list to JSON and saves it to a file.
    /// </summary>
    public void Save(List<Product> products, string path)
    {
        var json = JsonSerializer.Serialize(products, _options);
        _fileManager.Write(path, json);
    }

    /// <summary>
    /// Reads a JSON file and deserializes it to a product list.
    /// Returns an empty list if the file does not exist or is empty.
    /// </summary>
    public List<Product> Load(string path)
    {
        if (!_fileManager.Exists(path))
            return new List<Product>();

        var json = _fileManager.Read(path);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Product>();

        return JsonSerializer.Deserialize<List<Product>>(json, _options)
               ?? new List<Product>();
    }

    /// <summary>
    /// Creates a timestamped backup before overwriting data.
    /// </summary>
    public void CreateBackup(string path)
    {
        if (!_fileManager.Exists(path))
            return;

        var directory = Path.GetDirectoryName(path);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        var extension = Path.GetExtension(path);
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        var backupPath = Path.Combine(
            directory ?? ".",
            $"{fileNameWithoutExtension}_backup_{timestamp}{extension}"
        );

        File.Copy(path, backupPath);
    }
}
