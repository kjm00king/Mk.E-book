using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Mk.Entity;

namespace Mk.E_book.BLL
{
    public class WxApi
    {
        private Wx _ = new Wx();

        public string access_token = string.Empty;
        public string jsapi_ticket = string.Empty;
        public long timestamp_prev = 0;

        public WxApi()
        {
        }

        public void setWx(Wx wx)
        {
            _ = wx;
        }

        private string getResult(string url)
        {

            url = HttpUtility.HtmlDecode(url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            if (_.api_use_proxy)
            {
                request.Proxy = new WebProxy(_.api_proxy_address, _.api_proxy_port);
                request.Proxy.Credentials = _.getProxyCredential();
            }

            string result = string.Empty;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.UTF8);
            result = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return result;
        }

        public string getToken()
        {
            return getResult(_.getTokenUrl());
        }

        public string getTicket()
        {
            return getResult(_.getTicketUrl(access_token));
        }

        public bool check()
        {
            if (_.getTimeStamp() - timestamp_prev > _.api_refresh_interval)
            {
                string token_str = getToken();
                if (token_str.IndexOf("errcode") >= 0)
                {
                    Wx.Error token_error = Utility.FromJson<Wx.Error>(token_str);
                    throw new Exception(token_error.ToString());
                }
                else
                {
                    Wx.TokenResult token = Utility.FromJson<Wx.TokenResult>(token_str);
                    access_token = token.access_token;

                    string ticket_str = getTicket();

                    Wx.Error ticket_error = Utility.FromJson<Wx.Error>(ticket_str);
                    if (ticket_error.errcode != 0)
                    {
                        throw new Exception(ticket_error.ToString());
                    }
                    else
                    {
                        Wx.TicketResult ticket = Utility.FromJson<Wx.TicketResult>(ticket_str);
                        jsapi_ticket = ticket.ticket;
                    }
                }
                timestamp_prev = _.getTimeStamp();
                return true;
            }
            else
                return false;
        }
        
        public class Config
        {
            public bool debug = true;
            public string appId = string.Empty;
            public long timestamp = 0;
            public string nonceStr = string.Empty;
            public string signature = string.Empty;
            public List<string> jsApiList = new List<string>();

            public Config() { }
        }

        public Config getConfig(HttpRequestBase request)
        {
            return getConfig(request.Url.AbsolutePath);
        }

        public Config getConfig(string url)
        {
            Config rv = new Config()
            {
                appId = _.api_appid,
                debug = _.api_debug,
                nonceStr = Guid.NewGuid().ToString().Replace("-", ""),
                timestamp = _.getTimeStamp(),
                signature = string.Empty,
                jsApiList = _.api_jsApiList,
            };

            SHA1 algorithm = SHA1.Create();
            string source = HttpUtility.UrlDecode(_.getSignatureUrl(jsapi_ticket, rv.nonceStr, rv.timestamp, url));
            byte[] data = algorithm.ComputeHash( Encoding.UTF8.GetBytes(source));
            for (int i = 0; i < data.Length; i++)
            {
                rv.signature += data[i].ToString("x2").ToLowerInvariant();
            }

            return rv;
        }
    }
}