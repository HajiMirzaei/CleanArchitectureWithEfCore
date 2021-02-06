using AutoMapper;
using SampleProject.Core.Contracts;
using SampleProject.Core.DTOs;
using SampleProject.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Core.UseCases
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        //private readonly IMapper _mapper;
        public CourseService(IUnitOfWork uow)
        {
            _uow = uow;
            //_mapper = mapper;
        }

        public async Task<List<string>> RegisterCourseAsync(RegisterCourseInput arg)
        {
            var student = await _uow.StudentRepository.GetStudentWithRegisteredCourses(arg.StudentId);

            var errors = new List<string>();
            foreach (var item in arg.SelectedCourseCodes)
            {
                var course = await _uow.CourseRepository.GetByIdAsync(item);

                var res = student.RegisterForCourse(course);

                if (!res)
                {
                    errors.Add($"unable to register for {course?.Name}");
                }
                else
                {
                    var registerdCourse = new RegisteredCourse()
                    {
                        StudentId = arg.StudentId,
                        CourseId = course.Id
                    };
                    await _uow.RegisteredCourseRepository.AddAsync(registerdCourse);
                    await _uow.CommitAsync();
                }
            }
            return errors;
            //return new RegisterCourseOutput(!errors.Any(), errors, errors.Any() ? null : "Congratulates");
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var data = await _uow.CourseRepository.GetAllAsync(null, x => x.OrderByDescending(x => x.Id));
            return data;
            //var result = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseVM>>(data);
            //return new GetAllCoursesOutput(true, result);
        }
    }
}