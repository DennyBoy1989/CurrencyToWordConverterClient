namespace CurrencyToWordConverterClient.Adapter.CurrencyToWordApi;

/// <summary>
/// Holds configurations for the currency to word api endpoint. Can be injected using the .net Options pattern.
/// </summary>
public class CurrencyToWordConverterApiOptions {
    public string BaseUrl { get; set; } = null!;
}
