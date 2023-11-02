using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IReaderRepository
    {
        Task<PagedList<Reader>> GetReadersAsync(Guid libraryId, ReaderParameters readerParameters, bool trackChanges);
        Task<Reader> GetReaderAsync(Guid libraryId, Guid id, bool trackChanges);
        void CreateReaderForLibrary(Guid libraryId, Reader reader);
        void DeleteReader(Reader reader);
    }
}
