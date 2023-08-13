using CurrencyToWordConverterClient.Domain.DomainTypes;

namespace CurrencyToWordConverterClient.Domain.Interfaces;

public interface ICurrencyToWordConverter {

    /// <exception cref="DomainErrors.ConnectionError"></exception>
    public Task<CurrencyWordRepresentation> GetWordRepresentationOfCurrency(Currency currency);
}
