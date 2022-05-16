using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private bool IsValidUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri validatedUri)) //.NET URI validation.
            {
                //If true: validatedUri contains a valid Uri. Check for the scheme in addition.
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }

        private string GetUniqueId()
        {
            /* Ref: https://stackoverflow.com/a/44960751/3731282
            "It creates random ids of size 11 characters. You can increase/decrease that as well, just change the parameter of Take method.
            0.001% duplicates in 100 million."
             */
            var builder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }

        public async Task<BaseResponse> Handle(BaseRequest request)
        {
            var req = (PostShortUrlRequest)request;

            var sourceUrl = req.ShortUrl.URL;
            if(!IsValidUrl(sourceUrl))
            {
                return new ExceptionResponse()
                {
                    Message = "Invalid source URL."
                };
            }

            var resp = new GetShortUrlsResponse()
            {
                ShortUrls = new List<ShortUrl>() { req.ShortUrl }
            };
            if (req.IsModified)
            {
                req.ShortUrl.ModifiedOn = DateTime.Now;
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
                req.ShortUrl.ShortedURL ??= GetUniqueId();
                _shortUrlRepository.Add(req.ShortUrl);
            }

            return resp;
        }
    }
}