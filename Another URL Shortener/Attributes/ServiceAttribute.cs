using System;
using System.Collections.Generic;

namespace Another_URL_Shortener.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ServiceAttribute : Attribute
    {
        public readonly IEnumerable<Type> RegisterAs;
        //public readonly Type RequestType;

        public ServiceAttribute(IEnumerable<Type> registerAs)
        {
            RegisterAs = registerAs;
            //RequestType = requestType;
        }
    }
}