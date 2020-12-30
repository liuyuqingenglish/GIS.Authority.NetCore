/*
* ==============================================================================
*
* Filename: IUnitOfWork
* ClrVersion: 4.0.30319.42000
* Description: 工作单元接口
*
* Version: 1.0
* Created: 2020/3/31 21:12:24
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using GIS.Authority.Dal.IBll;

namespace GIS.Authority.Dal.UnitOfWork
{
    /// <summary>
    /// 工作单元接口--用于解析各类数据库访问对象
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Imea
        /// </summary>
        IImeiRepository ImeaRepository { get; }

        /// <summary>
        /// checkrecord
        /// </summary>
        ICheckRecordRepository CheckRecordRepository { get; }

        /// <summary>
        /// checkrecord
        /// </summary>
        IEirInfoRepository EirInfoRepository { get; }
    }
}