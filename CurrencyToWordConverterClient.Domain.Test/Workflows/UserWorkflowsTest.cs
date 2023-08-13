using Castle.Core.Logging;
using CurrencyToWordConverterClient.Domain.DomainErrors;
using CurrencyToWordConverterClient.Domain.DomainTypes;
using CurrencyToWordConverterClient.Domain.Test.Stubs;
using CurrencyToWordConverterClient.Domain.Workflows;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace CurrencyToWordConverterClient.Domain.Test.Workflows;

[TestFixture]
public class UserWorkflowsTest {

    private UserWorkflows userWorkflows;

    private CurrencyToWordConverterStub currencyToWordConverterStub;

    [SetUp] 
    public void SetUp() { 
        currencyToWordConverterStub = new CurrencyToWordConverterStub();
        userWorkflows = new UserWorkflows(A.Fake<ILogger<UserWorkflows>>(), currencyToWordConverterStub);
    }

    [Test]
    public async Task AddWordRepresentation_WhenExternalServiceHasWordRepresentation_ThenReturnWordRepresentation() {

        currencyToWordConverterStub.AddWordRepresentation(new Currency("1234", "56"), new CurrencyWordRepresentation("one thousand two hundred thirty-four dollars and fifty-six cents"));


        var result = await userWorkflows.GetWordRepresentationOfGivenDollarAndCents("1234", "56");
        Assert.That(result.Value, Is.EqualTo("one thousand two hundred thirty-four dollars and fifty-six cents"));
    }

    [Test]
    public void AddWordRepresentation_WhenExternalServiceThrowsConnectionError_ThenThrowConnectionError() {

        Assert.That(()=>userWorkflows.GetWordRepresentationOfGivenDollarAndCents("1234", "56"),Throws.Exception.InstanceOf<ConnectionError>());
    }
}
