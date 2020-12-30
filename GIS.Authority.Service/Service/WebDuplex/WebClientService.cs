using GIS.Authority.Contract;

namespace GIS.Authority.Service
{
    public class WebClientService : ClientBase, IWebClientService
    {
        public override void HandleProtocol(string connectid, int protocolType, string content)
        {
            switch (protocolType)
            {
                case (int)GIS.Authority.Contract.ProtocolType.TransferInfoRequest:
                    AnalysisTransferInfo(connectid, content);
                    break;
            }
            base.HandleProtocol(connectid, protocolType, content);
        }

        private string AnalysisTransferInfo(string connectid, string content)
        {
            ProtocolTransferMessageRequest request = AnalysisHelper.ToProtocol<ProtocolTransferMessageRequest>(content);
            ProtocolTransferMessageResponse response = new ProtocolTransferMessageResponse();
            SendMessage(connectid, AnalysisHelper.ToJson(response));
            return string.Empty;
        }
    }
}