using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(FirstGetRequest))]
    public class FirstGetService : IGenericService
    {
        public BaseResponse Handle(BaseRequest request)
        {
            request = request as FirstGetRequest;
            return new BaseResponse();
        }

    }
}