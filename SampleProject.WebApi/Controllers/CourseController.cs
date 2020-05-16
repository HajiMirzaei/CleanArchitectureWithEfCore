using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core.Contracts;
using SampleProject.Core.DTOs;
using SampleProject.Core.Entities;

namespace SampleProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        protected readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<GetAllCoursesOutput> Get()
        {
            var data = await _courseService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseVM>>(data);
            return new GetAllCoursesOutput(true, result);
        }

        [HttpPost("[action]")]
        public async Task<RegisterCourseOutput> RegisterCourse(RegisterCourseInput args)
        {
            var result = await _courseService.RegisterCourseAsync(args);
            return new RegisterCourseOutput(!result.Any(), result, result.Any() ? null : "Congratulates");
        }
    }
}