using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GIS.Authority.Common;

namespace GIS.Authority.Service
{
    public class GlobalClientService
    {
        #region 初始化

        public HttpClient httpClient { get; }

        public GlobalClientService(HttpClient client)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            client = new HttpClient();
            client.DefaultRequestVersion = new Version(2, 0);
            httpClient = client;
        }

        public async Task<string> GetString(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task<string> SendPost(string url, string para = null)
        {
            StringContent content = string.IsNullOrEmpty(para) ? null : new StringContent(para, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return string.Empty;
        }

        public async Task<string> SendPut(string url, string para = null)
        {
            StringContent content = string.IsNullOrEmpty(para) ? null : new StringContent(para);
            var response = await httpClient.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return string.Empty;
        }

        #endregion 初始化

    }
}