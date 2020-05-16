using NUnit.Framework;
using SampleProject.Core.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using SampleProject.Core.UseCases;
using SampleProject.Core.Entities;
using SampleProject.Core.DTOs;
using System.Linq;
using System.Linq.Expressions;

namespace SampleProject.Core.Test
{
    public class CourseService_UnitTest
    {
        private CourseService _courseService;
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IStudent> _studentMock;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _studentMock = new Mock<IStudent>();
            _courseService = new CourseService(_uowMock.Object);
        }

        [Test]
        public async Task Should_Return_List_String_Instance()
        {
            // Arrange
            _uowMock.Setup(p => p.StudentRepository.GetStudentWithRegisteredCourses(It.IsAny<int>())).Returns(Task.FromResult(_studentMock.Object));
            _uowMock.Setup(p => p.CourseRepository.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Course()));
            _studentMock.Setup(p => p.RegisterForCourse(It.IsAny<Course>())).Returns(true);
            _uowMock.Setup(p => p.RegisteredCourseRepository.AddAsync(It.IsAny<RegisteredCourse>()));

            var input = new RegisterCourseInput()
            {
                StudentId = 1,
                SelectedCourseCodes = new List<int>()
            };

            // Act
            var result = await _courseService.RegisterCourseAsync(input);

            // Assert
            Assert.IsAssignableFrom<List<string>>(result);
        }

        [Test]
        public async Task Should_Not_Register_Course()
        {
            // Arrange
            _uowMock.Setup(p => p.StudentRepository.GetStudentWithRegisteredCourses(It.IsAny<int>())).Returns(Task.FromResult(_studentMock.Object));
            _uowMock.Setup(p => p.CourseRepository.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Course()));
            _studentMock.Setup(p => p.RegisterForCourse(It.IsAny<Course>())).Returns(false);
            _uowMock.Setup(p => p.RegisteredCourseRepository.AddAsync(It.IsAny<RegisteredCourse>()));
            var input = new RegisterCourseInput()
            {
                StudentId = 1,
                SelectedCourseCodes = new List<int> { 1, 2 }
            };

            // Act
            var result = await _courseService.RegisterCourseAsync(input);

            // Assert
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task Should_Register_Course()
        {
            // Arrange
            _uowMock.Setup(p => p.StudentRepository.GetStudentWithRegisteredCourses(It.IsAny<int>())).Returns(Task.FromResult(_studentMock.Object));
            _uowMock.Setup(p => p.CourseRepository.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Course()));
            _studentMock.Setup(p => p.RegisterForCourse(It.IsAny<Course>())).Returns(true);
            _uowMock.Setup(p => p.RegisteredCourseRepository.AddAsync(It.IsAny<RegisteredCourse>()));
            var input = new RegisterCourseInput()
            {
                StudentId = 1,
                SelectedCourseCodes = new List<int> { 1, 2 }
            };

            // Act
            var result = await _courseService.RegisterCourseAsync(input);

            // Assert
            Assert.IsFalse(result.Any());
        }

        [Test]
        public async Task Should_Return_Unable_To_Register()
        {
            // Arrange
            var course = new Course
            {
                Id = 1,
                Name = "Javascript"
            };
            _uowMock.Setup(p => p.StudentRepository.GetStudentWithRegisteredCourses(It.IsAny<int>())).Returns(Task.FromResult(_studentMock.Object));
            _uowMock.Setup(p => p.CourseRepository.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(course));
            _studentMock.Setup(p => p.RegisterForCourse(It.IsAny<Course>())).Returns(false);
            _uowMock.Setup(p => p.RegisteredCourseRepository.AddAsync(It.IsAny<RegisteredCourse>()));
            var input = new RegisterCourseInput()
            {
                StudentId = 1,
                SelectedCourseCodes = new List<int> { 1 }
            };

            // Act
            var result = await _courseService.RegisterCourseAsync(input);

            // Assert
            Assert.IsTrue(result.Contains("unable to register for Javascript"));
        }

        [Test]
        public async Task Should_Return_GetAllCoursesOutput_Instance()
        {
            // Arrange
            IEnumerable<Course> courseList = new List<Course>();
            _uowMock.Setup(p => p.CourseRepository.GetAllAsync(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<Func<IQueryable<Course>, IOrderedQueryable<Course>>>(), It.IsAny<string>())).Returns(Task.FromResult(courseList));

            // Act
            var result = await _courseService.GetAllAsync();

            // Assert
            Assert.IsAssignableFrom<List<Course>>(result);
        }

        [Test]
        public async Task Should_Return_3_Record()
        {
            // Arrange
            IEnumerable<Course> courseList = new List<Course>()
            {
                new Course(){
                    Id = 1,
                    Name = "python",
                    Description = "Wow"
                },
                new Course(){
                    Id = 2,
                    Name = "javascript",
                    Description = "Wow"
                },
                new Course(){
                    Id = 3,
                    Name = "c#",
                    Description = "Wow"
                }
            };
            _uowMock.Setup(p => p.CourseRepository.GetAllAsync(It.IsAny<Expression<Func<Course, bool>>>(), It.IsAny<Func<IQueryable<Course>, IOrderedQueryable<Course>>>(), It.IsAny<string>())).Returns(Task.FromResult(courseList));

            // Act
            var result = await _courseService.GetAllAsync();

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }
    }
}
