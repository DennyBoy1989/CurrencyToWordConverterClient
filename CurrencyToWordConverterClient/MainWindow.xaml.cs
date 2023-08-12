using CurrencyToWordConverterClient.ViewModels;
using System.Windows;

namespace CurrencyToWordConverterClient;

public partial class MainWindow : Window {
    public MainWindow(CurrencyToWordConverterVm currencyToWordConverterVm) {
        InitializeComponent();
        this.DataContext = currencyToWordConverterVm;
    }
}
