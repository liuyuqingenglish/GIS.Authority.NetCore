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

using System;
using GIS.Authority.Dal.Base.BaseDal;
using GIS.Authority.Dal.IBll;
using GIS.Authority.Entity;

namespace GIS.Authority.Dal.Bll
{
    /// <summary>
    /// 报警记录数据库访问对象
    /// </summary>
    public class EirInfoRepository : BasicRepository<EirInfo>, IEirInfoRepository
    {
    }
}