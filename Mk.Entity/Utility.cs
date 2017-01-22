using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mk.Entity
{
    public class Utility
    {
        public static List<T> QueryList<T>(IEnumerable<T> query)
        {
            if (query.Any())
                return query.ToList();
            else
                return new List<T>();
        }

        public static string GetStr(Dictionary<string, string> KV, string key)
        {
            if (KV.Keys.Contains(key))
                return KV[key];
            else
                return string.Empty;
        }

        public static List<string> GetList(Dictionary<string, string> KV, string key)
        {
            return GetList(KV, key, "\r\n");
        }
        public static List<string> GetList(Dictionary<string, string> KV, string key, string split)
        {
            string[] rv = GetStr(KV, key).Split(new string[] { split }, StringSplitOptions.None);
            if (rv.Length > 0)
                return rv.ToList();
            else
                return new List<string>();
        }

        public static bool GetBool(Dictionary<string, string> KV, string key)
        {
            return GetBool(KV, key, false);
        }
        public static bool GetBool(Dictionary<string, string> KV, string key, bool def)
        {
            return Cbool(GetStr(KV, key), def);
        }

        public static int GetInt(Dictionary<string, string> KV, string key)
        {
            return GetInt(KV, key, 0);
        }
        public static int GetInt(Dictionary<string, string> KV, string key, int def)
        {
            return Cint(GetStr(KV, key), def);
        }
        
        public static long GetLong(Dictionary<string, string> KV, string key)
        {
            return GetLong(KV, key, 0);
        }
        public static long GetLong(Dictionary<string, string> KV, string key, long def)
        {
            return Clong(GetStr(KV, key), def);
        }

        public static bool Cbool(string key)
        {
            return Cbool(key, false);
        }
        public static bool Cbool(string key, bool def)
        {
            bool rv = def;
            bool.TryParse(key, out rv);
            return rv;
        }

        public static int Cint(string key)
        {
            return Cint(key, 0);
        }
        public static int Cint(string key, int def)
        {
            int rv = def;
            int.TryParse(key, out rv);
            return rv;
        }
        
        public static long Clong(string key)
        {
            return Clong(key, 0);
        }
        public static long Clong(string key, long def)
        {
            long rv = def;
            long.TryParse(key, out rv);
            return rv;
        }
    }
}
