using CurrencyToWordConverterClient.Domain.DomainErrors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CurrencyToWordConverterClient.Adapter.CurrencyToWordApi;

/// <summary>
/// Executes the actual web request to the CurrencyToWordConverter API. 
/// </summary>
public class CurrencyToWordConverterAdapter {

    private readonly HttpClient httpClient;
    private readonly CurrencyToWordConverterApiOptions currencyToWordConverterApiOptions;
    private readonly ILogger<CurrencyToWordConverterAdapter> logger;


    public CurrencyToWordConverterAdapter(HttpClient httpClient, IOptions<CurrencyToWordConverterApiOptions> currencyToWordConverterApiOptions, ILogger<CurrencyToWordConverterAdapter> logger) {
        this.httpClient = httpClient;
        this.currencyToWordConverterApiOptions = currencyToWordConverterApiOptions.Value;
        this.logger = logger;
    }


    /// <summary>
    /// Gets the currency word representation string by its currency number string.
    /// </summary>
    public virtual async Task<string> GetWordRepresentation(string currencyString) {
        HttpResponseMessage response;
        using (var request = new HttpRequestMessage(HttpMethod.Get, $"{currencyToWordConverterApiOptions.BaseUrl}/{currencyString}")) {
            try {
                response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var wordRepresentationString = await response.Content.ReadFromJsonAsync<WordRepresentationDto>();
                return wordRepresentationString?.Value ?? "";
            }
            catch (HttpRequestException ex) {
                logger.LogWarning(ex, $"Could not retrieve the word representation of currency '{currencyString}'. See inner Exception for more details.");
                throw new ConnectionError($"Connection error while trying to access '{currencyToWordConverterApiOptions.BaseUrl}/{currencyString}'", ex);
            }
        }
    }
}
