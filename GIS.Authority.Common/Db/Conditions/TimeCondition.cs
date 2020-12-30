/*
* ==============================================================================
*
* Filename: TimeCondition
* ClrVersion: 4.0.30319.42000
* Description: 包含时间的查询
*
* Version: 1.0
* Created: 2019/8/26 11:02:47
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Common
{
    /// <summary>
    /// 包含时间的查询
    /// </summary>
    public class TimeCondition : BasicCondition
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}