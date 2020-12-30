/*
* ==============================================================================
*
* Filename: BasicService
* ClrVersion: 4.0.30319.42000
* Description: Service基类
*
* Version: 1.0
* Created: 2020/3/31 21:41:52
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using GIS.Authority.Dal.UnitOfWork;

namespace GIS.Authority.Service
{
    /// <summary>
    /// Service基类
    /// </summary>
    public class BasicService : IDisposable
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IUnitOfWork Unit;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unit">工作单元</param>
        public BasicService()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unit">工作单元</param>
        public BasicService(IUnitOfWork unit)
        {
            Unit = unit;
        }

        /// <summary>
        /// 关闭服务，释放资源
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}