using System;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Models
{
    public class CountryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
    }
}