using System;
using System.Linq;
using System.Threading.Tasks;
using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Microsoft.Extensions.Options;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(GetShortUrlsRequest))]
    public class GetShortUrlsService : IGenericService
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        private readonly IOptions<CustomConfig> _settings;

        public GetShortUrlsService(IRepository<ShortUrl> shortUrlRepository, IOptions<CustomConfig> settings)
        {
            _shortUrlRepository = shortUrlRepository;
            _settings = settings;
        }


        private string AddBaseValue(string url)
        {
            return $"{_settings.Value.RootURL}/{url}";
        }

        public async Task<BaseResponse> Handle(BaseRequest request)
        {
            var req = (GetShortUrlsRequest)request;
            var resp = new GetShortUrlsResponse();

            if (req.Id != null)
            {
                var result = await _shortUrlRepository.Get((Guid)req.Id);
                result.ShortedURL = AddBaseValue(result.ShortedURL);
                resp.ShortUrls.Add(result);
            }
            else
            {
                var results = await _shortUrlRepository.GetAll();
                results.ForEach(s =>
                {
                    s.ShortedURL = AddBaseValue(s.ShortedURL);
                    resp.ShortUrls.Add(s);
                });
            }

            return resp;
        }

    }
}