using System;
using System.Threading.Tasks;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Another_URL_Shortener.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Another_URL_Shortener.Configuration
{
    public class ServiceHandler<TRequest> : IServiceHandler<TRequest> where TRequest: BaseRequest
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<BaseResponse> HandleRequest(TRequest request)
        {
            //return _serviceProvider.GetRequiredService<IGenericService>().Handle(request);
            return ((IGenericService)_serviceProvider.GetRequiredService(request.GetType())).Handle(request);
        }
    }
}