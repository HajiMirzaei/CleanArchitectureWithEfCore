using AutoMapper;
using Moq;
using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Core.DTOs;
using SampleProject.Core.Entities;
using SampleProject.WebApi.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.WebApi.Test
{
    public class CourseController_UnitTest
    {
        private CourseController _courseController;
        private Mock<ICourseService> _courseServiceMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _mapperMock = new Mock<IMapper>();
            _courseController = new CourseController(_courseServiceMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Should_Return_GetAllCoursesOutput_Instance()
        {
            // Arrange
            IEnumerable<Course> courseList = new List<Course>();
            _courseServiceMock.Setup(p => p.GetAllAsync()).Returns(Task.FromResult(courseList));

            // Act
            var result = await _courseController.Get();

            // Assert
            Assert.IsAssignableFrom<GetAllCoursesOutput>(result);
        }

        [Test]
        public async Task Should_Call_GetAllAsync_Once()
        {
            // Arrange
            IEnumerable<Course> courseList = new List<Course>();
            _courseServiceMock.Setup(p => p.GetAllAsync()).Returns(Task.FromResult(courseList));

            // Act
            var result = await _courseController.Get();

            // Assert
            _courseServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Return_RegisterCourseOutput_Instance()
        {
            // Arrange
            List<string> errors = new List<string>();
            _courseServiceMock.Setup(p => p.RegisterCourseAsync(It.IsAny<RegisterCourseInput>())).Returns(Task.FromResult(errors));

            // Act
            var result = await _courseController.RegisterCourse(It.IsAny<RegisterCourseInput>());

            // Assert
            Assert.IsAssignableFrom<RegisterCourseOutput>(result);
        }

        [Test]
        public async Task Should_Call_RegisterCourseAsync_Once()
        {
            // Arrange
            List<string> errors = new List<string>();
            _courseServiceMock.Setup(p => p.RegisterCourseAsync(It.IsAny<RegisterCourseInput>())).Returns(Task.FromResult(errors));

            // Act
            var result = await _courseController.RegisterCourse(It.IsAny<RegisterCourseInput>());

            // Assert
            _courseServiceMock.Verify(x => x.RegisterCourseAsync(It.IsAny<RegisterCourseInput>()), Times.Once);
        }

        [Test]
        public async Task Should_Return_Congratulates()
        {
            // Arrange
            List<string> errors = new List<string>();
            _courseServiceMock.Setup(p => p.RegisterCourseAsync(It.IsAny<RegisterCourseInput>())).Returns(Task.FromResult(errors));

            // Act
            var result = await _courseController.RegisterCourse(It.IsAny<RegisterCourseInput>());

            // Assert
            Assert.IsTrue(result.Message == "Congratulates");
        }
    }
}