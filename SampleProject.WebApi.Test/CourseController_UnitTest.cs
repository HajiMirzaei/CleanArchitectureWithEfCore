using Moq;
using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Core.DTOs;
using SampleProject.WebApi.Controllers;
using System.Threading.Tasks;

namespace SampleProject.WebApi.Test
{
    public class CourseController_UnitTest
    {
        private CourseController _courseController;
        private Mock<ICourseService> _courseServiceMock;

        [SetUp]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _courseController = new CourseController(_courseServiceMock.Object);
        }

        [Test]
        public async Task Should_Return_GetAllCoursesOutput_Instance()
        {
            // Arrange
            _courseServiceMock.Setup(p => p.GetAllAsync()).Returns(Task.FromResult(new GetAllCoursesOutput(true, null)));

            // Act
            var result = await _courseController.Get();

            // Assert
            Assert.IsAssignableFrom<GetAllCoursesOutput>(result);
        }

        [Test]
        public async Task Should_Call_GetAllAsync_Once()
        {
            // Arrange
            _courseServiceMock.Setup(p => p.GetAllAsync()).Returns(Task.FromResult(new GetAllCoursesOutput(true, null)));

            // Act
            var result = await _courseController.Get();

            // Assert
            _courseServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Return_RegisterCourseOutput_Instance()
        {
            // Arrange
            _courseServiceMock.Setup(p => p.RegisterCourseAsync(It.IsAny<RegisterCourseInput>())).Returns(Task.FromResult(new RegisterCourseOutput(true, null)));

            // Act
            var result = await _courseController.RegisterCourse(It.IsAny<RegisterCourseInput>());

            // Assert
            Assert.IsAssignableFrom<RegisterCourseOutput>(result);
        }

        [Test]
        public async Task Should_Call_RegisterCourseAsync_Once()
        {
            // Arrange
            _courseServiceMock.Setup(p => p.RegisterCourseAsync(It.IsAny<RegisterCourseInput>())).Returns(Task.FromResult(new RegisterCourseOutput(true, null)));

            // Act
            var result = await _courseController.RegisterCourse(It.IsAny<RegisterCourseInput>());

            // Assert
            _courseServiceMock.Verify(x => x.RegisterCourseAsync(It.IsAny<RegisterCourseInput>()), Times.Once);
        }
    }
}