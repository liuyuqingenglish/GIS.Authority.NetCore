namespace GIS.Authority.Contract
{
    public class ProtocolTransferMessageRequest : BaseRequest
    {
        public ProtocolTransferMessageRequest()
        {
            this.ProtocolType = (int)GIS.Authority.Contract.ProtocolType.TransferInfoRequest;
        }
    }
}