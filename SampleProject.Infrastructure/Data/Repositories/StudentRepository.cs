using System.Threading.Tasks;
using SampleProject.Core.Contracts;
using SampleProject.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.Infrastructure.Data
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(SampleProjectDbContext db) : base(db)
        {
        }

        public async Task<IStudent> GetStudentWithRegisteredCourses(int studentId)
        {
            var data = await _db.Students.Include(r => r.RegisteredCourses).FirstOrDefaultAsync(x => x.Id == studentId);
            return data;
        }
    }
}