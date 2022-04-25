using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;

namespace Another_URL_Shortener.Configuration
{
    public interface IServiceHandler<TRequest> where TRequest : BaseRequest
    {
        BaseResponse HandleRequest(TRequest request);
    }
}