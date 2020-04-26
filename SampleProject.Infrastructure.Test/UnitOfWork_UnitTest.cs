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
        //private Mock<IServiceCollection> _serviceCollectionMock;

        [SetUp]
        public void Setup()
        {
            //_serviceCollectionMock = new Mock<IServiceCollection>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _unitOfWork = new UnitOfWork(_serviceProviderMock.Object);

            //_serviceCollectionMock.Object.RegisterInfrastructureServices();
        }

        [Test]
        public void Should_Return_IStudentRepository()
        {
            // Arrange
            _serviceProviderMock
                .Setup(x => x.GetService(typeof(IStudentRepository)))
                .Returns(It.IsAny<IStudentRepository>());

            //var services = new ServiceCollection();
            //services.AddDbContext<SampleProjectDbContext>(options => {
            //    options.UseSqlServer("DefaultConnection");
            //});
            //services.RegisterInfrastructureServices();
            //var serviceProvider = services.BuildServiceProvider();
            //var appState = serviceProvider.GetRequiredService<IStudentRepository>();
            
            // Act
            var result = _unitOfWork.StudentRepository;

            // Assert
            Assert.IsAssignableFrom<IStudentRepository>(result);
        }
    }
}