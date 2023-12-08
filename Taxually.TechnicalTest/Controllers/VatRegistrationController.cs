using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Model;
using Taxually.TechnicalTest.Service.CompanyRegisterForVat;


namespace Taxually.TechnicalTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VatRegistrationController : ControllerBase
{
    private List<ICompanyRegisterForVat> _companyRegisterForVats;
    private ILogger<VatRegistrationController> _logger;

    public VatRegistrationController(IEnumerable<ICompanyRegisterForVat> companyRegisterForVats, 
        ILogger<VatRegistrationController> logger)
    {
        _companyRegisterForVats = companyRegisterForVats.ToList();
        _logger = logger;
    }
    

    [HttpPost]
    public async Task<ActionResult> RegisterCompanyForVat([FromBody] VatRegistrationRequestDto vatRegistrationRequestDto)
    {
        var companyRegisterForVat = _companyRegisterForVats.FirstOrDefault(registration
            => registration.IsThisRegistrationNeeded(vatRegistrationRequestDto.Country));
            
        if (companyRegisterForVat == null)
        {
            return BadRequest("Country not supported");
        }
            
        try
        {
            await companyRegisterForVat.Registration(vatRegistrationRequestDto);
            
            return Ok("Company registered for VAT");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception during VAT registration");
            
            return StatusCode(500, "Internal Server Error");
        }
    }
}

