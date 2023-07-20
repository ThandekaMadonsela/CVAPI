using AutoMapper;
using CVAPI.Dto;
using CVAPI.Models;

namespace CVAPI.AutoMapperOrHelper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<Student, CVDto>();
        }
    }
}
