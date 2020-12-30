/*
* ==============================================================================
*
* Filename: IAlarmRecordRepository
* ClrVersion: 4.0.30319.42000
* Description: 报警记录数据访问接口
*
* Version: 1.0
* Created: 2020/4/9 17:33:53
* Compiler: Visual Studio 2017
*
* Author: dgf
* Copyright: lyq
*
* ==============================================================================
*/

using GIS.Authority.Dal.Base.IBaseDal;
using GIS.Authority.Entity;

namespace GIS.Authority.Dal.IBll
{
    /// <summary>
    /// 报警记录数据访问接口
    /// </summary>
    public interface IEirInfoRepository : IBasicRepository<EirInfo>
    {
    }
}