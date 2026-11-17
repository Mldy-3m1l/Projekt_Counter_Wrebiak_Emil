
using System.Collections.ObjectModel;

namespace Counter.Models;

internal class AllCounters
{
    public ObservableCollection<CounterItem> Counters { get; set; } = new();

    private static string FolderPath =>
        Path.Combine(FileSystem.AppDataDirectory, "Counters");

    public AllCounters() => LoadCounters();

    public void LoadCounters()
    {
        Counters.Clear();

        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        foreach (string file in Directory.GetFiles(FolderPath, "*.txt"))
        {
            string[] lines = File.ReadAllLines(file);

            if (lines.Length >= 2 && int.TryParse(lines[1], out int value))
            {
                Counters.Add(new CounterItem
                {
                    Name = lines[0],
                    Value = value
                });
            }
        }
    }

    public void SaveCounters()
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        foreach (CounterItem counter in Counters)
        {
            string fileName = counter.Name + ".txt";
            string filePath = Path.Combine(FolderPath, fileName);

            File.WriteAllLines(filePath, new[]
            {
                counter.Name,
                counter.Value.ToString()
            });
        }
    }

    public CounterItem? FindByName(string name)
        => Counters.FirstOrDefault(c => c.Name == name);
}
