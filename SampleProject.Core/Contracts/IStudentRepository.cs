using SampleProject.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Core.Contracts
{
    public interface IStudentRepository : IAsyncGenericRepository<Student>
    {
        Task<IStudent> GetStudentWithRegisteredCourses(int studentId);
    }
}
