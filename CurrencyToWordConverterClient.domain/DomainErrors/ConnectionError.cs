namespace CurrencyToWordConverterClient.Domain.DomainErrors;

/// <summary>
/// Error, that indicates that there was a problem with the connection, when trying to connect to a external service.
/// </summary>
public class ConnectionError : Exception {

    public ConnectionError() : base() {
    }

    public ConnectionError(string message) : base(message) {
    }

    public ConnectionError(string? message, Exception e) : base(message, e) {
    }
}
