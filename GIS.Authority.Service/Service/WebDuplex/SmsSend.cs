using System;

namespace GIS.Authority.Service
{
    public class SmsSend : ISendMessage
    {
        public void Send()
        {
            Console.WriteLine("send sms");
        }
    }
}