using GIS.Authority.Entity;
using GIS.Authority.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
namespace GIS.Authority.NetCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseApiControl
    {
        private readonly ILoginService mLoginService;

        public LoginController(ILoginService service)
        {
            mLoginService = service;
        }

        [HttpGet]
        [SkipAction]
        [HttpGet("GetRandomCode")]
        public IActionResult GetRandomCode(string ssToken)
        {
            return new FileContentResult(mLoginService.GetRandomCode(ssToken), "application/octet-stream");
        }

        [HttpGet]
        [SkipAction]
        [HttpGet("CheckRandomCode")]
        public bool CheckRandomCode(string ssToken, string code)
        {
            return mLoginService.CheckRandomCode(ssToken, code);
        }

        [HttpGet]
        [SkipAction]
        [HttpGet("Login")]
        public UserAccountDto Login(UserAccountDto dto)
        {
            return (UserAccountDto)mLoginService.Login(dto);
        }
    }
}