/*
* ==============================================================================
*
* Filename: DepartmentService
* ClrVersion: 4.0.30319.42000
* Description: DepartmentService
*
* Version: 1.0
* Created: 2020/1/11 9:03:15
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using DapperExtensions;
using GIS.Authority.Common;
using GIS.Authority.Contract;
using GIS.Authority.Dal.UnitOfWork;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.Authority.Service
{
    public class UserAccountService : BaseService, IUserAccountService
    {
        public UserAccountService(IUnitOfWork unit) : base(unit)
        {
        }

        public bool AddUserAccount(UserAccountDto dto)
        {
            return Unit.UserRepository.AddUserAccount(dto.ToModel<GIS.Authority.Entity.UserAccount>());
        }

        public bool DeleteUserAccount(List<Guid> orgid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            foreach (Guid item in orgid)
            {
                group.Predicates.Add(Predicates.Field<GIS.Authority.Entity.UserAccount>(d => d.Id, Operator.Eq, item));
            }
            return Unit.UserRepository.Delete(group);
        }

        public PageResult<UserAccountDto> GetUserAccountDto(PageQueryCondition<ProtocolQueryUserAccount,PageQuery> query)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            if (!string.IsNullOrEmpty(query.Condition.OrganizeId))
            {
                group.Predicates.Add(Predicates.Field<UserAccount>(d => d.OrganizationId, Operator.Eq, query.Condition.OrganizeId));
            }
            if (!string.IsNullOrEmpty(query.Condition.DepartmentId))
            {
                group.Predicates.Add(Predicates.Field<UserAccount>(d => d.DepartmentId, Operator.Eq, query.Condition.DepartmentId));
            }
            if (!string.IsNullOrEmpty(query.Condition.UserId))
            {
                group.Predicates.Add(Predicates.Field<UserAccount>(d => d.Id, Operator.Eq, query.Condition.UserId));
            }
            if (!string.IsNullOrEmpty(query.Condition.Filter))
            {
                group.Predicates.Add(Predicates.Field<UserAccount>(d => d.Name, Operator.Like, query.Condition.Filter));
            }
            return Unit.UserRepository.GetUserAccount(group, query.Query).ToPageModel<GIS.Authority.Entity.UserAccount, UserAccountDto>();
           
        }

        public bool UpdateUserAccount(UserAccountDto dto)
        {
            return Unit.UserRepository.UpdateUserAccount(dto.ToModel<GIS.Authority.Entity.UserAccount>());
        }

        public UserAccountDto GetUserDto(string account, string password)
        {
            return Unit.UserRepository.GetUserAccount(account, password).ToModel<UserAccountDto>();
        }

        public List<RoleGroupPermissionDto> GetUserRolePermission(Guid userid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Predicates.Add(Predicates.Field<UserRole>(u => u.UserId, Operator.Eq, userid));
            List<UserRole> roleList = Unit.UserRoleGroupRepository.GetList(group).ToList();
            StringBuilder sql = new StringBuilder();
            foreach (UserRole item in roleList)
            {
                sql.Append($"{item.Id},");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append($"select * from RolePerssion where {RoleGroupPermissonService.ROLE_ID} in ({sql.ToString()})");
            return Unit.RolePerssionRepository.GetList<RoleGroupPermission>(sql.ToString()).ToListDto<RoleGroupPermission, RoleGroupPermissionDto>();
        }

        public bool ForbiddenUserAccount(List<Guid> userId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update useraccount set forbidden= true");
            if (userId.Count > 0)
            {
                StringBuilder info = new StringBuilder();
                foreach (Guid item in userId)
                {
                    info.Append($"'{item}',");
                }
                info.Remove(info.Length - 1, 1);
                sql.Append($" where userid in ({info.ToString()})");
            }
            return Unit.SystemRepository.Execute(sql.ToString());
        }
    }
}