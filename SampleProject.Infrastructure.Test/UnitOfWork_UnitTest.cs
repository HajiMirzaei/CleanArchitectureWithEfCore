using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Infrastructure.Data;
using SampleProject.Infrastructure.IoC;
using System;
using System.Linq;

namespace SampleProject.Infrastructure.Test
{
    public class UnitOfWork_UnitTest
    {
        private UnitOfWork _unitOfWork;
        private Mock<IServiceProvider> _serviceProviderMock;
        private Mock<SampleProjectDbContext> _dbContextMock;

        [SetUp]
        public void Setup()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _dbContextMock = new Mock<SampleProjectDbContext>();
            _unitOfWork = new UnitOfWork(_dbContextMock.Object, _serviceProviderMock.Object);
        }

        [Test]
        public void Should_Return_StudentRepository()
        {
            // Arrange
            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IStudentRepository)))
                .Returns(new StudentRepository(_dbContextMock.Object));

            // Act
            var result = _unitOfWork.StudentRepository;

            // Assert
            Assert.IsAssignableFrom<StudentRepository>(result);
        }

        [Test]
        public void Should_Return_CourseRepository()
        {
            // Arrange
            _serviceProviderMock
                .Setup(x => x.GetService(typeof(ICourseRepository)))
                .Returns(new CourseRepository(_dbContextMock.Object));

            // Act
            var result = _unitOfWork.CourseRepository;

            // Assert
            Assert.IsAssignableFrom<CourseRepository>(result);
        }

        [Test]
        public void Should_Return_RegisteredCourseRepository()
        {
            // Arrange
            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IRegisteredCourseRepository)))
                .Returns(new RegisteredCourseRepository(_dbContextMock.Object));

            // Act
            var result = _unitOfWork.RegisteredCourseRepository;

            // Assert
            Assert.IsAssignableFrom<RegisteredCourseRepository>(result);
        }
    }
}