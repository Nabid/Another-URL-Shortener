using System;
using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Services
{
    public interface IUrlShortenerService
    {
        ShortUrl Create(string key, string url, DateTime? validity);
        void Delete(string key);
        string LoadUrl(string tinyUrl);
        string LoadUrl(string tinyUrl, ITinyUrlService tinyUrlService);
        string Get(string key, long ticks, ITinyUrlService tinyUrlService);
    }
}