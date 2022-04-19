using System;
using System.Linq;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;

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

            _shortUrlRepository.Save(shortUrl);
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
            var shortUrl = _shortUrlRepository.Query().SingleOrDefault(x => x.URL == key);
            if (shortUrl == null)
            {
                throw new Exception($"Unable to find Url from {key}");
            }

            return shortUrl.URL;
        }

        public string LoadUrl(string key, ITinyUrlService tinyUrlService)
        {
            var shortUrl = _shortUrlRepository.Query().SingleOrDefault(x => x.URL == key);
            if (shortUrl != null) return shortUrl.URL;
            //check if the key is tiny url
            Guid convertedGuid;
            if (!Guid.TryParse(key, out convertedGuid))
            {
                return tinyUrlService.LoadUrl(key);
            }

            throw new Exception($"Unable to find Url from {key}");
        }

        public string Get(string key, long ticks, ITinyUrlService tinyUrlService)
        {
            var tinyUrl = tinyUrlService.Get(ticks);

            var url = (tinyUrl != null && !string.IsNullOrEmpty(tinyUrl.URL)) ? tinyUrl.URL : LoadUrl(key, tinyUrlService);

            return url;
        }

        private string GenerateTinyUrl(string key)
        {
            return $"/t/{key}";
        }
    }
}