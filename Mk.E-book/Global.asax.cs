using Mk.E_book.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mk.E_book
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //  https://www.w3.org/Protocols/rfc2616/rfc2616-sec6.html
        //  200：OK / 请求成功 
        //      处理方式：获得响应的内容，进行处理  
        //  201：Created / 请求完成，结果是创建了新资源。新创建资源的 URI 可在响应的实体中得到
        //      处理方式：爬虫中不会遇到   
        //  202：Accepted / 请求被接受，但处理尚未完成
        //      处理方式：阻塞等待 
        //  204：服务器端已经实现了请求，但是没有返回新的信 息。如果客户是用户代理，则无须为此更新自身的文档视图。    
        //      处理方式：丢弃
        //  300：该状态码不被 HTTP/1.0 的应用程序直接使用， 只是作为 3XX 类型回应的默认解释。存在多个可用的被请求资源。    
        //      处理方式：若程序中能够处理，则进行进一步处理，如果程序中不能处理，则丢弃  
        //  301：请求到的资源都会分配一个永久的 URL，这样就可以在将来通过该 URL 来访问此资源 
        //      处理方式：重定向到分配的 URL    
        //  302：请求到的资源在一个不同的 URL 处临时保存 
        //      处理方式：重定向到临时的 URL   
        //  304 请求的资源未更新 
        //      处理方式：丢弃   
        //  400 Bad Request / 非法请求 
        //      处理方式：丢弃   
        //  401 Unauthorized / 未授权 
        //      处理方式：丢弃   
        //  403 Forbidden / 禁止 
        //      处理方式：丢弃   
        //  404 Not Found / 没有找到 
        //      处理方式：丢弃   
        //  5XX 回应代码以“5”开头的状态码表示服务器端发现自己出现错误，不能继续执行请求 
        //      处理方式：丢弃

        public static MemCache WebCache = new MemCache();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            WebCache.SetPath(Server.MapPath("/DATA/Json")).LoadData();
        }
    }
}
