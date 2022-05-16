using System;
using System.Linq;
using System.Threading.Tasks;
using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(GetShortUrlsRequest))]
    public class GetShortUrlsService : IGenericService
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        public GetShortUrlsService(IRepository<ShortUrl> shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }


        public async Task<BaseResponse> Handle(BaseRequest request)
        {
            var req = (GetShortUrlsRequest)request;
            var resp = new GetShortUrlsResponse();

            if (req.Id != null)
            {
                var result = await _shortUrlRepository.Get((Guid)req.Id);
                resp.ShortUrls.Add(result);
            }
            else
            {
                var results = await _shortUrlRepository.GetAll();
                results.ForEach(s =>
                {
                    resp.ShortUrls.Add(s);
                });
            }

            return resp;
        }

    }
}