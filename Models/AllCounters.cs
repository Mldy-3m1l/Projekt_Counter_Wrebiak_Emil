using System.Collections.ObjectModel;
using System.Text.Json;

namespace Counter.Models;

internal class AllCounters
{
    public ObservableCollection<CounterItem> Counters { get; set; } = new();

    private static string FileName =>
        Path.Combine(FileSystem.AppDataDirectory, "counters.json");

    public AllCounters() => LoadCounters();

    public void LoadCounters()
    {
        Counters.Clear();

        if (!File.Exists(FileName))
            return;

        string json = File.ReadAllText(FileName);
        var loaded = JsonSerializer.Deserialize<List<CounterItem>>(json);

        if (loaded != null)
            foreach (CounterItem counter in loaded)
                Counters.Add(counter);
    }

    public void SaveCounters()
    {
        string json = JsonSerializer.Serialize(Counters.ToList());
        File.WriteAllText(FileName, json);
    }

    public CounterItem? FindByName(string name)
        => Counters.FirstOrDefault(c => c.Name == name);
}