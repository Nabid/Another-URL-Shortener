using System;
using System.Runtime.Serialization;

namespace Another_URL_Shortener.Models
{
    public class Entity
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }

    }
}