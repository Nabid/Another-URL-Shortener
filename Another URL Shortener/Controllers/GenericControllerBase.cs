using System.Threading.Tasks;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Another_URL_Shortener.Controllers
{
    public abstract class GenericControllerService
    {
        readonly IServiceHandler<BaseRequest> _serviceHandler;

        public GenericControllerService(IServiceHandler<BaseRequest> serviceHandler)
        {
            _serviceHandler = serviceHandler;
        }

        public Task<BaseResponse> HandleRequest(BaseRequest request)
        {
            return _serviceHandler.HandleRequest(request);
        }
    }
}