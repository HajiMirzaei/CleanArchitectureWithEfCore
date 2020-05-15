using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SampleProject.WebApi.IoC;

namespace SampleProject.WebApi.Test
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
        public void Should_Register_Singleton_AutoMapper()
        {
            // Act
            _serviceCollection.RegisterApiServices();

            // Assert
            Assert.IsTrue(_serviceCollection.Any(x => x.ServiceType == typeof(IMapper) && x.Lifetime == ServiceLifetime.Singleton));
        }
    }
}
