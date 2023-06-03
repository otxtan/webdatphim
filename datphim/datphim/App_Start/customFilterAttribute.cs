using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace datphim
{
    public class customFilterAttribute : FilterAttribute, IAuthorizationFilter, IExceptionFilter
    {


        public void OnAuthorization(AuthorizationContext filterContext)
        {
            /*var requestUrl = filterContext.HttpContext.Request.Url;*/

            // Kiểm tra xem đường dẫn của request có phù hợp hay không
            /*if (!IsUrlValid(requestUrl))
            {
                // Nếu không phù hợp, chuyển hướng về trang chủ
                filterContext.Result = new RedirectResult("~/Home/Trangchu");
            }*/
            if (filterContext.HttpContext.Session["UserName"] == null)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Account", action = "Login" }));
            /* throw new NotImplementedException();*/
        }
        /*private bool IsUrlValid(Uri url)
        {
            if (url.AbsolutePath.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }*/
        public void OnException(ExceptionContext filterContext)
        {
            /*filterContext.Result = new ViewResult { ViewName = "Error" };*/
            /*filterContext.ExceptionHandled = true;*/
            /*throw new Exception();*/
            /*throw new NotImplementedException();*/
        }

    }
}