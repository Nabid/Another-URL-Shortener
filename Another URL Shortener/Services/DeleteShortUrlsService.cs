using System;
using System.Threading.Tasks;
using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Microsoft.EntityFrameworkCore;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(DeleteShortUrlsRequest))]
    public class DeleteShortUrlsService: IGenericService
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;

        public DeleteShortUrlsService(IRepository<ShortUrl> shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<BaseResponse> Handle(BaseRequest request)
        {
            var req = (DeleteShortUrlsRequest)request;
            var resp = new GetShortUrlsResponse();

            var shortUrl = await _shortUrlRepository.Find(x => x.Id == req.Id).FirstOrDefaultAsync();
            if (shortUrl == null)
            {
                return null;
            }
            _shortUrlRepository.Delete(shortUrl);
            
            return resp;
        }
    }
}