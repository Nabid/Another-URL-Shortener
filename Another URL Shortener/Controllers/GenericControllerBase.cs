using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Another_URL_Shortener.Services;
using Microsoft.AspNetCore.Mvc;

namespace Another_URL_Shortener.Controllers
{
    public class GenericControllerBase: ControllerBase
    {
        IServiceHandler<BaseRequest> _serviceHandler;

        public GenericControllerBase(IServiceHandler<BaseRequest> serviceHandler)
        {
            _serviceHandler = serviceHandler;
        }

        public BaseResponse HandleRequest(BaseRequest request)
        {
            return _serviceHandler.HandleRequest(request);
        }
    }
}