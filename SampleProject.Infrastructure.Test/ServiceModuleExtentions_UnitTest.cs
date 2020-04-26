using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Infrastructure.IoC;
using System.Linq;

namespace SampleProject.Infrastructure.Test
{
    public class ServiceModuleExtentions_UnitTest
    {
        private IServiceCollection _serviceCollection;

        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection();
        }

        [Test]
        public void Should_Register_Scoped_IUnitOfWork()
        {
            // Act
            _serviceCollection.RegisterInfrastructureServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(IUnitOfWork) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void Should_Register_Scoped_ICourseRepository()
        {
            // Act
            _serviceCollection.RegisterInfrastructureServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(ICourseRepository) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void Should_Register_Scoped_IStudentRepository()
        {
            // Act
            _serviceCollection.RegisterInfrastructureServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(IStudentRepository) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void Should_Register_Scoped_IRegisteredCourseRepository()
        {
            // Act
            _serviceCollection.RegisterInfrastructureServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(IRegisteredCourseRepository) && x.Lifetime == ServiceLifetime.Scoped));
        }
    }
}