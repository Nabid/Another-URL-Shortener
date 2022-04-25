using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;

namespace Another_URL_Shortener.Services
{
    public interface IGenericService
    {
        BaseResponse Handle(BaseRequest request);
    }
}