using Mk.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xls = Microsoft.Office.Interop.Excel;
using System.Web.Script.Serialization;

namespace Mk.Importer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReadXls_Click(object sender, EventArgs e)
        {
            try
            {
                ConverterClass Converter = new ConverterClass()
                {
                    XlsPath = txtXls.Text,
                    WebPath = txtWeb.Text,
                    HtmlPath = txtHtml.Text,
                    Output = txtJson.Text,
                    IsHtml = isHtml.Checked,
                    Series = ddlSeries.Text,
                };

                typeof(ConverterClass).GetMethod("Convert" + ddlSeries.Text).Invoke(Converter, null);

                Converter.ConvertWx();

                MessageBox.Show("OK!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        
        public class ConverterClass
        {
            public string XlsPath { get; set; }
            public string WebPath { get; set; }
            public string HtmlPath { get; set; }
            public string Output { get; set; }
            public bool IsHtml { get; set; }
            public string Series { get; set; }

            public void ConvertManmo()
            {
                List<ManmoXlsRow> XlsRows = ReadXls<ManmoXlsRow>(XlsPath, "Manmo");

                DirectoryInfo output = new DirectoryInfo(Output + @"\Manmo");
                if (output.Exists)
                    output.Delete(true);
                output.Create();

                List<string> Years = Utility.QueryList<string>(XlsRows.Select(o => o.Year).Distinct().OrderBy(o => o));

                foreach (string Year in Years)
                {
                    List<string> Months = Utility.QueryList<string>(
                        (from x in XlsRows
                         where x.Year == Year
                         orderby x.Month
                         select x.Month).Distinct());

                    foreach (string Month in Months)
                    {
                        List<ManmoXlsRow> ABook = Utility.QueryList<ManmoXlsRow>(
                            from x in XlsRows
                            where x.Year == Year
                            where x.Month == Month
                            orderby x.No
                            select x);

                        Manmo tmp = new Manmo() { Year = Year, Month = Month, Empty = ABook.Count == 0, IsHtml = IsHtml };
                        tmp.Builde(ABook);

                        foreach(Manmo.HomeClass.Link hlink in tmp.Home.Links)
                        {
                            FileInfo f_home_lnk_svg_url = new FileInfo(WebPath + hlink.SvgUrl);
                            if (f_home_lnk_svg_url.Exists)
                            {
                                using(StreamReader sr = new StreamReader(f_home_lnk_svg_url.OpenRead(), Encoding.UTF8))
                                {
                                    hlink.Svg = sr.ReadToEnd();

                                    int _svg_b = hlink.Svg.IndexOf("<svg");
                                    _svg_b = hlink.Svg.IndexOf(">", _svg_b) + 1;
                                    int _svg_e = hlink.Svg.IndexOf("</svg>");
                                    hlink.Svg = hlink.Svg.Substring(_svg_b, _svg_e - _svg_b);
                                }
                            }
                        }

                        string file_path = output.FullName + @"\" + Guid.NewGuid().ToString().Replace("-", "") + ".json";
                        if (File.Exists(file_path))
                            File.Delete(file_path);

                        using (FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate))
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            Write(fs, serializer.Serialize(tmp));
                        }

                        if (tmp.IsHtml)
                        {
                            DirectoryInfo html_dir = new DirectoryInfo(HtmlPath + @"\" + Series + @"\" + Year + @"\" + Month);
                            if (!html_dir.Exists)
                                html_dir.Create();

                            FileInfo fHome = new FileInfo(html_dir.FullName + @"\" + "Home.html");
                            if (fHome.Exists) { fHome.Delete(); }
                            using (FileStream fs = new FileStream(fHome.FullName, FileMode.OpenOrCreate))
                            {
                                //Write(fs, tmp.Contents.RenderHtml());
                            }

                            FileInfo fContents = new FileInfo(html_dir.FullName + @"\" + "Contents.html");
                            if (fContents.Exists) { fContents.Delete(); }
                            using (FileStream fs = new FileStream(fContents.FullName, FileMode.OpenOrCreate))
                            {
                                Write(fs, tmp.Contents.RenderHtml());
                            }


                            DirectoryInfo aritcles_dir = new DirectoryInfo(html_dir.FullName + @"\Aritcles");
                            aritcles_dir.Create();

                            foreach (string no in tmp.Aritcles.Keys)
                            {
                                FileInfo fAritcle = new FileInfo(aritcles_dir.FullName + @"\" + no + ".html");
                                if (fAritcle.Exists) { fAritcle.Delete(); }
                                using (FileStream fs = new FileStream(fAritcle.FullName, FileMode.OpenOrCreate))
                                {
                                    Write(fs, tmp.Aritcles[no].RenderHtml());
                                }
                            }
                        }
                    }
                }
            }

            public void ConvertWx()
            {
                Dictionary<string, string> KV = ReadKV(XlsPath, "Wx");

                FileInfo output = new FileInfo(Output + @"\Wx.json");
                
                if (output.Exists)
                    output.Delete();

                Wx tmp = new Wx();
                tmp.Builde(KV);

                using (FileStream fs = new FileStream(output.FullName, FileMode.OpenOrCreate))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Write(fs, serializer.Serialize(tmp));
                }
            }

            private List<T> ReadXls<T>(string path, string sheet_name)
            {

                Xls.Application app = null;
                Xls.Workbook book = null;
                Xls.Worksheet ws = null;

                try
                {
                    app = new Xls.Application();

                    book = app.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    ws = (Xls.Worksheet)book.Worksheets.get_Item(sheet_name);

                    int r = 2;

                    List<T> rv = new List<T>();
                    T tmp;

                    do
                    {
                        tmp = Activator.CreateInstance<T>();

                        tmp = (T)typeof(XlsFun).GetMethod("Read" + typeof(T).Name).Invoke(tmp, new object[] { ws, r });

                        if ((bool)typeof(T).GetMethod("isEmpty").Invoke(tmp, null))
                            return rv;

                        rv.Add(tmp);
                        r++;
                    }
                    while (true);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    if (book != null)
                        book.Close(false, Type.Missing, Type.Missing);

                    if (app != null)
                        app.Quit();

                    ws = null;
                    book = null;
                    app = null;

                    GC.Collect();
                }
            }

            private Dictionary<string,string> ReadKV(string path, string sheet_name)
            {

                Xls.Application app = null;
                Xls.Workbook book = null;
                Xls.Worksheet ws = null;

                try
                {
                    app = new Xls.Application();

                    book = app.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                        , Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    ws = (Xls.Worksheet)book.Worksheets.get_Item(sheet_name);

                    int r = 2;

                    Dictionary<string, string> rv = new Dictionary<string, string>();

                    do
                    {
                        string key = XlsFun.ReadStr(ws, "A", r);
                        string value = XlsFun.ReadStr(ws, "B", r);

                        if (key.Trim() == string.Empty)
                            return rv;
                        else if (!rv.Keys.Contains(key))
                        {
                            rv.Add(key, value);
                        }

                        r++;
                    }
                    while (true);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    if (book != null)
                        book.Close(false, Type.Missing, Type.Missing);

                    if (app != null)
                        app.Quit();

                    ws = null;
                    book = null;
                    app = null;

                    GC.Collect();
                }
            }
            
            private void Write(FileStream fs, string content)
            {
                byte[] buff = System.Text.Encoding.UTF8.GetBytes(content);
                fs.Write(buff, 0, buff.Length);
            }
        }

        private void btnLoadTest_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            E_book.BLL.MemCache mem = new E_book.BLL.MemCache().SetPath(txtJson.Text);
            mem.LoadData();

            Console.WriteLine((DateTime.Now - dt).TotalSeconds);
        }

        private void btnCheckWxApi_Click(object sender, EventArgs e)
        {
            try
            {
                E_book.BLL.MemCache mem = new E_book.BLL.MemCache().SetPath(txtJson.Text);
                mem.LoadData();

                if (mem.Wxapi.check())
                {
                    FileInfo output = new FileInfo(txtJson.Text + @"\WxApi.json");

                    if (output.Exists)
                        output.Delete();

                    using (StreamWriter sw = new StreamWriter(output.Create(), Encoding.UTF8))
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        sw.Write(serializer.Serialize(mem.Wxapi));
                    }
                }
                MessageBox.Show("OK!");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
