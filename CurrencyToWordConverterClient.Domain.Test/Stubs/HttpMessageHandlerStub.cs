using System.Net.Http.Json;
using System.Net;

namespace CurrencyToWordConverterClient.Domain.Test.Stubs;

/// <summary>
/// Stub-Implementation of the <see cref="HttpMessageHandler"/> class for tests. Does not execute an actual http request. It returns a mocked response instead.
/// </summary>
public class HttpMessageHandlerStub : HttpMessageHandler {

    private Dictionary<TestResponseKey, HttpResponseMessage> responses = new Dictionary<TestResponseKey, HttpResponseMessage>();
    private HttpResponseMessage defaultResponse;

    public int AsyncCallCounter { get; private set; } = 0;

    public HttpMessageHandlerStub() {

        defaultResponse = new HttpResponseMessage() {
            StatusCode = HttpStatusCode.OK
        };
    }

    /// <summary>
    /// Sets the response content and code that should be returned for a given http-method-type and url.
    /// </summary>
    public void SetResponse<DTO>(HttpMethod method, string url, DTO? content, HttpStatusCode httpStatusCode) {

        var httpContent = JsonContent.Create(content);

        responses[new TestResponseKey(method, url)] = new HttpResponseMessage() {

            StatusCode = httpStatusCode,
            Content = httpContent
        };
    }

    /// <summary>
    /// Sets the response code that should be returned for a given http-method-type and url.
    /// </summary>
    public void SetResponse(HttpMethod method, string url, HttpStatusCode httpStatusCode) {

        responses[new TestResponseKey(method, url)] = new HttpResponseMessage() {

            StatusCode = httpStatusCode
        };
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

        var tcs = new TaskCompletionSource<HttpResponseMessage>();

        var key = new TestResponseKey(request.Method, request.RequestUri?.AbsoluteUri ?? "");
        var response = responses.GetValueOrDefault(key, defaultResponse);

        tcs.SetResult(response);

        AsyncCallCounter++;

        return tcs.Task;
    }
}

record TestResponseKey(HttpMethod Method, string Url);