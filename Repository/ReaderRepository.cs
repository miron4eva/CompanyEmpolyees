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
    public class ReaderRepository : RepositoryBase<Reader>, IReaderRepository
    {
        public ReaderRepository(RepositoryContext repositoryContext) : base(repositoryContext){}
        public async Task<IEnumerable<Reader>> GetReadersAsync(Guid libraryId, bool trackChanges) => await FindByCondition(e => e.LibraryId.Equals(libraryId), trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
        public async Task<Reader> GetReaderAsync(Guid libraryId, Guid id, bool trackChanges) => await FindByCondition(e => e.LibraryId.Equals(libraryId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        public void CreateReaderForLibrary(Guid libraryId, Reader reader)
        {
            reader.LibraryId = libraryId;
            Create(reader);
        }
        public void DeleteReader(Reader reader)
        {
            Delete(reader);
        }
    }
}
