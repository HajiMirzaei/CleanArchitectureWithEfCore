using Microsoft.Extensions.Configuration;
using SampleProject.Core.Contracts;
using SampleProject.Core.Entities;

namespace SampleProject.Infrastructure.Data
{
    public class RegisteredCourseRepository : RepositoryBase<RegisteredCourse>, IRegisteredCourseRepository
    {
        public RegisteredCourseRepository(SampleProjectDbContext db) : base(db)
        {
        }
    }
}