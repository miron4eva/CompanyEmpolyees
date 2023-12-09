using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace CompanyEmpolyees
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Library, LibraryDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Reader, ReaderDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<LibraryForCreationDto, Library>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<ReaderForCreationDto, Reader>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<ReaderForUpdateDto, Reader>().ReverseMap();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<LibraryForUpdateDto, Library>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
