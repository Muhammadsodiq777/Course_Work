using HotelListing.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;

[ApiVersion("2.0", Deprecated = true)] //// first way of apiversion
///[Route("api/{v:apiversion}/country")] //// second way of api version
[Route("api/country")]
[ApiController]
public class CountryV2Controller : ControllerBase 
{
    private DatabaseContext _database;

    public CountryV2Controller(DatabaseContext database)
    {
        _database = database;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCountries()
    {
        return Ok(_database.Countries);
    }
}
