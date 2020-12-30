using System.Text.Json;

namespace GIS.Authority.NetCore
{
    public class LowercasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            name = name.ToLower();
            return name;
        }
    }
}