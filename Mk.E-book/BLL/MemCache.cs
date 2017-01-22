using Mk.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace Mk.E_book.BLL
{
    public class MemCache
    {
        public List<Manmo> Manmos { get; set; }
        public WxApi Wxapi { get; set; }
        private string data_dir_path { get; set; }

        public MemCache()
        {
            Manmos = new List<Manmo>();
            Wxapi = new WxApi();
            data_dir_path = string.Empty;
        }

        public MemCache SetPath(string path)
        {
            data_dir_path = path;
            return this;
        }

        public Manmo GetManmo(string year, string month)
        {
            Manmo tmp = Utility.QueryFirst<Manmo>(
                from x in Manmos
                where x.Year == year
                where x.Month == month
                select x);
            if (tmp.Empty)
                throw new HttpException(404, null);
            return tmp;
        }

        public Manmo.Aritcle GetManmoAritcle(string year, string month, string no)
        {
            Manmo tmp = GetManmo(year, month);

            if (!tmp.Aritcles.Keys.Contains(no))
                throw new HttpException(404, null);

            return tmp.Aritcles[no];
        }

        public T LoadObj<T>()
        {
            T rv = Activator.CreateInstance<T>();

            FileInfo file = new FileInfo(data_dir_path + @"\" + typeof(T).Name + ".json");

            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(file.OpenRead(), System.Text.Encoding.UTF8))
                {
                    rv = Utility.FromJson<T>(sr.ReadToEnd());
                }
            }
            return rv;
        }

        public List<T> LoadObjs<T>()
        {
            List<T> rv = new List<T>();

            DirectoryInfo dir = new DirectoryInfo(data_dir_path + @"\" + typeof(T).Name);
            if (dir.Exists)
            {
                FileInfo[] fJsons = dir.GetFiles("*.json");

                foreach(FileInfo fJson in fJsons)
                {
                    T a = Activator.CreateInstance<T>();
                    using (StreamReader sr = new StreamReader(fJson.OpenRead(), System.Text.Encoding.UTF8))
                    {
                        rv.Add(Utility.FromJson<T>(sr.ReadToEnd()));
                    }
                }
            }

            return rv;
        }

        public void LoadData()
        {
            Manmos = LoadObjs<Manmo>();
            Wxapi = LoadObj<WxApi>();
            Wxapi.setWx( LoadObj<Wx>() ); 
        }
    }
}