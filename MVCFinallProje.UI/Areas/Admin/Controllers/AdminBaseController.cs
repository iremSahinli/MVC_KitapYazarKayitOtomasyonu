using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCFinallProje.UI.Controllers;

namespace MVCFinallProje.UI.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = "Admin")]  //Admin kontrolu.
    public class AdminBaseController : BaseController
    {
        
    }
}
