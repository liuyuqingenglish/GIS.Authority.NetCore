/*
* ==============================================================================
*
* Filename: CacheManagerBase
* ClrVersion: 4.0.30319.42000
* Description: 缓存管理基类
*
* Version: 1.0
* Created: 2019/6/24 14:58:37
* Compiler: Visual Studio 2017
*
* Author: lifu
* Copyright: lyq
*
* ==============================================================================
*/

using GIS.Authority.Common.Helper;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 获取公共配置类
    /// </summary>
    public static class ConfigurationData
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static readonly string ConnectionStr = EncryptHelper.GetInstance().Get3EdsInstance().Decryption("PostGreSql".ValueOfConnectionString());

        /// <summary>
        /// 项目启动端口
        /// </summary>
        public static readonly int ProgramPort = int.Parse("HttpProgramPort".GetAppSettingValue());

        /// <summary>
        /// nrf接口地址
        /// </summary>
        public static readonly string NrfUrl = "NrfUrl".GetAppSettingValue();

        /// <summary>
        /// nrf接口地址
        /// </summary>
        public static readonly string UdmUrl = "UdmUrl".GetAppSettingValue();

        /// <summary>
        /// nrf接口地址
        /// </summary>
        public static readonly string AgentUrl = "AgentUrl".GetAppSettingValue();

        /// <summary>
        /// nrf接口地址
        /// </summary>
        public static readonly string AmfUrl = "AmfUrl".GetAppSettingValue();

        /// <summary>
        /// nrf接口地址
        /// </summary>
        public static readonly string WhiteList = "WhiteList".GetAppSettingValue();

        /// <summary>
        /// nrf接口地址
        /// </summary>
        public static readonly string RedisConnectionStrings = "RedisConnectionStrings".GetAppSettingValue();

    }
}