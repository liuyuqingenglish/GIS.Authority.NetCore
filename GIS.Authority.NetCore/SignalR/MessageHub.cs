using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using GIS.Authority.Entity;

namespace GIS.Authority.NetCore
{
    /// <summary>
    /// 消息集线器
    /// </summary>
    public class MessageHub : Hub
    {
        private GlobalHubMessage<MessageHub> hubMessage;

        public MessageHub(GlobalHubMessage<MessageHub> hub)
        {
            hubMessage = hub;
        }

        public override Task OnConnectedAsync()
        {
            string url = Context.GetHttpContext().Connection.RemoteIpAddress.MapToIPv4().ToString();
            hubMessage.AsyncConnectGroup(true, ClientType.WebClient, Context.ConnectionId,url);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            hubMessage.RemoveClient(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("WebSendMessage")]
        public void WebSendMessage(string message)
        {
            //hubMessage.HandleCheckRecordInfo(message);
            hubMessage.HandleMessage(ClientType.WebClient, Context.ConnectionId, message);
        }

        [HubMethodName("MobileSendMessage")]
        public void MobileSendMessage(string message)
        {
            hubMessage.HandleMessage(ClientType.MobileClient, Context.ConnectionId, message);
        }
    }
}