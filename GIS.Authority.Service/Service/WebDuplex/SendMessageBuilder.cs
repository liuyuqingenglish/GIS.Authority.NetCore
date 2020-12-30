using Microsoft.Extensions.DependencyInjection;

namespace GIS.Authority.Service
{
    public class SendMessageBuilder
    {
        public IServiceCollection service;

        public SendMessageBuilder(IServiceCollection collection)
        {
            service = collection;
        }

        public void UseSms()
        {
            service.AddSingleton<ISendMessage, SmsSend>();
        }

        public void UseEmail()
        {
            service.AddSingleton<ISendMessage, EmailSend>();
        }
    }
}