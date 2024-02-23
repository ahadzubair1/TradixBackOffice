using AspNetCoreHero.Boilerplate.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Admin.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}