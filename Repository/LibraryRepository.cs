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
    }
}
