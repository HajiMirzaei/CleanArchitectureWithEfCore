using SampleProject.Core.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected SampleProjectDbContext _db { get; private set; }
        private IServiceProvider _serviceProvider;
        private bool _disposed;

        public UnitOfWork(SampleProjectDbContext db, IServiceProvider serviceProvider)
        {
            _db = db;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                    {
                        _db.Dispose();
                        _db = null;
                    }
                }
                _disposed = true;
            }
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IStudentRepository StudentRepository => _serviceProvider.GetRequiredService<IStudentRepository>();
        public ICourseRepository CourseRepository => _serviceProvider.GetService<ICourseRepository>();
        public IRegisteredCourseRepository RegisteredCourseRepository => _serviceProvider.GetService<IRegisteredCourseRepository>();
    }
}