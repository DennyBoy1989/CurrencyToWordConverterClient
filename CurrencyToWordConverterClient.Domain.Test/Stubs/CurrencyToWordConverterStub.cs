using CurrencyToWordConverterClient.Domain.DomainErrors;
using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Interfaces;

namespace CurrencyToWordConverterClient.Domain.Test.Stubs;

/// <summary>
/// Stub-Implementation of the <see cref="ICurrencyToWordConverter"/> interface for tests. Values, that were added, can be requested by the methods of the interface.
/// </summary>
public class CurrencyToWordConverterStub : ICurrencyToWordConverter {

    public Dictionary<Currency,CurrencyWordRepresentation> SavedWordRepresentations { get; private set; } = new Dictionary<Currency,CurrencyWordRepresentation>();

    /// <summary>
    /// Adds a Currency-CurrencyWordRepresentation mapping to the stub. The added word representation is returned, when the <see cref="GetWordRepresentationOfCurrency"/> method is called, with the added currency.
    /// </summary>
    public virtual void AddWordRepresentation(Currency currency, CurrencyWordRepresentation wordRepresentation) {
        SavedWordRepresentations.Add(currency, wordRepresentation);
    }

    public virtual Task<CurrencyWordRepresentation> GetWordRepresentationOfCurrency(Currency currency) {

        if (!SavedWordRepresentations.ContainsKey(currency)) {
            throw new ConnectionError();
        }

        return Task.FromResult(SavedWordRepresentations[currency]);
    }
}
