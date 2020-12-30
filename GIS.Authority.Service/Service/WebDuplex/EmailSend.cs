using System;

namespace GIS.Authority.Service
{
    public class EmailSend : ISendMessage
    {
        public void Send()
        {
            Console.WriteLine("send email");
        }
    }
}