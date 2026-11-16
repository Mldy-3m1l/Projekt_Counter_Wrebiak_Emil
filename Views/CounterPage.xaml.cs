using Counter.Models;

namespace Counter.Views;

[QueryProperty(nameof(CounterName), nameof(CounterName))]
public partial class CounterPage : ContentPage
{
    private CounterItem? currentCounter;

    public CounterPage()
    {
        InitializeComponent();
    }

    public string CounterName
    {
        set => LoadCounter(Uri.UnescapeDataString(value));
    }

    private void LoadCounter(string name)
    {
        var model = new AllCounters();
        currentCounter = model.FindByName(name);

        BindingContext = currentCounter;
    }

    private void OnPlusClicked(object sender, EventArgs e)
    {
        if (currentCounter == null)
            return;

        currentCounter.Value++;
        SaveCurrent();
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        if (currentCounter == null)
            return;

        currentCounter.Value--;
        SaveCurrent();
    }

    private void SaveCurrent()
    {
        var model = new AllCounters();

        if (currentCounter == null)
            return;

        var existing = model.FindByName(currentCounter.Name);
        if (existing != null)
            existing.Value = currentCounter.Value;

        model.SaveCounters();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}