using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QrCodeCore.Models.Context;
using QrCodeCore.Models;
using QrCodeCoreApi.DTO.FoodTypeDto;
using QrCodeCoreApi.DTO.UserDto;

namespace QrCodeCoreApi.Controllers
{
    [Route("api/sysusers")]
    [ApiController]
    public class SysUserControllerApi : ControllerBase
    {
        private readonly QrCodeCoreDbContext _qrCodeCoreDbContext;

        public SysUserControllerApi(QrCodeCoreDbContext qrCodeCoreDbContext)
        {
            _qrCodeCoreDbContext = qrCodeCoreDbContext;
        }
        [HttpGet("{business}")]
        public ActionResult<List<SysUsers>> GetFoods(int business)
        {
            int businessId = business;

            var query = _qrCodeCoreDbContext.TBL_USERS.Where(x=>x.User_BusinessId == businessId).ToList();

            return Ok(query);
        }

        [HttpPost("AddSysUser")]
        public ActionResult AddSysUser(AddUserDto addUserDto)
        {
            SysUsers sysUser = new SysUsers
            {
                User_BusinessId = addUserDto.User_BusinessId,
                User_FullName = addUserDto.User_FullName,
                User_LastName = addUserDto.User_LastName,
                User_Name = addUserDto.User_Name,
                User_Password= addUserDto.User_Password,
                User_Status = addUserDto.User_Status,
                User_UserName = addUserDto.User_UserName
            };
            _qrCodeCoreDbContext.TBL_USERS.Add(sysUser);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }

        [HttpGet("SysUserId/{userId}")]
        public SysUsers GetFoodTypeById(int userId)
        {
            var query = _qrCodeCoreDbContext.TBL_USERS.Where(
                x => x.User_Id == userId
                ).FirstOrDefault();

            return query;
        }

        [HttpPost("UpdateSysUser")]
        public ActionResult UpdateSysUser(UpdateSysUserDto updateSysUserDto)
        {
            SysUsers sysUser = new SysUsers 
            {
                User_Id = updateSysUserDto.User_Id,
                User_BusinessId = updateSysUserDto.User_BusinessId,
                User_FullName= updateSysUserDto.User_FullName,
                User_LastName= updateSysUserDto.User_LastName,
                User_Name = updateSysUserDto.User_Name,
                User_Password = updateSysUserDto.User_Password,
                User_Status= updateSysUserDto.User_Status,
                User_UserName = updateSysUserDto.User_UserName
            };

            _qrCodeCoreDbContext.TBL_USERS.Update(sysUser);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();

        }
        [HttpGet("RemoveSysUser/{id}")]
        public ActionResult RemoveSysUser(int id)
        {
            var user = _qrCodeCoreDbContext.TBL_USERS.Where(x => x.User_Id == id).FirstOrDefault();
            _qrCodeCoreDbContext.TBL_USERS.Remove(user);
            _qrCodeCoreDbContext.SaveChanges();
            return Ok();
        }

    }
}
