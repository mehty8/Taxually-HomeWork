using Taxually.TechnicalTest.Model;

namespace Taxually.TechnicalTest.Service.CompanyRegisterForVat;

public interface ICompanyRegisterForVat
{
    bool IsThisRegistrationNeeded(String countryName);

    Task Registration(VatRegistrationRequestDto vatRegistrationRequestDto);
}