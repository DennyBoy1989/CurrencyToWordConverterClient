using Castle.Core.Logging;
using CurrencyToWordConverterClient.Adapter.CurrencyToWordApi;
using CurrencyToWordConverterClient.Domain.DomainErrors;
using CurrencyToWordConverterClient.Domain.Test.Stubs;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace CurrencyToWordConverterClient.Adapter.Test.CurrencyToWordApi;

public class CurrencyToWordConverterAdapterTest {

    private CurrencyToWordConverterAdapter currencyToWordConverterAdapter;
    private HttpClientStub httpClientStub;

    [SetUp]
    public void Setup() {
        httpClientStub = new HttpClientStub(new HttpMessageHandlerStub());
        var options = new CurrencyToWordConverterApiOptions() {
            BaseUrl = "https://localhost:7107/wordrepresentation"
        };

        currencyToWordConverterAdapter = new CurrencyToWordConverterAdapter(httpClientStub, Options.Create(options), A.Fake<ILogger<CurrencyToWordConverterAdapter>>());
    }

    [Test]
    public async Task GetWordRepresentation_WhenReponseReturnsWith200_ThenReturnValueOfWordRepresentationDto() {
        var responseWordRepresentationDto = new WordRepresentationDto() {
            Value = "one thousand dollars"
        };
        httpClientStub.SetResponse(HttpMethod.Get, "https://localhost:7107/wordrepresentation/1000,00", responseWordRepresentationDto, HttpStatusCode.OK);


        var result = await currencyToWordConverterAdapter.GetWordRepresentation("1000,00");

        Assert.That(result, Is.EqualTo("one thousand dollars"));
    }

    [Test]
    public void GetWordRepresentation_WhenReponseReturnsWithBadGateway_ThenThrowConnectionError() {

        httpClientStub.SetResponse(HttpMethod.Get, "https://localhost:7107/wordrepresentation/1000,00", HttpStatusCode.BadGateway);


        Assert.That(() => currencyToWordConverterAdapter.GetWordRepresentation("1000,00"), Throws.Exception.InstanceOf<ConnectionError>());
    }
}
