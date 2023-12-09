using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmpolyees.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class LibrariesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public LibrariesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetLibraries()
        {
            var companies = await _repository.Library.GetAllLibrariesAsync(trackChanges: false);
            return Ok(companies);
        }
    }
}
