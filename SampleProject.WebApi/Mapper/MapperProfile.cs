using AutoMapper;
using SampleProject.Core.DTOs;
using SampleProject.Core.Entities;

namespace SampleProject.WebApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseVM>().ReverseMap();
        }
    }
}
