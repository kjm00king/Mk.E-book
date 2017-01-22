using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xls = Microsoft.Office.Interop.Excel;
using Mk.Entity;

namespace Mk.Importer
{
    public class XlsFun
    {
        public static ManmoXlsRow ReadManmoXlsRow(Xls.Worksheet ws, int r)
        {
            ManmoXlsRow rv = new ManmoXlsRow();
            rv.Year = XlsFun.ReadStr(ws, "A", r);
            rv.Month = XlsFun.ReadStr(ws, "B", r);
            rv.No = XlsFun.ReadStr(ws, "C", r);
            rv.Group = XlsFun.ReadStr(ws, "D", r);
            rv.Title = XlsFun.ReadStr(ws, "E", r);
            rv.WechatTitle = XlsFun.ReadStr(ws, "F", r);
            rv.WechatShare = XlsFun.ReadStr(ws, "G", r);
            rv.WechatInfo = XlsFun.ReadStr(ws, "H", r);
            rv.HomeNo = XlsFun.ReadStr(ws, "I", r);
            return rv;
        }

        public static String ReadStr(Xls.Worksheet ws, String col, int row)
        {
            object tmp = ws.get_Range(col + row.ToString()).Text;
            try
            {
                return Convert.ToString(tmp);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static Boolean ReadBool(Xls.Worksheet ws, String col, int row)
        {
            object tmp = ws.get_Range(col + row.ToString()).Text;
            try
            {
                return Convert.ToBoolean(tmp);
            }
            catch
            {
                return false;
            }
        }

        public static Decimal ReadDec(Xls.Worksheet ws, String col, int row)
        {
            object tmp = ws.get_Range(col + row.ToString()).Text;
            try
            {
                return Convert.ToDecimal(tmp);
            }
            catch
            {
                return 0;
            }
        }
    }
}
