using System;
using System.Web.Mvc;//此為MVC使用 另一個為API使用

namespace MVC5Course.Controllers
{
    public class LocalDebugOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(!filterContext.HttpContext.Request.IsLocal)  //不是在local的話
            {
                filterContext.Result = new RedirectResult("/"); //Result就是ActionResult
            }
        }
    }
}