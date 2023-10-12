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
    public class ReaderRepository : RepositoryBase<Reader>, IReaderRepository
    {
        public ReaderRepository(RepositoryContext repositoryContext) : base(repositoryContext){}
        public IEnumerable<Reader> GetReaders(Guid libraryId, bool trackChanges) => FindByCondition(e => e.LibraryId.Equals(libraryId), trackChanges)
            .OrderBy(e => e.Name);
        public Reader GetReader(Guid libraryId, Guid id, bool trackChanges) => FindByCondition(e => e.LibraryId.Equals(libraryId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();
    }
}
