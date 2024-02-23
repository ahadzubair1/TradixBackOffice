using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
        public SelectList Brands { get; set; }
    }
}