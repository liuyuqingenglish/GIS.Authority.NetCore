﻿using GIS.Authority.Common;
using GIS.Authority.Entity;

namespace GIS.Authority.Service
{
    public class LoginService : ILoginService
    {
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

        public UserAccountDto Login(UserAccountDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}