using SampleProject.Core.DTOs;
using SampleProject.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Core.Contracts
{
    public interface ICourseService
    {
        Task<List<string>> RegisterCourseAsync(RegisterCourseInput arg);
        Task<IEnumerable<Course>> GetAllAsync();
    }
}