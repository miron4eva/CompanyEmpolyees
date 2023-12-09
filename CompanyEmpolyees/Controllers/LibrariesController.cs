using AutoMapper;
using CompanyEmpolyees.ActionFilters;
using CompanyEmpolyees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet(Name = "GetLibraries"), Authorize(Roles = "Manager")]

        public async Task<IActionResult> GetLibrariesAsync()
        {
            var libraries = await _repository.Library.GetAllLibrariesAsync(trackChanges: false);
            var librariesDto = _mapper.Map<IEnumerable<LibraryDto>>(libraries);
            return Ok(librariesDto);
        }
        [HttpGet("{id}", Name = "LibraryById")]
        public async Task<IActionResult> GetLibraryAsync(Guid id)
        {
            var linrary = await _repository.Library.GetLibraryAsync(id, trackChanges: false);
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateLibrary([FromBody] LibraryForCreationDto library)
        {
            var libraryEntity = _mapper.Map<Library>(library);
            _repository.Library.CreateLibrary(libraryEntity);
            await _repository.SaveAsync();
            var libraryToReturn = _mapper.Map<CompanyDto>(libraryEntity);
            return CreatedAtRoute("LibraryById", new { id = libraryToReturn.Id },libraryToReturn);
        }
        [HttpGet("collection/({ids})", Name = "LibraryCollection")]
        public async Task<IActionResult> GetLibraryCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var libraryEntities = await _repository.Library.GetByIdsAsync(ids, trackChanges: false);
            if (ids.Count() != libraryEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var librariesToReturn = _mapper.Map<IEnumerable<LibraryDto>>(libraryEntities);
            return Ok(librariesToReturn);

        }
        [HttpPost("collection")]
        public async Task<IActionResult> CreateLibraryCollection([FromBody] IEnumerable<LibraryForCreationDto> libraryCollection)
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
            await _repository.SaveAsync();
            var libraryCollectionToReturn = _mapper.Map<IEnumerable<LibraryDto>>(libraryEntities);
            var ids = string.Join(",", libraryCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("LibraryCollection", new { ids }, libraryCollectionToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateLibraryExistsAttribute))]
        public async Task<IActionResult> DeleteLibrary(Guid id)
        {
            var library = HttpContext.Items["library"] as Library;
            _repository.Library.DeleteLibrary(library);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateLibraryExistsAttribute))]
        public async Task<IActionResult> UpdateLibrary(Guid id, [FromBody] LibraryForUpdateDto library)
        {
            var libraryEntity = HttpContext.Items["library"] as Library;
            _mapper.Map(library, libraryEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateLibraryExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid libraryId, Guid id, [FromBody] JsonPatchDocument<ReaderForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var readerEntity = HttpContext.Items["reader"] as Reader;
            var readerToPatch = _mapper.Map<ReaderForUpdateDto>(readerEntity);
            patchDoc.ApplyTo(readerToPatch, ModelState);
            TryValidateModel(readerToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(readerToPatch, readerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetLibrariesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
