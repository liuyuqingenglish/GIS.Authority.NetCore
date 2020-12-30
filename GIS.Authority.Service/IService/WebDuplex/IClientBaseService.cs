using System;
using System.Collections;
namespace GIS.Authority.Service
{
    public interface IClientBaseService
    {
        void HandleProtocol(string connectid, int protocolType, string content);

        void SetHanleCallback(Action<string,string> action);
    }
}