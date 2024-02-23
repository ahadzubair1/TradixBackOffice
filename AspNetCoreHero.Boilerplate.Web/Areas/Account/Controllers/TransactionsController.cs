using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Transactions.Queries.GetAll;
using AspNetCoreHero.Boilerplate.Web.Areas.Account.Models;
using AspNetCoreHero.Boilerplate.Web.Areas.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class TransactionsController : BaseController<TransactionsController>
    {
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllTransactionsQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TransactionViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }
    }
}