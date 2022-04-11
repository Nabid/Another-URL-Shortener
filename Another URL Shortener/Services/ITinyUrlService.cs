using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Services
{
    public interface ITinyUrlService
    {
        string LoadUrl(string key);
        ShortUrl Get(long ticks);
    }
}