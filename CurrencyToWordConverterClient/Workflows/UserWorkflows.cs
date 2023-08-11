using CurrencyToWordConverterClient.Domain;
using CurrencyToWordConverterClient.Interfaces;
using System.Threading.Tasks;

namespace CurrencyToWordConverterClient.Workflows;

public class UserWorkflows {

    private ICurrencyToWordConverter currencyToWordConverter;

    public UserWorkflows(ICurrencyToWordConverter currencyToWordConverter) {
        this.currencyToWordConverter = currencyToWordConverter;
    }

    public async Task<CurrencyWordRepresentation> GetWordRepresentationOfGivenDollarAndCents(string dollarString , string centString) {
        return await currencyToWordConverter.GetWordRepresentationOfCurrency(new Currency(dollarString, centString));
    }
}
