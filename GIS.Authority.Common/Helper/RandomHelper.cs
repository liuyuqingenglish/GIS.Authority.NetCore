using System;
using System.Text;

namespace GIS.Authority.Common
{
    public class RandomHelper
    {
        public static string GetRandomId(int number)
        {
            StringBuilder build = new StringBuilder();
            Random ran = new Random(Guid.NewGuid().GetHashCode());
            for (var i = 0; i < number; i++)
            {
                build.Append(ran.Next(10));
            }
            return build.ToString();
        }
    }
}