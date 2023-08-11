using CurrencyToWordConverterClient.Domain;
using System.Threading.Tasks;

namespace CurrencyToWordConverterClient.Interfaces;

public interface ICurrencyToWordConverter {

    public Task<CurrencyWordRepresentation> GetWordRepresentationOfCurrency(Currency currency);
}
