using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyEmpolyees.ActionFilters
{
    public class ValidateReaderForLibraryExistAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateReaderForLibraryExistAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var libraryId = (Guid)context.ActionArguments["libraryId"];
            var library = await _repository.Library.GetLibraryAsync(libraryId, false);
            if (library == null)
            {
                _logger.LogInfo($":Library with id: {libraryId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var reader = await _repository.Reader.GetReaderAsync(libraryId, id, trackChanges);
            if (reader == null)
            {
                _logger.LogInfo($"Reader with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("reader", reader);
                await next();
            }
        }
    }
}
