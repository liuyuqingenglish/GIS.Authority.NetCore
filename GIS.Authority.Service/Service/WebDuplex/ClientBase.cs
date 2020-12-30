using System;
using GIS.Authority.Contract;
namespace GIS.Authority.Service
{
    public class ClientBase : IClientBaseService
    {
        private Action<string, string> handleMessage;

        public virtual void HandleProtocol(string connectid,int protocolType,string content)
        {
            
        }

        public void SetHanleCallback(Action<string, string> action)
        {
            handleMessage = action;
        }

        protected void SendMessage(string connecttionId, string message)
        {
            handleMessage(connecttionId, message);
        }
    }
}