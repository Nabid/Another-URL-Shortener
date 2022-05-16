using System;

namespace Another_URL_Shortener.Requests
{
    public class GetShortUrlsRequest : BaseRequest
    {
        public Guid? Id { get; set; } = null;
    }
}