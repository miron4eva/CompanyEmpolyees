using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

        public ReadersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetReadersForLibrary(Guid libraryId)
        {
            var library = await _repository.Library.GetLibraryAsync(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readersFromDb = await _repository.Reader.GetReadersAsync(libraryId, trackChanges: false);
            var readersDto = _mapper.Map<IEnumerable<ReaderDto>>(readersFromDb);
            return Ok(readersDto);
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
        public async Task<IActionResult> CreateReaderForLibrary(Guid libraryId, [FromBody] ReaderForCreationDto reader)
        {
            if (reader == null)
            {
                _logger.LogError("ReaderForCreationDto object sent from client is null.");
                return BadRequest("ReaderForCreationDto object is null");
            }
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
        public async Task<IActionResult> DeleteReaderForLibrary(Guid libraryId, Guid id)
        {
            var library = await _repository.Company.GetCompanyAsync(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerForLibrary = await _repository.Reader.GetReaderAsync(libraryId, id, trackChanges: false);
            if (readerForLibrary == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Reader.DeleteReader(readerForLibrary);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReaderForLibrary(Guid libraryId, Guid id, [FromBody] ReaderForUpdateDto reader)
        {
            if (reader == null)
            {
                _logger.LogError("ReaderForUpdateDto object sent from client is null.");
                return BadRequest("ReaderForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var library = await _repository.Library.GetLibraryAsync(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerEntity = await _repository.Reader.GetReaderAsync(libraryId, id, trackChanges: true);
            if (readerEntity == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(reader, readerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
