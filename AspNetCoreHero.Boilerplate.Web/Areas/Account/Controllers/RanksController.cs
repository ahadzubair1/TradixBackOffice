namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class RanksController : BaseController<RanksController>
    {
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            return View();
        }
    }
}