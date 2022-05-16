using System;
using System.Runtime.Serialization;
using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Responses
{
    [Serializable]
    public class BaseResponse : Entity
    {
        //[DataMember] 
        //public string Url { get; set; } = "something";
    }
}