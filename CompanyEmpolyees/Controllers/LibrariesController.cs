using AutoMapper;
using CompanyEmpolyees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public LibrariesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetLibraries()
        {
            var libraries = _repository.Library.GetAllLibraries(trackChanges: false);
            var librariesDto = _mapper.Map<IEnumerable<LibraryDto>>(libraries);
            return Ok(librariesDto);
        }
        [HttpGet("{id}", Name = "LibraryById")]
        public IActionResult GetLibrary(Guid id)
        {
            var linrary = _repository.Library.GetLibrary(id, trackChanges: false);
            if (linrary == null)
            {
                _logger.LogInfo($"Library with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var libraryDto = _mapper.Map<LibraryDto>(linrary);
                return Ok(libraryDto);
            }
        }
        [HttpPost]
        public IActionResult CreateLibrary([FromBody] LibraryForCreationDto library)
        {
            if (library == null)
            {
                _logger.LogError("LibraryForCreationDto object sent from client is null.");
                return BadRequest("LibraryForCreationDto object is null");
            }
            var libraryEntity = _mapper.Map<Library>(library);
            _repository.Library.CreateLibrary(libraryEntity);
            _repository.Save();
            var libraryToReturn = _mapper.Map<CompanyDto>(libraryEntity);
            return CreatedAtRoute("LibraryById", new { id = libraryToReturn.Id },libraryToReturn);
        }
        [HttpGet("collection/({ids})", Name = "LibraryCollection")]
        public IActionResult GetLibraryCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var libraryEntities = _repository.Library.GetByIds(ids, trackChanges: false);
            if (ids.Count() != libraryEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var librariesToReturn = _mapper.Map<IEnumerable<LibraryDto>>(libraryEntities);
            return Ok(librariesToReturn);

        }
        [HttpPost("collection")]
        public IActionResult CreateLibraryCollection([FromBody] IEnumerable<LibraryForCreationDto> libraryCollection)
        {
            if (libraryCollection == null)
            {
                _logger.LogError("Library collection sent from client is null.");
                return BadRequest("Library collection is null");
            }
            var libraryEntities = _mapper.Map<IEnumerable<Library>>(libraryCollection);
            foreach (var library in libraryEntities)
            {
                _repository.Library.CreateLibrary(library);
            }
            _repository.Save();
            var libraryCollectionToReturn = _mapper.Map<IEnumerable<LibraryDto>>(libraryEntities);
            var ids = string.Join(",", libraryCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("LibraryCollection", new { ids }, libraryCollectionToReturn);
        }
    }
}
