using AutoMapper;
using Domain;
using Services.Dtos;

namespace Services.AutomapperProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
        }
    }
}
