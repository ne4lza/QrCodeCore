using Microsoft.AspNetCore.Mvc;
using QrCodeCore.Models;
using QrCodeCore.Models.Context;

namespace QrCodeCore.Controllers
{
    public class UserController : Controller
    {
        private readonly QrCodeCoreDbContext _qrCodeCoreDbContext;

        public UserController(QrCodeCoreDbContext qrCodeCoreDbContext)
        {
            _qrCodeCoreDbContext = qrCodeCoreDbContext;
        }

        public ActionResult Index()
        {
            int businessId = (int)HttpContext.Session.GetInt32("Business_Id");
            var model = _qrCodeCoreDbContext.TBL_USERS.Where(x => x.User_BusinessId == businessId).ToList();
            ViewBag.countUser = _qrCodeCoreDbContext.TBL_USERS.Where(x => x.User_BusinessId == businessId && x.User_Status == true).Count();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(SysUsers user)
        {
            return View();
        }
    }
}
