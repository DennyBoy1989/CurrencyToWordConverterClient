using CurrencyToWordConverterClient.Domain.DomainTypes;

namespace CurrencyToWordConverterClient.Domain.Interfaces;

/// <summary>
/// Interface for converting a currency to its word representation.
/// </summary>
public interface ICurrencyToWordConverter {

    /// <summary>
    /// Convert a currency to its word representation.
    /// </summary>
    /// <exception cref="DomainErrors.ConnectionError"></exception>
    public Task<CurrencyWordRepresentation> GetWordRepresentationOfCurrency(Currency currency);
}
