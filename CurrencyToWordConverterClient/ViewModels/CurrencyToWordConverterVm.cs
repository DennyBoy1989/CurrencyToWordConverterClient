﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CurrencyToWordConverterClient.Workflows;
using System.Threading.Tasks;

namespace CurrencyToWordConverterClient.ViewModels;

public partial class CurrencyToWordConverterVm : ObservableObject {

    private UserWorkflows userWorkflows;

    [ObservableProperty]
    string dollars = "100";

    [ObservableProperty]
    string cents = "00";

    [ObservableProperty]
    string wordRepresentation = "one hundred dollars";

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
        var wordRepresentationObject =  await userWorkflows.GetWordRepresentationOfGivenDollarAndCents(Dollars, Cents);
        WordRepresentation = wordRepresentationObject.Value;
    }
}
