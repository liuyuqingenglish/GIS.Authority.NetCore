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
    public interface IImeiRepository : IBasicRepository<Imei>
    {
        /// <summary>
        ///GetImeiByCode
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>int</returns>
        public int GetImeiByCode(string code);

        /// <summary>
        /// GetImei
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>Imei</returns>
        public Imei GetImei(string code);

        /// <summary>
        /// GetEditImeiByCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool GetEditImeiByCode(string code);
    }
}