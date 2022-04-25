using System;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;

namespace Another_URL_Shortener.Configuration
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private Repository<ShortUrl> _shortUrlRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Repository<ShortUrl> ShortUrlRepository
        {
            get
            {
                if (_shortUrlRepository == null)
                {
                    _shortUrlRepository = new Repository<ShortUrl>(_dbContext);
                }

                return _shortUrlRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}