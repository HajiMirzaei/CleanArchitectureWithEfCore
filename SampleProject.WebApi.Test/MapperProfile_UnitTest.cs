using AutoMapper;
using NUnit.Framework;
using SampleProject.WebApi.Mapper;
using SampleProject.Core.Entities;
using SampleProject.Core.DTOs;
using System.Linq;

namespace SampleProject.WebApi.Test
{
    public class MapperProfile_UnitTest
    {
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Test]
        public void Should_ReverseMap_Course_CourseVM()
        {
            var maps = _mapper.ConfigurationProvider.GetAllTypeMaps();
            System.Collections.Generic.List<TypeMap> typemaps = new System.Collections.Generic.List<TypeMap>();
            foreach(var map in maps)
            {
                typemaps.Add(map);
            }

            Assert.That(typemaps.Any(x => x.Types.SourceType == typeof(Course) && x.Types.DestinationType == typeof(CourseVM)));
            Assert.That(typemaps.Any(x => x.Types.SourceType == typeof(CourseVM) && x.Types.DestinationType == typeof(Course)));
        }
    }
}