using System;

namespace GIS.Authority.Entity
{
    public class ImeiDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string Result { get; set; }
    }
}