/*
* ==============================================================================
*
* Filename: GpsPositionType
* ClrVersion: 4.0.30319.42000
* Description: 解状态
*
* Version: 1.0
* Created: 2020/4/8 13:21:49
* Compiler: Visual Studio 2017
*
* Author: dgf
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Common.Helper
{
    /// <summary>
    /// 解状态
    /// </summary>
    public enum GpsPositionType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// 单点定位
        /// </summary>
        Single = 1,

        /// <summary>
        /// 伪距差分
        /// </summary>
        RTD = 2,

        /// <summary>
        /// 浮动解
        /// </summary>
        RTK_FIXED = 4,

        /// <summary>
        /// 固定解
        /// </summary>
        RTK_FLOAT = 5,

        /// <summary>
        /// 航位推测模式
        /// </summary>
        DRM = 6,

        /// <summary>
        /// 已知位置
        /// </summary>
        FIXED_POS = 7,

        /// <summary>
        /// 模拟位置
        /// </summary>
        SIMULATOR = 8,

        /// <summary>
        /// 广域差分
        /// </summary>
        SBAS = 9,

        /// <summary>
        /// PPP固定解
        /// </summary>
        PPP_FIXED = 10,

        /// <summary>
        /// PPP浮动解
        /// </summary>
        PPP_FLOAT = 11,

        /// <summary>
        /// 室内位置
        /// </summary>
        INDOOR = 126,
    }
}