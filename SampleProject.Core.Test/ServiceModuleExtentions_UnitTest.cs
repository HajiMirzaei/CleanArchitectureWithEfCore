using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Core.IoC;
using System.Linq;

namespace SampleProject.Core.Test
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
        public void Should_Register_Scoped_ICourseService()
        {
            // Act
            _serviceCollection.RegisterCoreServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(ICourseService) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void Should_Register_Singleton_AutoMapper()
        {
            // Act
            _serviceCollection.RegisterCoreServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(IMapper) && x.Lifetime == ServiceLifetime.Singleton));
        }

    }
}