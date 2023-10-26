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
        public IActionResult GetReadersForLibrary(Guid libraryId)
        {
            var library = _repository.Library.GetLibrary(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readersFromDb = _repository.Reader.GetReaders(libraryId, trackChanges: false);
            var readersDto = _mapper.Map<IEnumerable<ReaderDto>>(readersFromDb);
            return Ok(readersDto);
        }
        [HttpGet("{id}", Name = "GetReaderForLibrary")]
        public IActionResult GetReaderForLibrary(Guid libraryId, Guid id)
        {
            var library = _repository.Library.GetLibrary(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerDb = _repository.Reader.GetReader(libraryId, id, trackChanges: false);
            if (readerDb == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var reader = _mapper.Map<ReaderDto>(readerDb);
            return Ok(reader);
        }
        [HttpPost]
        public IActionResult CreateReaderForLibrary(Guid libraryId, [FromBody] ReaderForCreationDto reader)
        {
            if (reader == null)
            {
                _logger.LogError("ReaderForCreationDto object sent from client is null.");
                return BadRequest("ReaderForCreationDto object is null");
            }
            var library = _repository.Library.GetLibrary(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerEntity = _mapper.Map<Reader>(reader);
            _repository.Reader.CreateReaderForLibrary(libraryId, readerEntity);
            _repository.Save();
            var readerToReturn = _mapper.Map<ReaderDto>(readerEntity);
            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                libraryId,
                id = readerToReturn.Id
            }, readerToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReaderForLibrary(Guid libraryId, Guid id)
        {
            var library = _repository.Company.GetCompany(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerForLibrary = _repository.Reader.GetReader(libraryId, id, trackChanges: false);
            if (readerForLibrary == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Reader.DeleteReader(readerForLibrary);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateReaderForLibrary(Guid libraryId, Guid id, [FromBody] ReaderForUpdateDto reader)
        {
            if (reader == null)
            {
                _logger.LogError("ReaderForUpdateDto object sent from client is null.");
                return BadRequest("ReaderForUpdateDto object is null");
            }
            var library = _repository.Library.GetLibrary(libraryId, trackChanges: false);
            if (library == null)
            {
                _logger.LogInfo($"Library with id: {libraryId} doesn't exist in the database.");
                return NotFound();
            }
            var readerEntity = _repository.Reader.GetReader(libraryId, id, trackChanges: true);
            if (readerEntity == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(reader, readerEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
