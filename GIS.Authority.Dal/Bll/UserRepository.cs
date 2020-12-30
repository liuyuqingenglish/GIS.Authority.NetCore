/*
* ==============================================================================
*
* Filename: DepartmentRepository
* ClrVersion: 4.0.30319.42000
* Description: DepartmentRepository
*
* Version: 1.0
* Created: 2019/12/26 17:21:49
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using DapperExtensions;
using GIS.Authority.Common;
using GIS.Authority.Dal.Base.BaseDal;
using GIS.Authority.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.Authority.Dal
{
    public class UserRepository : BasicRepository<UserAccount>, IUserRepository
    {
        public bool AddUserAccount(UserAccount user)
        {
            return base.Insert(user);
        }

        public bool DeleteUserAccount(PredicateGroup group)
        {
            return base.Delete(group);
        }

        public List<UserAccount> GetUserAccount(PredicateGroup group, PageQuery query)
        {
            return base.GetPager(group, query).ToList();
        }

        public bool UpdateUserAccount(UserAccount user)
        {
            return base.Update(user);
        }

        public UserAccount GetUserAccount(string account, string password)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($"select * from useraccount where account={account} and password={password};");
            return (base.Get(sql.ToString()));
        }
    }
}