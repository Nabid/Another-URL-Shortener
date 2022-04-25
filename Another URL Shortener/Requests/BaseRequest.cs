using System;
using System.Runtime.Serialization;
using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Requests
{
    [Serializable]
    public abstract class BaseRequest: Entity
    {
        //[DataMember]
        //public string Version { get; set; }
    }
}