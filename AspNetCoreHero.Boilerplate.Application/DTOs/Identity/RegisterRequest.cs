using AspNetCoreHero.Boilerplate.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreHero.Boilerplate.Application.DTOs.Identity
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [CustomEmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(500)]
        public string Referral { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}