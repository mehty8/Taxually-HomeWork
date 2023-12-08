using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Service.Client;

namespace Taxually.TechnicalTest.Service.CompanyRegisterForVat;

public class GBCompanyRegisterForVat : ICompanyRegisterForVat
{
    private static string _countryName = "GB";

    private ITaxuallyHttpClient _taxuallyHttpClient;
    

    public GBCompanyRegisterForVat(ITaxuallyHttpClient taxuallyHttpClient)
    {
        _taxuallyHttpClient = taxuallyHttpClient;
    }

    public bool IsThisRegistrationNeeded(string countryName)
    {
        return _countryName.Equals(countryName);
    }

    public async Task Registration(VatRegistrationRequestDto vatRegistrationRequestDto)
    {
        await _taxuallyHttpClient.PostAsync("https://api.uktax.gov.uk", vatRegistrationRequestDto);
    }
}