using AspNetCoreHero.Boilerplate.Application.DTOs.Dashboard;
using AspNetCoreHero.Boilerplate.Web.Areas.Dashboard.Models;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Dashboard.Mappings
{
    internal class DashboardDataProfile : Profile
    {
        public DashboardDataProfile()
        {
            CreateMap<UserDashboardDto, DashboardViewModel>().ReverseMap();
        }
    }
}
