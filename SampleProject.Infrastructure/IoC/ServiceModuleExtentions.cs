using Microsoft.Extensions.DependencyInjection;
using SampleProject.Core.Contracts;
using SampleProject.Infrastructure.Data;
using System.Linq;

namespace SampleProject.Infrastructure.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
            serviceCollection.AddScoped<IStudentRepository, StudentRepository>();
            serviceCollection.AddScoped<IRegisteredCourseRepository, RegisteredCourseRepository>();
        }
    }
}