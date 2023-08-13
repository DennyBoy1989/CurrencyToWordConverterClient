using CommunityToolkit.Mvvm.ComponentModel;
using CurrencyToWordConverterClient.Domain.DomainErrors;
using CurrencyToWordConverterClient.Domain.Workflows;
using System;
using System.Threading.Tasks;

namespace CurrencyToWordConverterClient.ViewModels;

/// <summary>
/// View Model for the MainWindow. Handles dollar and cent inputs and displays the word reprensetation fetched by external services.
/// Uses the Cummunity Toolkit MVVM to generate additional source files.
/// </summary>
public partial class CurrencyToWordConverterVm : ObservableObject {

    private UserWorkflows userWorkflows;

    [ObservableProperty]
    private string dollars = "100";

    [ObservableProperty]
    private string cents = "00";

    [ObservableProperty]
    private string wordRepresentation = "one hundred dollars";

    public CurrencyToWordConverterVm(UserWorkflows userWorkflows) {
        this.userWorkflows = userWorkflows;
    }

    async partial void OnDollarsChanged(string value) {
        await GetWordRepresentation();
    }

    async partial void OnCentsChanged (string value) {
        await GetWordRepresentation();
    }

    private async Task GetWordRepresentation() {
        try {
            var wordRepresentationObject = await userWorkflows.GetWordRepresentationOfGivenDollarAndCents(Dollars, Cents);
            WordRepresentation = wordRepresentationObject.Value;
        }
        catch (Exception ex) {
            WordRepresentation = ex switch {
                ConnectionError => "Could not contact the needed backend service. Please check if the service is running or the connection string is set correctly.",
                _ => "Something complete unforeseen happend. Please contact the support!"
            };
        }
        
    }
}
