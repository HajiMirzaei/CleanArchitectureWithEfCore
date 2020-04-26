using AutoMapper;
using SampleProject.Core.DTOs;
using SampleProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.WebApi
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseVM>().ReverseMap();
        }
    }
}
