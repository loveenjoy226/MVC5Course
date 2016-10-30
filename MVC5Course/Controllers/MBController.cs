using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    [LocalDebugOnly]
    public class MBController : BaseController
    {
        // GET: MB
        [Share頁面上常用的ViewBag變數資料]    //將可共用controller的code搬走 
        public ActionResult Index()
        {
            //ViewData["Temp1"] = "暫存資料";

            var b = new ClientLoginViewModel()
            {
                FirstName = "Mamie",
                LastName = "Yang"
            };

            ViewData["Temp2"] = b;

            ViewBag.Temp3 = b;
            
            return View();
        }

        public ActionResult MyForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModel c)
        {
            if(ModelState.IsValid)  //所有的欄位驗證皆通過
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }
        public ActionResult MyFormResult()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var data = db.Product.OrderBy(p => p.ProductId).Take(10);
            return View(data);
        }
        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {
            /*
             *      item.ProductId 
             *      item[i].ProductId 
             */
            //if(ModelState.IsValid)
            {
                foreach(var i in items)
                {
                    var product = db.Product.Find(i.ProductId);
                    product.ProductName = i.ProductName;
                    product.Active = i.Active;
                    product.Stock = i.Stock;
                    product.Price = i.Price;
                }
                db.SaveChanges();//有跳出例外的話 就是不符合DB的規則
                return RedirectToAction("ProductList");
            }
            return View();
        }
        public ActionResult MyError()
         {
             throw new InvalidOperationException("ERROR");
             return View();
         }
}
}