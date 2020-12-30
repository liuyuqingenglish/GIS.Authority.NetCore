using System;

namespace GIS.Authority.Entity
{
    public class EirInfoDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// EirId
        /// </summary>
        public Guid EirId { get; set; }

        /// <summary>
        /// EirIp
        /// </summary>
        public string EirIp { get; set; }

        /// <summary>
        /// EirPort
        /// </summary>
        public string EirPort { get; set; }

        /// <summary>
        /// EirFqdn
        /// </summary>
        public string EirFqdn { get; set; }

        /// <summary>
        /// EirName
        /// </summary>
        public string EirName { get; set; }

        /// <summary>
        /// EirDescription
        /// </summary>
        public string EirDescription { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public string UpdateTime { get; set; }
    }
}