using System.Text.Json;
using System.Text.Json.Serialization;

public static class FileHandler
{
    public static readonly JsonSerializerOptions options = new
        JsonSerializerOptions()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper,
        Converters = { new JsonStringEnumConverter() }
    };
    public static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Console.WriteLine("\n\tKatalog yaratildi :)");
        }
    }
    public static void EnsureFileExists(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path);
            Console.WriteLine("\n\tFile yaratildi :)");
        }
    }

    public static void WriteToJsonFile(string path, List<Expense> expenses)
    {
        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            string content = JsonSerializer.Serialize(expenses, options);
            streamWriter.WriteLine(content);
        }
    }

    public static List<Expense> ReadFromJsonFile(string path)
    {
        using (StreamReader streamReader = new StreamReader(path))
        {
            return JsonSerializer.Deserialize<List<Expense>>(streamReader.ReadToEnd(), options) ?? new List<Expense>();
        }
    }

}