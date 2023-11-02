using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
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
        public async Task<PagedList<Reader>> GetReadersAsync(Guid libraryId, ReaderParameters readerParameters, bool trackChanges)
        {
            var readers = await FindByCondition(e => e.LibraryId.Equals(libraryId), trackChanges)
                .OrderBy(e => e.Name)
                .ToListAsync();
            return PagedList<Reader>.ToPagedList(readers, readerParameters.PageNumber, readerParameters.PageSize);
        }
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
