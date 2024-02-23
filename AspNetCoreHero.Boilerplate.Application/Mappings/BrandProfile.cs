using AspNetCoreHero.Boilerplate.Application.Features.Brands.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetById;
using AspNetCoreHero.Boilerplate.Application.Features.Tickets.Commands.Create;
using AspNetCoreHero.Boilerplate.Domain.Entities.App;

namespace AspNetCoreHero.Boilerplate.Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
            CreateMap<CreateTicketsCommand, Ticket>().ReverseMap();
        }
    }
}