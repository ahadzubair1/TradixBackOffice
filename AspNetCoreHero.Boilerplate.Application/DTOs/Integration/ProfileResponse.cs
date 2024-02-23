using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.DTOs.Integration
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public object Wallet { get; set; }  // Assuming Wallet can be null or any other type
        public object Company { get; set; }  // Assuming Company can be null or any other type
        public string Type { get; set; }
        public bool EmailVerified { get; set; }
        public bool BusinessVerified { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public Preference Preference { get; set; }
        public bool Success { get; set; }
    }

    public class Preference
    {
        public List<string> ActiveNetworks { get; set; } = new List<string>();
    }

}
