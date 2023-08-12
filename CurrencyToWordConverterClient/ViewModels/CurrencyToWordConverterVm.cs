using CommunityToolkit.Mvvm.ComponentModel;
using CurrencyToWordConverterClient.Workflows;

namespace CurrencyToWordConverterClient.ViewModels;

public partial class CurrencyToWordConverterVm : ObservableObject {

    private UserWorkflows userWorkflows;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WordRepresentation))]
    string dollars = "dollars";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WordRepresentation))]
    string cents = "cents";

    public string WordRepresentation => $"{Dollars},{Cents}";

    public CurrencyToWordConverterVm(UserWorkflows userWorkflows) {
        this.userWorkflows = userWorkflows;
    }

}
