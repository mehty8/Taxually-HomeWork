using Moq;
using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Service.Client;
using Taxually.TechnicalTest.Service.CompanyRegisterForVat;

namespace TestHomeWork;

public class UnitTests
{
    [Test]
    public async Task RegistrationEnqueuesXml()
    {
        var taxuallyQueueClientMock = new Mock<ITaxuallyQueueClient>();
        var deCompanyRegisterForVat = new DECompanyRegisterForVat(taxuallyQueueClientMock.Object);

        VatRegistrationRequestDto vatRegistrationRequestDto = new()
        {
            CompanyName = "FestiWall",
            CompanyId = "5",
            Country = "DE"
        };
        
        await deCompanyRegisterForVat.Registration(vatRegistrationRequestDto);
        
        taxuallyQueueClientMock.Verify(
            client => client.EnqueueAsync("vat-registration-xml", It.IsAny<string>()),
            Times.Once);
    }
    
    
    [Test]
    public async Task RegistrationEnqueuesCsvWithCorrectContent()
    {
        var taxuallyQueueClientMock = new Mock<ITaxuallyQueueClient>();
        var frCompanyRegisterForVat = new FRCompanyRegisterForVat(taxuallyQueueClientMock.Object);

        VatRegistrationRequestDto vatRegistrationRequestDto = new()
        {
            CompanyName = "SugarFrog",
            CompanyId = "blubli",
            Country = "FR"
        };
        
        await frCompanyRegisterForVat.Registration(vatRegistrationRequestDto);
        
        taxuallyQueueClientMock.Verify(
            client => client.EnqueueAsync("vat-registration-csv", It.IsAny<byte[]>()),
            Times.Once);
    }
    
    
    [Test]
    public async Task RegistrationSendsHttpPostToCorrectUrl()
    {
        var taxuallyHttpClientMock = new Mock<ITaxuallyHttpClient>();
        var gbCompanyRegisterForVat = new GBCompanyRegisterForVat(taxuallyHttpClientMock.Object);

        VatRegistrationRequestDto vatRegistrationRequestDto = new()
        {
            CompanyName = "BloodyWeather",
            CompanyId = "scooby",
            Country = "GB"
        };

        await gbCompanyRegisterForVat.Registration(vatRegistrationRequestDto);

        taxuallyHttpClientMock.Verify(
            client => client.PostAsync("https://api.uktax.gov.uk", vatRegistrationRequestDto),
            Times.Once);
    }

    
    [Test]
    public void RegistrationHandlesExceptionDuringHttpPost()
    {
        var taxuallyHttpClientMock = new Mock<ITaxuallyHttpClient>();
        
        taxuallyHttpClientMock.Setup(client => client.PostAsync(It.IsAny<string>(),
                It.IsAny<VatRegistrationRequestDto>()))
            .ThrowsAsync(new Exception("Simulated exception during HTTP post"));
        
        var gbCompanyRegisterForVat = new GBCompanyRegisterForVat(taxuallyHttpClientMock.Object);
        
        VatRegistrationRequestDto vatRegistrationRequestDto = new()
        {
            CompanyName = "BloodyWeather",
            CompanyId = "scooby",
            Country = "GB"
        };
        
        Assert.ThrowsAsync<Exception>(() => gbCompanyRegisterForVat.Registration(vatRegistrationRequestDto));
        
        taxuallyHttpClientMock.Verify(
            client => client.PostAsync("https://api.uktax.gov.uk", vatRegistrationRequestDto),
            Times.Once);
    }

}