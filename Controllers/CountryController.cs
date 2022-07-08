using AutoMapper;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        //// adding cashe
        // another way of declaring chaching specifically
        [HttpCacheExpiration(CacheLocation  = CacheLocation.Public, MaxAge = 50)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ResponseCache(CacheProfileName = "SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountries([FromQuery] RequestParams requestParams)
        {
            var countries = await _unitOfWork.Countries.GetAllPaged(requestParams);
            var results = _mapper.Map<IList<CountryDTO>>(countries);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetCountry")]
        /// declaring caching annotation
        [ResponseCache(CacheProfileName = "SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
            var result = _mapper.Map<CountryDTO>(country);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateHotelDTO)}");
                return BadRequest(ModelState);
            }

            var country = _mapper.Map<Country>(countryDTO);

            await _unitOfWork.Countries.Insert(country);

            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(long id, [FromBody] UpdateCountryDTO updateCountry)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attemp in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            var country = await _unitOfWork.Countries.Get(option => option.Id == id, new List<string> { "Hotels" });
            if (country == null)
            {
                _logger.LogError($"Invalid Update attemp in {nameof(UpdateCountry)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(updateCountry, country);
            _unitOfWork.Countries.Update(country);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(long id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid ID attemp in {nameof(DeleteCountry)}");
                return BadRequest(ModelState);
            }
            var country = await _unitOfWork.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
            if (country == null)
            {
                _logger.LogError($"Invalid Delete attemp in {nameof(DeleteCountry)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Countries.Delete(country.Id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }


    }

}
