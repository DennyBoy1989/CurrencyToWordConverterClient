using CurrencyToWordConverterClient.Domain;
using CurrencyToWordConverterClient.Interfaces;
using System.Threading.Tasks;

namespace CurrencyToWordConverterClient.Adapter;
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
