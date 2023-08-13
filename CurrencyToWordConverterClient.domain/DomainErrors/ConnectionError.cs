namespace CurrencyToWordConverterClient.Domain.DomainErrors;

public class ConnectionError : Exception {

    public ConnectionError() : base() {
    }

    public ConnectionError(string message) : base(message) {
    }

    public ConnectionError(string? message, Exception e) : base(message, e) {
    }
}
