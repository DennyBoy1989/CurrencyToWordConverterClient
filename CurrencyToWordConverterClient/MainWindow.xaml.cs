using CurrencyToWordConverterClient.ViewModels;
using System.Windows;

namespace CurrencyToWordConverterClient;

/// <summary>
/// Logic behinde the MainWindow. Should only contain Initialization and the binding to the correct viewmodel
/// </summary>
public partial class MainWindow : Window {
    public MainWindow(CurrencyToWordConverterVm currencyToWordConverterVm) {
        InitializeComponent();
        this.DataContext = currencyToWordConverterVm;
    }
}
