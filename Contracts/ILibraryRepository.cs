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
        IEnumerable<Library> GetAllLibraries(bool trackChanges);
        Library GetLibrary(Guid libraryId, bool trackChanges);
        void CreateLibrary(Library library);
        IEnumerable<Library> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

    }
}
