using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Mk.Entity
{
    public class ManmoXlsRow
    {
        public string Year = string.Empty;
        public string Month = string.Empty;
        public string No = string.Empty;
        public string Group = string.Empty;
        public string Title = string.Empty;
        public string WechatTitle = string.Empty;
        public string WechatInfo = string.Empty;
        public string HomeNo = string.Empty;
        public string WechatShare = string.Empty;

        public ManmoXlsRow() { }

        public bool isEmpty()
        {
            return (Year + Month + No + Group + Title + WechatInfo + WechatTitle + HomeNo).Trim().Length == 0;
        }
    }

    public class Manmo: Ebook
    {
        public class WechatInfo
        {
            public string Title { get; set; }
            public string Info { get; set; }
            public string Url { get; set; }
            public bool EnableShare { get; set; }

            public WechatInfo()
            {
                Title = string.Empty;
                Info = string.Empty;
                EnableShare = false;
                Url = string.Empty;
            }
        }

        public class HomeClass
        {
            public class Link
            {
                public int UrlIndex { get; set; }
                public string SvgUrl { get; set; }
                public string Svg { get; set; }

                public Link()
                {
                    SvgUrl = string.Empty;
                    Svg = string.Empty;
                    UrlIndex = 0;
                }
            }

            public List<Link> Links { get; set; }
            public string CoverBg { get; set; }
            public string CoverDate { get; set; }
            public List<string> Urls { get; set; }
            public List<WechatInfo> WeChats { get; set; }

            public HomeClass()
            {
                Links = new List<Link>();
                CoverBg = string.Empty;
                CoverDate = string.Empty;
                Urls = new List<string>();
                WeChats = new List<WechatInfo>();
            }
        }

        public class ContentsClass
        {
            public class Link
            {
                public int UrlIndex { get; set; }
                public string Title { get; set; }

                public Link()
                {
                    UrlIndex = 0;
                    Title = string.Empty;
                }
            }

            public class Group
            {
                public List<Link> Links { get; set; }
                public string Title { get; set; }
                public int UrlIndex { get; set; }

                public Group()
                {
                    Links = new List<Link>();
                    Title = string.Empty;
                    UrlIndex = 0;
                }
            }

            public List<Group> Groups { get; set; }

            public ContentsClass()
            {
                Groups = new List<Group>();
            }
            
            public string RenderHtml()
            {
                StringBuilder html = new StringBuilder();

                html.Append("<div class=\"navbar\">");
                html.Append("<div class=\"navbar-inner\">");
                html.Append("<div class=\"left\"><a href=\"#\" class=\"back link\"> <i class=\"icon icon-back\"></i><span>Back</span></a></div>");
                html.Append("<div class=\"center sliding\">目录</div>");
                html.Append("<div class=\"right\"><a href=\"javascript:goto(0)\" class=\"link icon-only\"><i class=\"icon icon-home\"></i></a></div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("<div class=\"pages\">");
                html.Append("<div data-page=\"about\" class=\"page\">");
                html.Append("<div class=\"page-content\">");
                html.Append("<div class=\"content-block-index\">");
                html.Append("<div class=\"content-block-inner-index\">");
                html.Append("<img src=\"/Content/Manmo/panel-title.svg\" width=\"142px\" alt=\"\"  style=\"margin: 30px 20px 10px 20px\" />");
                html.Append("<ul style=\" list-style: none; margin: 0px; padding: 0px; font-family:微软雅黑;\">");

                for (int i = 0; i < Groups.Count; i++)
                {
                    Group group = Groups[i];
                    html.AppendFormat("<li class=\"panel-{0}\">", i % 2 == 1 ? "odd" : "even");
                    html.AppendFormat("<span onclick=\"goto({0})\" style=\"width:100%; display:block\">{1}</span>", group.UrlIndex, group.Title);
                    html.Append("<ul>");
                    foreach (Link link in group.Links)
                        html.AppendFormat("<li><a onclick=\"goto({0})\" class=\"close-panel\">{1}</a></li>", link.UrlIndex, link.Title);
                    html.Append("</ul>");
                    html.Append("</li>");
                }

                html.Append("</ul>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");

                return html.ToString();
            }
        }

        public class Aritcle
        {
            public string Img { get; set; }
            public string Title { get; set; }
            public WechatInfo WeChat { get; set; }

            public Aritcle()
            {
                Title = string.Empty;
                Img = string.Empty;
                WeChat = new WechatInfo();
            }

            public string RenderHtml()
            {
                StringBuilder html = new StringBuilder();

                html.Append("<div class=\"navbar\">");
                html.Append("<div class=\"navbar-inner\">");
                html.Append("<div class=\"left\"><a href=\"#\" class=\"back link\"> <i class=\"icon icon-back\"></i><span>Back</span></a></div>");
                html.Append("<div class=\"center sliding\">");
                html.Append("<a href=\"javascript:goto(1)\" class=\"link icon-only\"><i class=\"icon icon-bars\"></i></a>");
                html.Append("</div>");
                html.Append("<div class=\"right\"><a href=\"javascript:goto(0)\" class=\"link icon-only\"><i class=\"icon icon-home\"></i></a></div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("<div class=\"pages\">");
                html.Append("<div data-page=\"about\" class=\"page\">");
                html.Append("<div class=\"page-content\">");
                html.Append("<div class=\"content-block\">");
                html.Append("<div class=\"content-block-inner\">");
                html.Append("<span class=\"preloader\"></span>");
                html.Append("<img name=\"page\" width=\"100%\" src=\"" + Img + "\" class=\"lazy lazy-fadein\" />");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");

                return html.ToString();
            }
        }

        public HomeClass Home { get; set; }
        public ContentsClass Contents { get; set; }
        public Dictionary<string, Aritcle> Aritcles { get; set; }
        public bool IsHtml = false;

        public Manmo()
        {
            Home = new HomeClass();
            Contents = new ContentsClass();
            Aritcles = new Dictionary<string, Aritcle>();
        }
        
        public void Builde(List<ManmoXlsRow> Xls)
        {
            BuildeHome(Xls);
            BuildIndex(Xls);
            BuildAritcles(Xls);
        }

        private void BuildeHome(List<ManmoXlsRow> Xls)
        {
            Home.CoverBg = string.Format(@"/Content/Manmo/{0}/cover_bg.svg", Year);
            Home.CoverDate = string.Format(@"/Content/Manmo/{0}/{1}/cover_date.svg", Year, Month);

            List<ManmoXlsRow> HomeLinks = Utility.QueryList<ManmoXlsRow>(
                from x in Xls
                orderby x.HomeNo
                where x.HomeNo != string.Empty
                select x);

            foreach (ManmoXlsRow HomeLink in HomeLinks)
            {
                Home.Links.Add(new HomeClass.Link()
                {
                    SvgUrl = string.Format(@"/Content/Manmo/{0}/{1}/cover_lnk{2}.svg", Year, Month, HomeLink.HomeNo),
                    UrlIndex = int.Parse(HomeLink.No) + 1,
                });
            }

            Home.Urls.Add(string.Format(@"/Ebook/Manmo/{0}/{1}/Home", Year, Month));
            Home.WeChats.Add(new WechatInfo());

            if(IsHtml)
                Home.Urls.Add(string.Format(@"/Content/Manmo/{0}/{1}/Contents.html", Year, Month));
            else
                Home.Urls.Add(string.Format(@"/Ebook/Manmo/{0}/{1}/Contents", Year, Month));

            Home.WeChats.Add(new WechatInfo());

            foreach(ManmoXlsRow XlsRow in Xls)
            {
                if (IsHtml)
                    Home.Urls.Add(string.Format(@"/Content/Manmo/{0}/{1}/Aritcles/{2}.html", Year, Month, XlsRow.No));
                else
                    Home.Urls.Add(string.Format(@"/Ebook/Manmo/{0}/{1}/Aritcles/{2}", Year, Month, XlsRow.No));

                Home.WeChats.Add(new WechatInfo()
                {
                    EnableShare = XlsRow.WechatShare.ToUpper() == "Y",
                    Title = XlsRow.WechatTitle,
                    Info = XlsRow.WechatInfo,
                    Url = string.Format(@"/Ebook/Manmo/{0}/{1}/Aritcle/{2}", Year, Month, XlsRow.No),
                });
            }
        }

        private void BuildIndex(List<ManmoXlsRow> Xls)
        {
            List<string> XlsGroups = Utility.QueryList<string>(
                (from x in Xls select x.Group).Distinct());

            foreach (string XlsGroup in XlsGroups)
            {
                ContentsClass.Group IndexGroup = new ContentsClass.Group()
                {
                    Title = XlsGroup,
                };

                List<ManmoXlsRow> XlsLinks = Utility.QueryList<ManmoXlsRow>(
                    from x in Xls where x.Group == XlsGroup select x);

                foreach (ManmoXlsRow XlsLink in XlsLinks)
                {
                    IndexGroup.Links.Add(new ContentsClass.Link()
                    {
                        Title = XlsLink.Title,
                        UrlIndex = int.Parse(XlsLink.No) + 1,
                    });
                }

                if (XlsLinks.Count > 0)
                    IndexGroup.UrlIndex = int.Parse(XlsLinks[0].No) + 1;

                Contents.Groups.Add(IndexGroup);
            }
        }

        private void BuildAritcles(List<ManmoXlsRow> Xls)
        {
            foreach (ManmoXlsRow XlsRow in Xls)
            {
                Aritcles.Add(XlsRow.No,
                    new Aritcle()
                    {
                        Title= XlsRow.Title,
                        Img = string.Format(@"/Content/Manmo/{0}/{1}/a{2}.svg", Year, Month, XlsRow.No),
                        WeChat = new WechatInfo()
                        {
                            EnableShare = XlsRow.WechatShare.ToUpper() == "TRUE",
                            Title = XlsRow.WechatTitle,
                            Info = XlsRow.WechatInfo,
                            Url = string.Format(@"/Ebook/Manmo/{0}/{1}/Aritcle/{2}", Year, Month, XlsRow.No),
                        }
                    });
            }
        }
    }
}
