using System.Collections.Generic;
using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Responses
{
    public class GetShortUrlsResponse: BaseResponse
    {
        public List<ShortUrl> ShortUrls { get; set; }

        public GetShortUrlsResponse()
        {
            ShortUrls = new List<ShortUrl>();
        }
    }
}