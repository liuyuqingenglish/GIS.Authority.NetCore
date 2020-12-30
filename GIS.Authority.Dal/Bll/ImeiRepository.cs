/*
* ==============================================================================
*
* Filename: AlarmRecordRepository
* ClrVersion: 4.0.30319.42000
* Description: 报警记录数据库访问对象
*
* Version: 1.0
* Created: 2020/4/9 17:36:40
* Compiler: Visual Studio 2017
*
* Author: dgf
* Copyright: lyq
*
* ==============================================================================
*/

using DapperExtensions;
using System;
using System.Collections.Generic;
using GIS.Authority.Dal.Base.BaseDal;
using GIS.Authority.Dal.IBll;
using GIS.Authority.Entity;

namespace GIS.Authority.Dal.Bll
{
    /// <summary>
    /// 报警记录数据库访问对象
    /// </summary>
    public class ImeiRepository : BasicRepository<Imei>, IImeiRepository
    {
        /// <summary>
        /// GetImeiByCode
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>int</returns>
        public int GetImeiByCode(string code)
        {
            PredicateGroup group = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };
            group.Predicates.Add(Predicates.Field<Imei>(t => t.Code, Operator.Eq, code));

            return Count(group);
        }

        /// <summary>
        /// GetImeiByCode
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>int</returns>
        public Imei GetImei(string code)
        {
            PredicateGroup group = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };
            group.Predicates.Add(Predicates.Field<Imei>(t => t.Code, Operator.Eq, code));
            List<Imei> imei = GetList(group);
            if (imei != null && imei.Count > 0)
            {
                return imei[0];
            }
            return new Imei();
        }

        /// <summary>
        /// GetEditImeiByCode
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>bool</returns>
        public bool GetEditImeiByCode(string code)
        {
            PredicateGroup group = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };
            group.Predicates.Add(Predicates.Field<Imei>(f => f.Code, Operator.Eq, code));
            return Count(group) >= 2 ? true : false;
        }
    }
}