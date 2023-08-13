using CurrencyToWordConverterClient.Adapter.CurrencyToWordApi;
using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Test.Stubs;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace CurrencyToWordConverterClient.Adapter.Test.CurrencyToWordApi;

public class CurrencyToWordConverterTest {

    private CurrencyToWordConverter currencyToWordConverter;
    private HttpClientStub httpClientStub;

    [SetUp] 
    public void Setup() {

        httpClientStub = new HttpClientStub(new HttpMessageHandlerStub());
        var options = new CurrencyToWordConverterApiOptions() {
            BaseUrl = "https://localhost:7107/wordrepresentation"
        };
        var adapter = new CurrencyToWordConverterAdapter(httpClientStub, Options.Create(options), A.Fake<ILogger<CurrencyToWordConverterAdapter>>());
        currencyToWordConverter = new CurrencyToWordConverter(adapter);
    }

    [Test]
    public async Task GetWordRepresentationOfCurrency_WhenEndPointReturnsWordRepresentationDto_ThenReturnWordRepresentation() {

        var responseWordRepresentationDto = new WordRepresentationDto() {
            Value = "one thousand dollars"
        };
        httpClientStub.SetResponse(HttpMethod.Get, "https://localhost:7107/wordrepresentation/1000,00", responseWordRepresentationDto, HttpStatusCode.OK);


        var result = await currencyToWordConverter.GetWordRepresentationOfCurrency(new Currency("1000", "00"));
        Assert.That(result.Value, Is.EqualTo("one thousand dollars"));
    }
}
