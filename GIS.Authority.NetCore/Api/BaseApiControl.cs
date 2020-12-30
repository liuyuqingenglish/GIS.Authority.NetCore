using Microsoft.AspNetCore.Mvc;

namespace GIS.Authority.NetCore
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ActionResultFilter]
    public class BaseApiControl : ControllerBase
    {
    }
}