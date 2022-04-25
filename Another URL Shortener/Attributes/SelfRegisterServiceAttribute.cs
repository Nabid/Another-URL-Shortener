using System;
using System.Collections.Generic;

namespace Another_URL_Shortener.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SelfRegisterServiceAttribute : ServiceAttribute
    {
        public SelfRegisterServiceAttribute(params Type[] registerAs) : base(registerAs)
        {
        }
    }
}