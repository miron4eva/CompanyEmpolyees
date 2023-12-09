using AutoMapper;
using CompanyEmpolyees.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmpolyees.Controllers
{
    [Route("api/libraries/{libraryId}/readers")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<ReaderDto> _dataShaper;

        public ReadersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<ReaderDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        [HttpGet]
        public async Task<IActionResult> GetReadersForLibrary(Guid libraryId, [FromQuery] ReaderParameters readerParameters)
        {
            var library = await _repository.Library.GetLibraryAsync(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readersFromDb = await _repository.Reader.GetReadersAsync(libraryId, readerParameters, trackChanges: false);
            var readersDto = _mapper.Map<IEnumerable<ReaderDto>>(readersFromDb);
            return Ok(_dataShaper.ShapeData(readersDto, readerParameters.Fields));
        }
        [HttpGet("{id}", Name = "GetReaderForLibrary")]
        public async Task<IActionResult> GetReaderForLibrary(Guid libraryId, Guid id)
        {
            var library = await _repository.Library.GetLibraryAsync(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerDb = await _repository.Reader.GetReaderAsync(libraryId, id, trackChanges: false);
            if (readerDb == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var reader = _mapper.Map<ReaderDto>(readerDb);
            return Ok(reader);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateReaderForLibrary(Guid libraryId, [FromBody] ReaderForCreationDto reader)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var library = await _repository.Library.GetLibraryAsync(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerEntity = _mapper.Map<Reader>(reader);
            _repository.Reader.CreateReaderForLibrary(libraryId, readerEntity);
            await _repository.SaveAsync();
            var readerToReturn = _mapper.Map<ReaderDto>(readerEntity);
            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                libraryId,
                id = readerToReturn.Id
            }, readerToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateReaderForLibraryExistAttribute))]
        public async Task<IActionResult> DeleteReaderForLibrary(Guid libraryId, Guid id)
        {
            var readerForLibrary = HttpContext.Items["reader"] as Reader;
            _repository.Reader.DeleteReader(readerForLibrary);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateReaderForLibraryExistAttribute))]
        public async Task<IActionResult> UpdateReaderForLibrary(Guid libraryId, Guid id, [FromBody] ReaderForUpdateDto reader)
        {
            var readerEntity = HttpContext.Items["reader"] as Reader;
            _mapper.Map(reader, readerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
