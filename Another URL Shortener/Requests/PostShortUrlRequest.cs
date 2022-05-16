using Another_URL_Shortener.Models;
using Another_URL_Shortener.Requests;

namespace Another_URL_Shortener.Services
{
    public class PostShortUrlRequest: BaseRequest
    {
        public ShortUrl ShortUrl { get; set; }
        public bool IsModified { get; set; } = false;
    }
}