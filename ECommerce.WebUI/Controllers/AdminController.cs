using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    //[Authorize(Roles="Admin,Editor")]//it is for all action methods
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
