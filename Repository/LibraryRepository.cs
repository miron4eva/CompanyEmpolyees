using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
    {
        public LibraryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public IEnumerable<Library> GetAllLibraries(bool trackChanges) => FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
        public Library GetLibrary(Guid libraryId, bool trackChanges) => FindByCondition(c => c.Id.Equals(libraryId), trackChanges).SingleOrDefault();
        public void CreateLibrary(Library library) => Create(library);
        public IEnumerable<Library> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();
        public void DeleteLibrary(Library library)
        {
            Delete(library);
        }
    }
}
