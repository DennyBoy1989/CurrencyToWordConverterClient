using System.Net;

namespace CurrencyToWordConverterClient.Domain.Test.Stubs;

/// <summary>
/// Stub-Implementation of the <see cref="HttpClient"/> class for tests. Does not execute an actual http request. It returns a mocked response instead.
/// </summary>
public class HttpClientStub : HttpClient {

    private HttpMessageHandlerStub httpMessageHandlerStub;

    public HttpClientStub(HttpMessageHandlerStub httpMessageHandlerStub) : base(httpMessageHandlerStub) {

        this.httpMessageHandlerStub = httpMessageHandlerStub;
    }

    /// <summary>
    /// Sets the response content and code that should be returned for a given http-method-type and url.
    /// </summary>
    public void SetResponse<DTO>(HttpMethod method, string url, DTO? content, HttpStatusCode httpStatusCode) {

        httpMessageHandlerStub.SetResponse(method, url, content, httpStatusCode);
    }

    /// <summary>
    /// Sets the response code that should be returned for a given http-method-type and url.
    /// </summary>
    public void SetResponse(HttpMethod method, string url, HttpStatusCode httpStatusCode) {

        httpMessageHandlerStub.SetResponse(method, url, httpStatusCode);
    }
}