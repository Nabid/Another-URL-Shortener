using System;
using System.ComponentModel.DataAnnotations;

namespace Another_URL_Shortener.Models
{
    public class ShortUrl: Entity
    {
        public ShortUrl()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
            IsExpired = false;
        }

        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string URL { get; set; }
        
        public string ShortedURL { get; set; }
        
        public bool IsExpired { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedOn { get; set; }
    }
}