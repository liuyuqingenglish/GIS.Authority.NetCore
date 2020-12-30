using Microsoft.Extensions.DependencyInjection;
using System;

namespace GIS.Authority.Service
{
    public static class ServiceCollectionExtension
    {
        public static void AddSendService(this IServiceCollection service, Action<SendMessageBuilder> action)
        {
            SendMessageBuilder sendBuild = new SendMessageBuilder(service);
            action(sendBuild);
        }
    }
}