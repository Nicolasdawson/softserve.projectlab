using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly CountryService _service;

    public CountryController(CountryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _service.GetAllAsync();
        return Ok(countries);
    }

    [HttpGet("{id}/regions")]
    public async Task<IActionResult> GetRegionsByCountry(Guid id)
    {
        var regions = await _service.GetRegionsByCountryIdAsync(id);
        return Ok(regions);
    }
}
