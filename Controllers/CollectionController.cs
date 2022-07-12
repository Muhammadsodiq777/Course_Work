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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CollectionController> _logger;
        private readonly IMapper _mapper;

        public CollectionController(IUnitOfWork unitOfWork, ILogger<CollectionController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        //// adding cashe
        // another way of declaring chaching specifically
        /*[HttpCacheExpiration(CacheLocation  = CacheLocation.Public, MaxAge = 50)]
        [HttpCacheValidation(MustRevalidate = false)]*/
        [ResponseCache(CacheProfileName = "SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCollection([FromQuery] RequestParams requestParams)
        {
            var collections = await _unitOfWork.Collections.GetAllPaged(requestParams);
            var results = _mapper.Map<IList<CollectionDTO>>(collections);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetCollection")]
        /// declaring caching annotation
        [ResponseCache(CacheProfileName = "SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCollection(int id)
        {
            var collection = await _unitOfWork.Collections.Get(q => q.Id == id, new List<string> { "Files" });
            var result = _mapper.Map<CollectionDTO>(collection);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionDTO collectionDTO)
        {   
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateCollection)}");
                return BadRequest(ModelState);
            }

            var collection = _mapper.Map<Collection>(collectionDTO);

            await _unitOfWork.Collections.Insert(collection);

            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetCollection", new { id = collection.Id }, collection);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCollection(long id, [FromBody] UpdateCollectionDTO updateCollection)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attemp in {nameof(UpdateCollection)}");
                return BadRequest(ModelState);
            }

            var collection = await _unitOfWork.Collections.Get(option => option.Id == id, new List<string> { "Files" });
            if (collection == null)
            {
                _logger.LogError($"Invalid Update attemp in {nameof(UpdateCollection)}");
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(updateCollection, collection);
            _unitOfWork.Collections.Update(collection);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCollection(long id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid ID attemp in {nameof(DeleteCollection)}");
                return BadRequest(ModelState);
            }
            var collection = await _unitOfWork.Collections.Get(q => q.Id == id, new List<string> { "Files" });
            if (collection == null)
            {
                _logger.LogError($"Invalid Delete attemp in {nameof(DeleteCollection)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Collections.Delete(collection.Id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }


    }

}
