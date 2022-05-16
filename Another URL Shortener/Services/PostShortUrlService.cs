using System.Collections.Generic;
using System.Threading.Tasks;
using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Microsoft.EntityFrameworkCore;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(PostShortUrlRequest))]
    public class PostShortUrlService: IGenericService
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        public PostShortUrlService(IRepository<ShortUrl> shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<BaseResponse> Handle(BaseRequest request)
        {
            var req = (PostShortUrlRequest)request;

            if (req.IsModified)
            {
                _shortUrlRepository.ModifyContextState(req.ShortUrl);
                try
                {
                    await _shortUrlRepository.SaveContext();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_shortUrlRepository.Find(x => x.Id == req.ShortUrl.Id) == null)
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                _shortUrlRepository.Add(req.ShortUrl);
            }

            var resp = new GetShortUrlsResponse()
            {
                ShortUrls = new List<ShortUrl>(){ req.ShortUrl }
            };

            return resp;
        }
    }
}