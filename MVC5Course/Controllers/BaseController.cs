using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected FabricsEntities db = new FabricsEntities();
        protected override void HandleUnknownAction(string actionName)
        {//error 404
            //base.HandleUnknownAction(actionName);
            this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        }
    }
}