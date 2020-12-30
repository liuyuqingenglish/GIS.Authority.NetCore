using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace GIS.Authority.NetCore
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalHubMessageController : BaseApiControl
    {
        protected GlobalHubMessage<MessageHub> DuplexHub;

        public GlobalHubMessageController(IServiceProvider provider)
        {
            DuplexHub = (GlobalHubMessage<MessageHub>)provider.GetService(typeof(GlobalHubMessage<Hub>));
        }
    }
}