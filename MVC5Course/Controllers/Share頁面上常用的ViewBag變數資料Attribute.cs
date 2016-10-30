using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class Share頁面上常用的ViewBag變數資料Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["Temp1"] = "暫存資料";    //Controller 就是控制器的Controller
        }
    }
}