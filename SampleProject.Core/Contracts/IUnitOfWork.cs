using System;
using System.Threading.Tasks;

namespace SampleProject.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        IRegisteredCourseRepository RegisteredCourseRepository { get; }
    }
}
