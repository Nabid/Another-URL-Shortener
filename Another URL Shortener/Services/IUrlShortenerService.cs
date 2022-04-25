using System;
using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Services
{
    public interface IUrlShortenerService : IServiceProvider
    {
        ShortUrl Create(string key, string url, DateTime? validity);
        void Delete(string key);
        string LoadUrl(string tinyUrl);
        string Get(string key, long ticks);
    }
}