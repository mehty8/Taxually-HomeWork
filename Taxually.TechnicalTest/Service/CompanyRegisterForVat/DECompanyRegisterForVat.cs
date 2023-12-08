using System.Xml.Serialization;
using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Service.Client;

namespace Taxually.TechnicalTest.Service.CompanyRegisterForVat;

public class DECompanyRegisterForVat : ICompanyRegisterForVat
{
    private static string _countryName = "DE";

    private ITaxuallyQueueClient _taxuallyQueueClient;
    

    public DECompanyRegisterForVat(ITaxuallyQueueClient taxuallyQueueClient)
    {
        _taxuallyQueueClient = taxuallyQueueClient;
    }

    public bool IsThisRegistrationNeeded(string countryName)
    {
        return _countryName.Equals(countryName);
    }

    public async Task Registration(VatRegistrationRequestDto vatRegistrationRequestDto)
    {
        await using StringWriter stringWriter = new(); 
        XmlSerializer serializer = new(typeof(VatRegistrationRequestDto)); 
        serializer.Serialize(stringWriter, vatRegistrationRequestDto);
        string xml = stringWriter.ToString();
        await _taxuallyQueueClient.EnqueueAsync("vat-registration-xml", xml);
    }
}