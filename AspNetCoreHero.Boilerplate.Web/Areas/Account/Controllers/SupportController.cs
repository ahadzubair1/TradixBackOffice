
using AspNetCoreHero.Boilerplate.Application.Features.Countries.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Tickets.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Tickets.Queries.GetAllPaged;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class SupportController : BaseController<SupportController>
    {
        public async Task<IActionResult> IndexAsync()
        {
            _notify.Information("Hi There!");
           
            var ticketsResponse = await _mediator.Send(new GetAllTicketsQuery(1,10));
            var tickets = ticketsResponse.Data?.ToList() ?? new List<GetAllTicketsResponse>();
             
            return View(tickets);
        }

        public async Task<IActionResult> OnPostCreateSupport(CreateTicketsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}