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

namespace SampleProject.Core.Test
{
    public class CourseService_UnitTest
    {
        private CourseService _courseService;
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IStudent> _studentMock;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _studentMock = new Mock<IStudent>();
            _courseService = new CourseService(_uowMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Should_Return_RegisterCourseOutput_Instance()
        {
            // Arrange
            _uowMock.Setup(p => p.StudentRepository.GetStudentWithRegisteredCourses(It.IsAny<int>())).Returns(Task.FromResult(new Student()));
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
            Assert.IsAssignableFrom<RegisterCourseOutput>(result);
        }

        [Test]
        public async Task Should_Not_Register_Course()
        {
            // Arrange
            _uowMock.Setup(p => p.StudentRepository.GetStudentWithRegisteredCourses(It.IsAny<int>())).Returns(Task.FromResult(new Student()));
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
            Assert.IsAssignableFrom<RegisterCourseOutput>(result);
        }
    }
}
