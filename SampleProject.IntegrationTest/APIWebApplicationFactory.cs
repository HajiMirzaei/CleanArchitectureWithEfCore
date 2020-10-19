using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Core.Entities;
using SampleProject.Infrastructure.Data;
using SampleProject.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleProject.IntegrationTest
{
    public class APIWebApplicationFactory : WebApplicationFactory<Startup>
    {
        // setup inmemory database
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<SampleProjectDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<SampleProjectDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SampleProjectDbContext>();
                    //var logger = scopedServices
                    //    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        //logger.LogError(ex, "An error occurred seeding the " +
                        //"database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        private static void InitializeDbForTests(SampleProjectDbContext db)
        {
            db.Students.Add(new Student { Id = 1, FirstName = "john", LastName = "Wick", RegisteredCourses = new List<RegisteredCourse> { new RegisteredCourse { Id = 1, CourseId = 1, StudentId = 1 } } });
            db.Students.Add(new Student { Id = 2, FirstName = "Bill", LastName = "Gates" });
            db.Students.Add(new Student { Id = 3, FirstName = "Joe", LastName = "Anderson" });

            db.Courses.Add(new Course { Id = 1, Name = "c#", Description = "Awesome", StartDate = System.DateTime.Now, EndDate = System.DateTime.Now.AddDays(5) });
            db.Courses.Add(new Course { Id = 2, Name = "JavaScript", Description = "WOW", StartDate = System.DateTime.Now, EndDate = System.DateTime.Now.AddDays(5) });

            db.SaveChanges();
        }

    }
}