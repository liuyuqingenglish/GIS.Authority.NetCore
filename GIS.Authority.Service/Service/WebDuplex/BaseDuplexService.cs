using System.Collections.Concurrent;
using System.Collections.Generic;
using GIS.Authority.Contract;
using GIS.Authority.Entity;

namespace GIS.Authority.Service
{
    public class BaseDuplexService : IBaseDuplexService
    {
        /// <summary>
        /// 发送给所有web端的协议
        /// </summary>
        private static ConcurrentQueue<string> SendWebProtocol = new ConcurrentQueue<string>();

        /// <summary>
        /// 发送给所有移动端的协议
        /// </summary>
        private static ConcurrentQueue<string> SendMobileProtocol = new ConcurrentQueue<string>();

        /// <summary>
        /// 立即发送协议
        /// </summary>
        private static ConcurrentDictionary<string, ConcurrentQueue<string>> imedialyProtocol = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        /// <summary>
        /// 缓存协议
        /// </summary>
        private static ConcurrentDictionary<string, ConcurrentQueue<string>> bufferProtocol = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        private IWebClientService mWebClientService;

        public BaseDuplexService(IWebClientService web)
        {
            mWebClientService = web;
            mWebClientService.SetHanleCallback(SendMessage);
        }

        public void HandleProtocolMessage(ClientType type, string connnectionId, string protocol)
        {
            BaseRequest request = AnalysisHelper.ToProtocol<BaseRequest>(protocol);
            switch (type)
            {
                case ClientType.WebClient:
                    mWebClientService.HandleProtocol(connnectionId, request.ProtocolType, protocol);
                    break;

                case ClientType.MobileClient:

                    break;
            }
        }

        public void SendMessage(string connectionId, string message)
        {
            if (!imedialyProtocol.ContainsKey(connectionId))
            {
                imedialyProtocol[connectionId] = new ConcurrentQueue<string>();
            }
            imedialyProtocol[connectionId].Enqueue(message);
        }

        public Dictionary<string, string[]> GetImediatelyProtocol()
        {
            Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
            if (imedialyProtocol.Count > 0)
            {
                foreach (var (key, value) in imedialyProtocol)
                {
                    dic.Add(key, value.ToArray());
                }
            }
            return dic;
        }
    }
}