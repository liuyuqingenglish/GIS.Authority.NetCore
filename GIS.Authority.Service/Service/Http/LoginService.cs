using GIS.Authority.Common;
using GIS.Authority.Dal.UnitOfWork;
using GIS.Authority.Entity;
using System;

namespace GIS.Authority.Service
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly IUserAccountService userAccountService;

        public LoginService(IUnitOfWork iunit, IUserAccountService service) : base(iunit)
        {
            userAccountService = service;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="ssToken"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckRandomCode(string ssToken, string code)
        {
            string tempCode = RedisInstanceHelper.GetInstance().GetKeyString(ssToken);
            if (string.IsNullOrEmpty(tempCode))
            {
                return false;
            }
            if (!tempCode.Equals(code))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="ssToken"></param>
        /// <returns></returns>
        public byte[] GetRandomCode(string ssToken)
        {
            string code = ValidationCodeHelper.GetInstance().CreateCode(2, 4);
            RedisInstanceHelper.GetInstance().SetKeyString(ssToken, code, 30);
            return ValidationCodeHelper.GetInstance().CreateImage(code);
        }

        public object Login(UserAccountDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Password))
            {
                return new ServiceResult<Exception>(new Exception("用户名或者密码为空"));
            }
            UserAccountDto userTemp = userAccountService.GetUserDto(dto.Name, dto.Password);
            if (userTemp == null)
            {
                return new ServiceResult<Exception>(new Exception("当前用户不存在"));
            }
            if (dto.IsRemain)
            {
                RedisInstanceHelper.GetInstance().defaulTimeHour += 3 * 24;
            }
            string webToken = Guid.NewGuid().ToString();
            userTemp.WebToken = webToken;
            RedisInstanceHelper.GetInstance().SetKeyString(webToken, userTemp, (int)RedisInstanceHelper.GetInstance().defaulTimeHour * 3600);
            return new ServiceResult<UserAccountDto>(userTemp);
        }
    }
}