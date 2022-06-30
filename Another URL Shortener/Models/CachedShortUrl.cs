namespace Another_URL_Shortener.Models
{
    public class CachedShortUrl : Entity
    {
        public string Id { get; set; }
        public string ActualUrl { get; set; }
    }
}