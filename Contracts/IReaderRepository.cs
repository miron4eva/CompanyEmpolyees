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
        IEnumerable<Reader> GetReaders(Guid libraryId, bool trackChanges);
        Reader GetReader(Guid libraryId, Guid id, bool trackChanges);
        void CreateReaderForLibrary(Guid libraryId, Reader reader);

    }
}
