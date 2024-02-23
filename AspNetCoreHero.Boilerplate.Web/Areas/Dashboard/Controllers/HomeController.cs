using AspNetCoreHero.Boilerplate.Web.Areas.Dashboard.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using AspNetCoreHero.Boilerplate.Application.Features.Dashboard.Queries;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _mediator.Send(new GetUserDashboardDataDapperRequest(Guid.Parse(currentUserId)));

            var viewModel = _mapper.Map<DashboardViewModel>(response);

            return View(viewModel);
        }
    }
}