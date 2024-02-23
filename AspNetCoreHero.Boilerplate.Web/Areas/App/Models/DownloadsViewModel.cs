using System;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Models
{
    public class DownloadsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string Description { get; set; }
    }
}