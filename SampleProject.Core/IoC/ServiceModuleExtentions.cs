using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Core.Contracts;
using SampleProject.Core.Mapper;
using SampleProject.Core.UseCases;

namespace SampleProject.Core.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICourseService, CourseService>();
            var automapper = RegisterAutoMapperService();
            serviceCollection.AddSingleton(automapper);
        }

        private static IMapper RegisterAutoMapperService()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            return mapper;
        }
    }
}
