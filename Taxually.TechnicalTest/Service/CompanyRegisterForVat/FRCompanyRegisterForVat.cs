using System.Text;
using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Service.Client;

namespace Taxually.TechnicalTest.Service.CompanyRegisterForVat;

public class FRCompanyRegisterForVat : ICompanyRegisterForVat
{
    private static string _countryName = "FR";

    private ITaxuallyQueueClient _taxuallyQueueClient;
    

    public FRCompanyRegisterForVat(ITaxuallyQueueClient taxuallyQueueClient)
    {
        _taxuallyQueueClient = taxuallyQueueClient;
    }

    public bool IsThisRegistrationNeeded(string countryName)
    {
        return _countryName.Equals(countryName);
    }

    public async Task Registration(VatRegistrationRequestDto vatRegistrationRequestDto)
    {
        StringBuilder csvBuilder = new();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{vatRegistrationRequestDto.CompanyName}{vatRegistrationRequestDto.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
        await _taxuallyQueueClient.EnqueueAsync("vat-registration-csv", csv);
    }
}