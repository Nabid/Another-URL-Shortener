using System;
using System.ComponentModel.DataAnnotations;

namespace Another_URL_Shortener.Models
{
    public class ShortUrl
    {
        public ShortUrl()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
            IsExpired = false;
        }

        public Guid Id { get; set; }
        public string? URL { get; set; }
        public string? ShortedURL { get; set; }
        public bool IsExpired { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ModifiedOn { get; set; }
    }
}