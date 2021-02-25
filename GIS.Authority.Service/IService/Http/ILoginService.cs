using System;
using System.Collections.Generic;
using System.Text;
using GIS.Authority.Common;
using GIS.Authority.Entity;
namespace GIS.Authority.Service
{
   public interface ILoginService
    {
        byte[] GetRandomCode(string ssToken);

        bool CheckRandomCode(string ssToken, string code);

        object Login(UserAccountDto dto);
        bool LoginOut(string token);
    }
}
