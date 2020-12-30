using System;

namespace GIS.Authority.Entity
{
    public class CheckRecordDto
    {
        public string Id { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid ImeiId { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime ResponseTime { get; set; }

        /// <summary>
        /// supi
        /// </summary>
        public string Supi { get; set; }
    }
}