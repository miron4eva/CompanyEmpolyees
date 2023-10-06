using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
    }
}
