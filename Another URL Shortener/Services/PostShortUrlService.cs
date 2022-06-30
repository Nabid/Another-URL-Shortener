using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(PostShortUrlRequest))]
    public class PostShortUrlService: IGenericService
    {
        private readonly IRepository<ShortUrl> _shortUrlRepository;
        private readonly ICachedDbRepository<CachedShortUrl> _cachedShortUrlRepository;
        private readonly IOptions<CustomConfigs> _settings;

        public PostShortUrlService(IRepository<ShortUrl> shortUrlRepository, IOptions<CustomConfigs> settings, ICachedDbRepository<CachedShortUrl> cachedShortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
            _settings = settings;
            _cachedShortUrlRepository = cachedShortUrlRepository;
        }

        private static bool IsValidUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri validatedUri)) //.NET URI validation.
            {
                //If true: validatedUri contains a valid Uri. Check for the scheme in addition.
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }

        private static string GenerateUniqueId(int length)
        {
            /* Ref: https://stackoverflow.com/a/44960751/3731282
            "It creates random ids of size 11 characters. You can increase/decrease that as well, just change the parameter of Take method.
            0.001% duplicates in 100 million."*/
            var builder = new StringBuilder();
            Enumerable
                .Range(65, 26).Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid()) // randomize order
                .Take(length)
                .ToList()
                .ForEach(e => builder.Append(e));
            return builder.ToString();
        }

        private string GetUniqueId()
        {
            var uniqueId = GenerateUniqueId(_settings.Value.UniqueIdLength);
            var res = _cachedShortUrlRepository.Find(x => x.Id == uniqueId).FirstOrDefault();
            while (_cachedShortUrlRepository.Find(x => x.Id == uniqueId).FirstOrDefault() != null)
            {
                uniqueId = GenerateUniqueId(_settings.Value.UniqueIdLength);
            }
            return uniqueId;
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
                _cachedShortUrlRepository.Add(new CachedShortUrl()
                {
                    Id = req.ShortUrl.ShortedURL,
                    ActualUrl = req.ShortUrl.URL
                });
                _shortUrlRepository.Add(req.ShortUrl);
            }

            return resp;
        }
    }
}