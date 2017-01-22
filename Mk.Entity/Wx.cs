using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Mk.Entity
{
    public class Wx
    {
        public string api_url = string.Empty;
        public bool api_debug = false;
        public string api_grant_type = string.Empty;
        public string api_appid = string.Empty;
        public string api_secret = string.Empty;
        //public string api_access_token = string.Empty;
        //public string api_jsapi_ticket = string.Empty;
        public string api_type = string.Empty;

        public string api_token_url = string.Empty;
        public string api_ticket_url = string.Empty;
        public string api_signature_url = string.Empty;

        public bool api_use_proxy = false;
        public string api_proxy_address = string.Empty;
        public int api_proxy_port = 0;
        public string api_proxy_domain = string.Empty;
        public string api_proxy_user = string.Empty;
        public string api_proxy_pwd = string.Empty;

        //public long api_timestamp_prev = 0;
        public int api_refresh_interval = 0;

        public List<string> api_jsApiList = new List<string>();

        public Wx() { }
        
        public class Error
        {
            public string errmsg { get; set; }
            public int errcode { get; set; }

            public override string ToString()
            {
                return string.Format("ERROR: {1} (CODE:{0})", errcode, errmsg);
            }
        }
        
        public class TokenResult
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }

        public class TicketResult
        {
            public string ticket { get; set; }
            public int expires_in { get; set; }
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

        public long getTimeStamp()
        {
            return (long)DateTime.UtcNow
               .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
               .TotalSeconds;
        }

        public string getTokenUrl()
        {
            return string.Format(api_token_url, api_url, api_grant_type, api_appid, api_secret);
        }

        public string getTicketUrl(string access_token)
        {
            return string.Format(api_ticket_url, api_url, access_token, api_type);
        }

        public string getSignatureUrl(string jsapi_ticket, string noncestr, long timestamp, string url)
        {
            return string.Format(api_signature_url
                , jsapi_ticket, noncestr, timestamp, url);
        }

        public NetworkCredential getProxyCredential()
        {
            return new NetworkCredential(api_proxy_user
                , api_proxy_pwd
                , api_proxy_domain);
        }

        public void Builde(Dictionary<string,string> KV)
        {
            api_url = Utility.GetStr(KV, "api_url");
            api_grant_type = Utility.GetStr(KV, "api_grant_type");
            api_debug = Utility.GetBool(KV, "api_debug");
            api_appid = Utility.GetStr(KV, "api_appid");
            api_secret = Utility.GetStr(KV, "api_secret");
            //api_access_token = Utility.GetStr(KV, "api_access_token");
            //api_jsapi_ticket = Utility.GetStr(KV, "api_jsapi_ticket");
            api_type = Utility.GetStr(KV, "api_type");
            api_use_proxy = Utility.GetBool(KV, "api_use_proxy");
            api_proxy_address = Utility.GetStr(KV, "api_proxy_address");
            api_proxy_port = Utility.GetInt(KV, "api_proxy_port");
            api_proxy_domain = Utility.GetStr(KV, "api_proxy_domain");
            api_proxy_user = Utility.GetStr(KV, "api_proxy_user");
            api_proxy_pwd = Utility.GetStr(KV, "api_proxy_pwd");
            api_token_url = Utility.GetStr(KV, "api_token_url");
            api_ticket_url = Utility.GetStr(KV, "api_ticket_url");
            //api_timestamp_prev = Utility.GetLong(KV, "api_timestamp_prev");
            api_refresh_interval = Utility.GetInt(KV, "api_refresh_interval");
            api_signature_url = Utility.GetStr(KV, "api_signature_url");
            api_jsApiList = Utility.GetList(KV, "api_jsApiList", "\n");
        }
    }
}
