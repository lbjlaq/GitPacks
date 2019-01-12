using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Windows;
using System.Windows.Forms;

namespace GitPack
{
    class GetPack
    {
        CookieContainer cookies = new CookieContainer();
        HttpClientHandler handler = new HttpClientHandler();
        private HttpClient client;
        private string username;
        private string passwd;
        public GetPack(string username, string passwd)
        {
            this.username = username;
            this.passwd = passwd;
            handler.CookieContainer = cookies;
            client = new HttpClient(handler);
            handler.UseCookies = true;
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,zh-CN;q=0.8,zh;q=0.7");
            client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            //client.DefaultRequestHeaders.Add("Content-Length", "160");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            client.DefaultRequestHeaders.Add("Cookie", cookies.ToString());
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3668.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Referer", "https://github.com/login");
            handler.UseDefaultCredentials = false;
        }

        public async Task<bool> loginAsync()
        {
            try
            {
                HtmlDocument html=
                var postData = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("login", username),
            new KeyValuePair<string, string>("password ", passwd),
            new KeyValuePair<string, string>("commit","Sign in"),
            new KeyValuePair<string, string>("authenticity_token","dsa")
        };
                HttpContent content = new FormUrlEncodedContent(postData);
                var response = await client.PostAsync("https://github.com/session", content);
                MessageBox.Show(response.StatusCode.ToString());
                var responseString = await response.Content.ReadAsStringAsync();

                handler.CookieContainer = ReadCookies(response);
                MessageBox.Show(client.BaseAddress.ToString());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        public static CookieContainer ReadCookies(HttpResponseMessage response)
        {
            var pageUri = response.RequestMessage.RequestUri;

            var cookieContainer = new CookieContainer();
            IEnumerable<string> cookies;
            if (response.Headers.TryGetValues("set-cookie", out cookies))
            {
                foreach (var c in cookies)
                {
                    cookieContainer.SetCookies(pageUri, c);
                }
            }

            return cookieContainer;
        }

    }
}
