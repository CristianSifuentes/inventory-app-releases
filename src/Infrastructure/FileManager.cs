namespace Inventory.Infrastructure;

/// <summary>
/// Utility class for file operations.
/// Centralizes all I/O logic in one place.
/// 
/// System.IO methods used:
/// - File.WriteAllText: Creates or overwrites a file
/// - File.ReadAllText: Reads all content
/// - File.AppendAllText: Appends to the end
/// - File.Exists: Checks existence
/// - File.Delete: Deletes a file
/// - File.ReadAllLines: Reads line by line
/// - File.WriteAllLines: Writes lines
/// - Directory.CreateDirectory: Creates folders
/// - Directory.GetFiles: Lists files with a pattern
/// </summary>
public class FileManager
{
    public void Write(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public string Read(string path)
    {
        return File.ReadAllText(path);
    }

    public void Append(string path, string content)
    {
        File.AppendAllText(path, content);
    }

    public bool Exists(string path)
    {
        return File.Exists(path);
    }

    public void Delete(string path)
    {
        if (Exists(path))
        {
            File.Delete(path);
        }
    }

    public string[] ReadLines(string path)
    {
        return File.ReadAllLines(path);
    }

    public void WriteLines(string path, IEnumerable<string> lines)
    {
        File.WriteAllLines(path, lines);
    }

    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public string[] GetFiles(string directory, string pattern = "*")
    {
        if (!Directory.Exists(directory))
            return Array.Empty<string>();

        return Directory.GetFiles(directory, pattern);
    }
}
