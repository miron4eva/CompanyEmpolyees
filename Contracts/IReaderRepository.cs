using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> GetReadersAsync(Guid libraryId, bool trackChanges);
        Task<Reader> GetReaderAsync(Guid libraryId, Guid id, bool trackChanges);
        void CreateReaderForLibrary(Guid libraryId, Reader reader);
        void DeleteReader(Reader reader);
    }
}
