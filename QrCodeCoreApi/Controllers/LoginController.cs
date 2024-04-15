using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QrCodeCore.Models.Context;
using QrCodeCore.Models;
using QrCodeCoreApi.DTO.UserDto;
using Newtonsoft.Json;
using QrCodeCoreEntities.Models;

namespace QrCodeCoreApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly QrCodeCoreDbContext _qrCodeCoreDbContext;

        public LoginController(QrCodeCoreDbContext qrCodeCoreDbContext)
        {
            _qrCodeCoreDbContext = qrCodeCoreDbContext;
        }

        [HttpPost]
        public IActionResult Login(AuthDto authDto)
        {
            var query = _qrCodeCoreDbContext.TBL_USERS
                .FirstOrDefault(x => x.User_UserName == authDto.UserName && x.User_Password == authDto.Password);

            if (query != null)
            {
                var setBusiness = _qrCodeCoreDbContext.TBL_BUSINESSES
                    .FirstOrDefault(x => x.Business_Id == query.User_BusinessId);

                HttpContext.Session.SetString("BusinessName", setBusiness.Business_Name ?? "");
                HttpContext.Session.SetString("UserFullName", query.User_FullName ?? "");
                HttpContext.Session.SetInt32("BusinessId", setBusiness.Business_Id);

                SessionData sessionData = new SessionData
                {
                    BusinessId  =setBusiness.Business_Id,
                    BusinessName = setBusiness.Business_Name,
                    UserFullName = query.User_FullName
                };

                return Ok(sessionData);
            }

            return NotFound("Invalid username or password."); // veya isteğinize uygun bir dönüş yapabilirsiniz
        }
    }
}
