using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Admin.Models
{
    public class SupportViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string TicketTypeID { get; set; }
        [BindProperty]
        public SelectList TicketTypes { get; set; }

    }
}