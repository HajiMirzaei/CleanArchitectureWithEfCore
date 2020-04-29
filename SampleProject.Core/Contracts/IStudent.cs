using SampleProject.Core.Entities;
using System.Collections.Generic;

namespace SampleProject.Core.Contracts
{
    public interface IStudent
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        ICollection<RegisteredCourse> RegisteredCourses { get; set; }
        bool RegisterForCourse(Course course);
    }
}
