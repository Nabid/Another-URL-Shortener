using System;
using System.Linq;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;

namespace Another_URL_Shortener.Services
{
    public class UrlShortenerService: IUrlShortenerService
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        public UrlShortenerService(IRepository<ShortUrl> shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public ShortUrl Create(string key, string url, DateTime? validity)
        {
            var shortUrl = new ShortUrl
            {
                URL = url,
                ShortedURL = GenerateTinyUrl(key),
            };

            _shortUrlRepository.Add(shortUrl);
            return shortUrl;
        }

        public void Delete(string key)
        {
            var shortUrl = _shortUrlRepository.Query().FirstOrDefault(x => x.URL == key);

            if (shortUrl != null)
                _shortUrlRepository.Delete(shortUrl);
        }

        public string LoadUrl(string key)
        {
            throw new NotImplementedException();
        }

        public string Get(string key, long ticks)
        {
            throw new NotImplementedException();
        }

        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        private string GenerateTinyUrl(string key)
        {
            throw new NotImplementedException();
        }

        public object? GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}