using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Core.Entities;
using SampleProject.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Test
{
    public class Repository_UnitTest
    {
        private DbContextOptions<SampleProjectDbContext> dbContextOptions;

        [SetUp]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<SampleProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "SampleDatabase")
                .Options;
        }

        [Test]
        public async Task Should_Return_All_Entity()
        {
            // Arrange
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                context.Students.RemoveRange(context.Students);

                context.Students.Add(new Student { Id = 1, FirstName = "john", LastName = "Wick", RegisteredCourses = new List<RegisteredCourse> { new RegisteredCourse { Id = 1, CourseId = 1, StudentId = 1 } } });
                context.Students.Add(new Student { Id = 2, FirstName = "Bill", LastName = "Gates" });
                context.Students.Add(new Student { Id = 3, FirstName = "Joe", LastName = "Anderson" });
                context.SaveChanges();
            }
            IEnumerable<Student> students;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                students = await repository.GetAllAsync();
            }

            // Assert
            Assert.AreEqual(3, students.Count());
        }

        [Test]
        public async Task Should_Add_Entity()
        {
            // Arrange
            var student = new Student()
            {
                Id = 1,
                FirstName = "test",
                LastName = "last",
                RegisteredCourses = new List<RegisteredCourse> { 
                    new RegisteredCourse { Id = 2, CourseId = 1, StudentId = 1 }
                }
            };
            int count = 0;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                await repository.AddAsync(student);
                context.SaveChanges();
                count = context.Students.Where(x => x.Id == 1).Count();
            }

            // Assert
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task Should_Remove_Entity()
        {
            // Arrange
            var student = new Student()
            {
                Id = 1,
                FirstName = "test",
                LastName = "last",
                RegisteredCourses = new List<RegisteredCourse> {
                    new RegisteredCourse { Id = 3, CourseId = 1, StudentId = 1 }
                }
            };
            int count = 0;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                await repository.DeleteAsync(1);
                context.SaveChanges();
                count = context.Students.Where(x => x.Id == 1).Count();
            }

            // Assert
            Assert.AreEqual(0, count);
        }

        [Test]
        public async Task Should_Return_Entity()
        {
            // Arrange
            var student = new Student()
            {
                Id = 5,
                FirstName = "test",
                LastName = "last",
                RegisteredCourses = new List<RegisteredCourse> {
                    new RegisteredCourse { Id = 3, CourseId = 1, StudentId = 5 }
                }
            };
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            Student stu;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                stu = await repository.GetByIdAsync(5);
            }

            // Assert
            Assert.AreEqual(5, stu.Id);
        }

        [Test]
        public async Task Should_Update_Entity()
        {
            // Arrange
            var student = new Student()
            {
                Id = 6,
                FirstName = "name",
                LastName = "last"
            };
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            Student stu;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                var entiy = new Student()
                {
                    Id = 6,
                    FirstName = "john",
                    LastName = "last"
                };
                repository.Update(entiy);
                await context.SaveChangesAsync();

                stu = context.Students.FirstOrDefault(x => x.Id == 6);
            }
            
            // Assert
            Assert.AreEqual("john", stu.FirstName);
            Assert.AreEqual("last", stu.LastName);
        }

        [Test]
        public async Task Should_Return_Student()
        {
            // Arrange
            var student = new Student()
            {
                Id = 7,
                FirstName = "Johny",
                LastName = "Depp",
                RegisteredCourses = new List<RegisteredCourse> {
                    new RegisteredCourse { Id = 4, CourseId = 1, StudentId = 7 }
                }
            };
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            IStudent stu;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                stu = await repository.GetStudentWithRegisteredCourses(7);
            }

            // Assert
            Assert.IsAssignableFrom<Student>(stu);
        }

        [Test]
        public async Task Should_Return_Student_With_Registered_Courses()
        {
            // Arrange
            var student = new Student()
            {
                Id = 8,
                FirstName = "Johny",
                LastName = "Depp",
                RegisteredCourses = new List<RegisteredCourse> {
                    new RegisteredCourse { Id = 5, CourseId = 1, StudentId = 8 }
                }
            };
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            IStudent stu;

            // Act
            using (var context = new SampleProjectDbContext(dbContextOptions))
            {
                IStudentRepository repository = new StudentRepository(context);
                stu = await repository.GetStudentWithRegisteredCourses(8);
            }

            // Assert
            Assert.IsTrue(stu.RegisteredCourses.Count() == 1);
            Assert.IsTrue(stu.RegisteredCourses.FirstOrDefault().CourseId == 1);
        }
    }
}