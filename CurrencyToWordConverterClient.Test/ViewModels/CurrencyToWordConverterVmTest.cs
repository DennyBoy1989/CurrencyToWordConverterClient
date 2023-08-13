using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Test.Stubs;
using CurrencyToWordConverterClient.Domain.Workflows;
using CurrencyToWordConverterClient.ViewModels;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace CurrencyToWordConverterClient.Test.ViewModels;

[TestFixture]
public class CurrencyToWordConverterVmTest {

    private CurrencyToWordConverterVm currencyToWordConverterVm;
    private CurrencyToWordConverterStub currencyToWordConverterSpy;

    [SetUp]
    public void SetUp() {
        
        currencyToWordConverterSpy = A.Fake<CurrencyToWordConverterStub>(option => option.Wrapping(new CurrencyToWordConverterStub()));
        var userWorkflow = new UserWorkflows(A.Fake<ILogger<UserWorkflows>>(), currencyToWordConverterSpy);
        currencyToWordConverterVm = new CurrencyToWordConverterVm(userWorkflow);
    }

    [Test]
    public void OnCurrencyToWordConverterVm_WhenDollarsAndCentsAreChanged_ThenWordRepresentationShouldChangeToo() {

        currencyToWordConverterSpy.AddWordRepresentation(new Currency("1234", "56"), new CurrencyWordRepresentation("one thousand two hundred thirty-four dollars and fifty-six cents"));
        currencyToWordConverterSpy.AddWordRepresentation(new Currency("1234", "00"), new CurrencyWordRepresentation("one thousand two hundred thirty-four dollars"));


        // VM init values are Dollars: "100", Cents: "00", WordRepresentation: "one hundred dollars"
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("one hundred dollars"));

        currencyToWordConverterVm.Dollars = "1234";
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("one thousand two hundred thirty-four dollars"));

        currencyToWordConverterVm.Cents = "56";
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("one thousand two hundred thirty-four dollars and fifty-six cents"));
    }

    [Test]
    public void OnCurrencyToWordConverterVm_WhenDollarsAreChangedButCalledServiceThrowsConnectionError_ThenDisplayConnectionErrorText() {

        // VM init values are Dollars: "100", Cents: "00", WordRepresentation: "one hundred dollars"
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("one hundred dollars"));

        currencyToWordConverterVm.Dollars = "1234";
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("Could not contact the needed backend service. Please check if the service is running or the connection string is set correctly."));
    }

    [Test]
    public void OnCurrencyToWordConverterVm_WhenDollarsAreChangedButSomeUnforeseenExceptionIsThrown_ThenDisplayUnforeseenErrorText() {

        A.CallTo(() => currencyToWordConverterSpy.GetWordRepresentationOfCurrency(A<Currency>.Ignored)).Throws(new Exception());

        // VM init values are Dollars: "100", Cents: "00", WordRepresentation: "one hundred dollars"
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("one hundred dollars"));

        currencyToWordConverterVm.Dollars = "1234";
        Assert.That(currencyToWordConverterVm.WordRepresentation, Is.EqualTo("Something complete unforeseen happend. Please contact the support!"));
    }
}
