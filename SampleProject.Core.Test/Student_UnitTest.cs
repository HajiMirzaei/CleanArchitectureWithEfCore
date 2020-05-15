using NUnit.Framework;
using SampleProject.Core.Contracts;
using SampleProject.Core.Entities;
using System.Collections.Generic;

namespace SampleProject.Core.Test
{
    public class Student_UnitTest
    {
        private IStudent _student;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Should_Return_False1()
        {
            // Arrange
            _student = new Student()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Cena",
                RegisteredCourses = new List<RegisteredCourse>()
                {
                    new RegisteredCourse()
                    {
                        Id = 1,
                        StudentId = 1,
                        CourseId = 1
                    }
                }
            };
            var course = new Course()
            {
                Id = 1,
                Name = "python",
                Description = "Wow"
            };

            // Act
            var result =_student.RegisterForCourse(course);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_Return_False2()
        {
            // Arrange
            _student = new Student()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Cena",
                RegisteredCourses = new List<RegisteredCourse>()
            };
            var course = new Course()
            {
                Id = 1,
                Name = "python",
                Description = "Wow",
                StartDate = System.DateTime.Now.AddDays(-5)
            };

            // Act
            var result = _student.RegisterForCourse(course);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_Return_True()
        {
            // Arrange
            _student = new Student()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Cena",
                RegisteredCourses = new List<RegisteredCourse>()
            };
            var course = new Course()
            {
                Id = 1,
                Name = "python",
                Description = "Wow",
                StartDate = System.DateTime.Now.AddDays(-4)
            };

            // Act
            var result = _student.RegisterForCourse(course);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_Return_False3()
        {
            // Arrange
            _student = new Student()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Cena",
                RegisteredCourses = new List<RegisteredCourse>()
            };

            // Act
            var result = _student.RegisterForCourse(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}