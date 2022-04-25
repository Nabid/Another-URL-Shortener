using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Requests;
using Another_URL_Shortener.Responses;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace Another_URL_Shortener.Services
{
    [SelfRegisterService(typeof(SecondGetRequest))]
    public class SecondGetService : IGenericService
    {
        public BaseResponse Handle(BaseRequest request)
        {
            return new BaseResponse();
        }
    }
}