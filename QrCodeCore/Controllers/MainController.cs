using Microsoft.AspNetCore.Mvc;

namespace QrCodeCore.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
