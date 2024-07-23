using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCFinallProje.UI.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {

        [Area("Admin")]
        [Authorize(Roles = "Admin")]  //Admin kontrolu.
        public IActionResult Index()
        {
            return View();
        }
    }
}
