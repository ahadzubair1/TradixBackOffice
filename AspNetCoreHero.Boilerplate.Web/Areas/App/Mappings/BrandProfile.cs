using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Update;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Features.Events.Commands.Create;
using AspNetCoreHero.Boilerplate.Web.Areas.App.Models;

namespace AspNetCoreHero.Boilerplate.Web.Areas.App.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<GetAllBrandsCachedResponse, BrandViewModel>().ReverseMap();
            CreateMap<GetBrandByIdResponse, BrandViewModel>().ReverseMap();
            CreateMap<CreateBrandCommand, BrandViewModel>().ReverseMap();
            CreateMap<CreateMiningRaceEventCommand, BrandViewModel>().ReverseMap();
            CreateMap<UpdateBrandCommand, BrandViewModel>().ReverseMap();
        }
    }
}