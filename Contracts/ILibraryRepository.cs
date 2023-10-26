using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Library>> GetAllLibrariesAsync(bool trackChanges);
        Task<Library> GetLibraryAsync(Guid libraryId, bool trackChanges);
        void CreateLibrary(Library library);
        Task<IEnumerable<Library>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteLibrary(Library library);
    }
}
