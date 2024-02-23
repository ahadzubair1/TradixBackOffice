namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class BonusesController : BaseController<BonusesController>
    {
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            return View();
        }
    }
}