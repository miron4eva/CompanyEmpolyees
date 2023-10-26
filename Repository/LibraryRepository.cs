using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
    {
        public LibraryRepository(RepositoryContext repositoryContext): base(repositoryContext){}
        public async Task<IEnumerable<Library>> GetAllLibrariesAsync(bool trackChanges) => await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
        public async Task<Library> GetLibraryAsync(Guid libraryId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(libraryId), trackChanges).SingleOrDefaultAsync();
        public void CreateLibrary(Library library) => Create(library);
        public async Task<IEnumerable<Library>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
        public void DeleteLibrary(Library library)
        {
            Delete(library);
        }
    }
}
