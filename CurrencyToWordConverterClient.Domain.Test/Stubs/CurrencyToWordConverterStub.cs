using CurrencyToWordConverterClient.Domain.DomainErrors;
using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Interfaces;

namespace CurrencyToWordConverterClient.Domain.Test.Stubs;

public class CurrencyToWordConverterStub : ICurrencyToWordConverter {

    public Dictionary<Currency,CurrencyWordRepresentation> SavedWordRepresentations { get; private set; } = new Dictionary<Currency,CurrencyWordRepresentation>();

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
