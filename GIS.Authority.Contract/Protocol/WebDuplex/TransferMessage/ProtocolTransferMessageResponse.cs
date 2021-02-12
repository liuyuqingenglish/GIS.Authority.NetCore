using GIS.Authority.Entity;
namespace GIS.Authority.Contract
{
    public class ProtocolTransferMessageResponse : ProtocolNormalResponse
    {
        /// <summary>
        /// 信息
        /// </summary>
        public CheckRecordDto info { get; set; }

        /// <summary>
        /// 远程ip
        /// </summary>
        public string RemoteAddress { get; set; }
        public ProtocolTransferMessageResponse()
        {
            this.ProtocolType = (int)GIS.Authority.Contract.ProtocolType.TransferInfoResponse;
        }
    }
}