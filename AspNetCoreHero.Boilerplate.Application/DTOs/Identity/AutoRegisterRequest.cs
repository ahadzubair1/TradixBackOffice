using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AspNetCoreHero.Boilerplate.Infrastructure.Attributes;
namespace AspNetCoreHero.Boilerplate.Application.DTOs.Identity
{
    public class AutoRegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [CustomEmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(500)]
        public string ReferredBy { get; set; }

        //[Required]
       public string Password { get; set; }

        [Required]
        public string PasswordHashBcrypt { get; set; }
        public string CountryCode { get; set; }
        [JsonIgnore]
        public bool FromApi { get;set; }




    }
}