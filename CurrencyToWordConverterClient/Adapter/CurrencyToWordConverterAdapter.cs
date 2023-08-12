using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace CurrencyToWordConverterClient.Adapter;

public class CurrencyToWordConverterAdapter {

    private readonly HttpClient httpClient;
    private readonly ILogger<CurrencyToWordConverterAdapter> logger;

    public CurrencyToWordConverterAdapter(HttpClient httpClient, ILogger<CurrencyToWordConverterAdapter> logger) {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public virtual async Task<string> GetWordRepresentation(string currencyString) {
        HttpResponseMessage response;
        using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7107/wordrepresentation/{currencyString}")) {
            try {
                response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var wordRepresentationString = await response.Content.ReadFromJsonAsync<WordRepresentationDto>();
                return wordRepresentationString?.Value ?? "";
            }
            catch (HttpRequestException ex) {
                logger.LogWarning(ex, $"Could not retrieve the word representation of currency '{currencyString}'. See inner Exception for more details.");
                return "";
            }
        }
    }
}
