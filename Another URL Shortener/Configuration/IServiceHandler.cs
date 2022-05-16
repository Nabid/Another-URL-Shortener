using System.Threading.Tasks;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;

namespace Another_URL_Shortener.Configuration
{
    public interface IServiceHandler<TRequest> where TRequest : BaseRequest
    {
        Task<BaseResponse> HandleRequest(TRequest request);
    }
}