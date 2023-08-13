using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CurrencyToWordConverterClient.Domain.Workflows;

/// <summary>
/// Collection of user workflows.
/// </summary>
public class UserWorkflows {

    private ILogger<UserWorkflows> logger;
    private ICurrencyToWordConverter currencyToWordConverter;

    public UserWorkflows(ILogger<UserWorkflows> logger,ICurrencyToWordConverter currencyToWordConverter) {
        this.logger = logger;
        this.currencyToWordConverter = currencyToWordConverter;
    }

    /// <summary>
    /// The basic workflow, when the user inputs values for dollars and cents. Returns a word representation of the input value.
    /// </summary>
    /// <exception cref="DomainErrors.ConnectionError"></exception>
    public async Task<CurrencyWordRepresentation> GetWordRepresentationOfGivenDollarAndCents(string dollarString, string centString) {

        logger.LogDebug("Started getting word representation from interface");
        var wordRepresentation = await currencyToWordConverter.GetWordRepresentationOfCurrency(new Currency(dollarString, centString));
        logger.LogDebug("Finished getting word representation from interface");

        return wordRepresentation;
    }
}
