using Microsoft.AspNetCore.Mvc.Filters;

namespace GIS.Authority.NetCore
{
    public class ResourceFilterAttribute : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //throw new System.NotImplementedException();
        }
    }
}