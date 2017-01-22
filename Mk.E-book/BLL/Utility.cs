using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;
using System.Configuration;

namespace Mk.E_book.BLL
{
    public class Utility
    {
        public static string GetSrcPath(String Uri)
        {
            return ConfigurationManager.AppSettings["web_root"] + Uri;
        }

        public static string GetFullPath(HttpContext Http, String Uri)
        {
            return GetFullPath(Http.Server, Uri);
        }

        public static string GetFullPath(HttpServerUtility Server, String Uri)
        {
            return Server.MapPath(ConfigurationManager.AppSettings["web_root"] + Uri);
        }

        public static string GetFullPath(HttpContextBase Http, String Uri)
        {
            return GetFullPath(Http.Server, Uri);
        }

        public static string GetFullPath(HttpServerUtilityBase Server, String Uri)
        {
            return Server.MapPath(ConfigurationManager.AppSettings["web_root"] + Uri);
        }

        public static string GetUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToLower();
        }
        
        public static String ToJson(Object Data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(Data);
        }

        public static T FromJson<T>(String Data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(Data);
        }

        public static T FromQueryString<T>(HttpRequestBase Request)
        {
            return FromNV<T>(Request.QueryString);
        }

        public static T FromNV<T>(NameValueCollection NV)
        {
            return FromJson<T>(NV["param"]);
        }

        public static T FromForm<T>(HttpRequestBase Request)
        {
            return FromNV<T>(Request.Form);
        }

        public static void Copy(Object From, Object To)
        {
            Type T = To.GetType();
            PropertyInfo[] MP = T.GetProperties();
            foreach (PropertyInfo P in MP)
            {
                var Value = P.GetValue(From, null);
                P.SetValue(To, Value, null);
            }
        }

        public static T QueryFirst<T>(IEnumerable<T> query)
        {
            if (query.Any())
                return query.First();
            else
                return Activator.CreateInstance<T>();
        }

        public static List<T> QueryList<T>(IEnumerable<T> query)
        {
            if (query.Any())
                return query.ToList();
            else
                return new List<T>();
        }

        public static T QueryMax<T>(IEnumerable<T> query, T def)
        {
            if (query.Any())
                return query.Max();
            else
                return def;
        }
        
        public static string SaveFile(HttpPostedFileBase file, HttpServerUtilityBase Server, string DirUrl, string Extension)
        {
            DirUrl = "/Upload/" + DirUrl.Replace("/", "") + "/";
            string DirFullPath = Server.MapPath(DirUrl);
            if (!Directory.Exists(DirFullPath))
                Directory.CreateDirectory(DirFullPath);
            if (string.IsNullOrEmpty(Extension))
            {
                Extension = Path.GetExtension(file.FileName);
            }
            string FileUrl = DirUrl + Guid.NewGuid().ToString().Replace("-", "") + Extension;
            string FileFullPath = Server.MapPath(FileUrl);
            file.SaveAs(FileFullPath);

            return FileUrl;
        }
    }
}