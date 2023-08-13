using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Interfaces;

namespace CurrencyToWordConverterClient.Adapter.CurrencyToWordApi;

/// <summary>
/// Implementation of the <see cref="ICurrencyToWordConverter"/> interface. To convert a currency to its word representation a web api is called.
/// </summary>
public class CurrencyToWordConverter : ICurrencyToWordConverter {

    private CurrencyToWordConverterAdapter adapter;

    public CurrencyToWordConverter(CurrencyToWordConverterAdapter adapter) {
        this.adapter = adapter;
    }

    public async Task<CurrencyWordRepresentation> GetWordRepresentationOfCurrency(Currency currency) {
        var wordRepresentationAsString = await adapter.GetWordRepresentation(currency.Dollars + "," + currency.Cents);
        return new CurrencyWordRepresentation(wordRepresentationAsString);
    }
}
