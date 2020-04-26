using SampleProject.Core.Entities;

namespace SampleProject.Core.Contracts
{
    public interface IStudent
    {
        bool RegisterForCourse(Course course);
    }
}
