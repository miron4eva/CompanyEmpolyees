using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private ILibraryRepository _libraryRepository;
        private IReaderRepository _readerRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }

        public ILibraryRepository Library
        {
            get
            {
                if(_libraryRepository == null)
                    _libraryRepository = new LibraryRepository(_repositoryContext);
                return _libraryRepository;
            }
        }

        public IReaderRepository Reader
        {
            get
            {
                if (_readerRepository == null)
                    _readerRepository = new ReaderRepository(_repositoryContext);
                return _readerRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
