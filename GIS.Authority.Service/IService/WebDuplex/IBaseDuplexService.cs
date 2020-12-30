using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Collections;
namespace GIS.Authority.Service
{
    public interface IBaseDuplexService
    {
        void HandleProtocolMessage(ClientType type, string connnectionId, string protocol);

        Dictionary<string,string[]> GetImediatelyProtocol();
    }
}