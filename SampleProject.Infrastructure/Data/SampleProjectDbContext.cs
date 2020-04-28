using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Entities;

namespace SampleProject.Infrastructure.Data
{
    public class SampleProjectDbContext : DbContext
    {
        public SampleProjectDbContext(DbContextOptions options) : base(options) { }
        public SampleProjectDbContext(){}

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Student>().HasData
            (
                new Student { Id = 1, FirstName = "John", LastName = "Cena" },
                new Student { Id = 2, FirstName = "Alex", LastName = "Morgan" }
            );

            builder.Entity<Course>().HasData
            (
                new Course { Id = 1, Name = "c#", Description = "Awesome", StartDate = System.DateTime.Now, EndDate = System.DateTime.Now.AddDays(5) },
                new Course { Id = 2, Name = "JavaScript", Description = "WOW", StartDate = System.DateTime.Now, EndDate = System.DateTime.Now.AddDays(5) }
            );
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<RegisteredCourse> RegisteredCourses { get; set; }
    }
}
