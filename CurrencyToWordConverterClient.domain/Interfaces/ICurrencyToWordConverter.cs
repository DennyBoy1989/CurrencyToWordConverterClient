using CurrencyToWordConverterClient.Domain.DomainTypes;

namespace CurrencyToWordConverterClient.Domain.Interfaces;

public interface ICurrencyToWordConverter {

    public Task<CurrencyWordRepresentation> GetWordRepresentationOfCurrency(Currency currency);
}
