using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using GIS.Authority.Common;
using GIS.Authority.Entity;
using GIS.Authority.Service;

namespace GIS.Authority.NetCore
{
    public class GlobalHubMessage<T> where T : Hub
    {
        /// <summary>
        /// web组
        /// </summary>
        private const string WEB_GROUP = "WebGroup";

        /// <summary>
        /// mobile组
        /// </summary>
        private const string Mobile_GROUP = "MobileGroup";

        private ConcurrentDictionary<string, string> client = new ConcurrentDictionary<string, string>();

        private ConcurrentQueue<string> info = new ConcurrentQueue<string>();
        private IHubContext<T> hubMessage;

        private IBaseDuplexService mBaseService;

        public GlobalHubMessage(IHubContext<T> hubs, IBaseDuplexService baseService)
        {
            hubMessage = hubs;
            mBaseService = baseService;
        }

        public void HandleMessage(ClientType type, string connecttionId, string message)
        {
            AsyncConnectGroup(true, type, connecttionId);
            mBaseService.HandleProtocolMessage(type, connecttionId, message);
        }

        public void AsyncConnectGroup(bool isAdd, ClientType type, string connecttionId, string ipAddress = "")
        {
            string groupName = string.Empty;
            switch (type)
            {
                case ClientType.WebClient:
                    groupName = WEB_GROUP;
                    break;

                case ClientType.MobileClient:
                    groupName = Mobile_GROUP;
                    break;
            }
            if (isAdd)
            {
                if (!client.ContainsKey(connecttionId))
                {
                    client.TryAdd(connecttionId, ipAddress);
                    hubMessage.Groups.AddToGroupAsync(connecttionId, groupName);
                }
            }
            else
            {
                if (client.ContainsKey(connecttionId))
                {
                    string value = string.Empty;
                    client.TryRemove(connecttionId, out value);

                    hubMessage.Groups.RemoveFromGroupAsync(connecttionId, groupName);
                }
            }
        }

        public void HandleCheckRecordInfo(string info)
        {
            List<string> list = new List<string>(ConfigurationData.WhiteList.Split(";"));
            List<string> excludeIp = new List<string>();
            foreach (var (id, url) in client)
            {
                if (!list.Contains(url))
                {
                    excludeIp.Add(id);
                }
            }
            hubMessage.Clients.GroupExcept(WEB_GROUP, excludeIp).SendAsync("ReciveMessage", info);
        }

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="connectionId"></param>
        public void RemoveClient(string connectionId)
        {
            if (client.ContainsKey(connectionId))
            {
                string value = string.Empty;
                client.TryRemove(connectionId, out value);
            }
            hubMessage.Groups.RemoveFromGroupAsync(connectionId, WEB_GROUP);
            hubMessage.Groups.RemoveFromGroupAsync(connectionId, Mobile_GROUP);
        }
    }
}