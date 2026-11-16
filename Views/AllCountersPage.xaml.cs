using Counter.Models;

namespace Counter.Views;

public partial class AllCountersPage : ContentPage
{
    public AllCountersPage()
    {
        InitializeComponent();
        BindingContext = new AllCounters();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((AllCounters)BindingContext).LoadCounters();
    }

    private async void OnAddCounterClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("Uwaga", "Wpisz nazwe licznika!", "OK");
            return;
        }

        var model = (AllCounters)BindingContext;

        model.Counters.Add(new CounterItem
        {
            Name = name.Trim(),
            Value = 0
        });

        model.SaveCounters();
        NameEntry.Text = "";
    }

    private async void OnCounterSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var selected = (CounterItem)e.CurrentSelection[0];

        await Shell.Current.GoToAsync(
            $"{nameof(CounterPage)}?{nameof(CounterPage.CounterName)}={Uri.EscapeDataString(selected.Name)}");

        countersCollection.SelectedItem = null;
    }
}