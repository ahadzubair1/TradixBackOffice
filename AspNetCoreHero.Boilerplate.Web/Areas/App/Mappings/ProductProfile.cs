﻿using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Products.Queries.GetById;
using AspNetCoreHero.Boilerplate.Web.Areas.App.Models;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsCachedResponse, ProductViewModel>().ReverseMap();
            CreateMap<GetProductByIdResponse, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductViewModel>().ReverseMap();
        }
    }
}