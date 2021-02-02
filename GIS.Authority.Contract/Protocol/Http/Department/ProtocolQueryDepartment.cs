/*
* ==============================================================================
*
* Filename: QueryDepartment
* ClrVersion: 4.0.30319.42000
* Description: QueryDepartment
*
* Version: 1.0
* Created: 2020/3/28 14:41:53
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIS.Authority.Common;
namespace GIS.Authority.Contract
{
    public class ProtocolQueryDepartment
    {
        public string DepartmentId{ get; set; }
        public string DepartmentName { get; set; }

        public string OrganizeId { get; set; }

        public PageQuery Query { get; set; }
    }
}
