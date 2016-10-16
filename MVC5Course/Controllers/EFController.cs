using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        public ActionResult Index()
        {
            var data = db.Product.Where(p => p.ProductName.Contains("White"));
            return View(data);
        }
        public ActionResult Create()
        {
            var product = new Product()
            {
                ProductName = "White Cat",
                Active = true,
                Price = 199,
                Stock = 5,
                IsDeleted = false
            };

            db.Product.Add(product);

            db.SaveChanges();//此步驟才會改DB
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            //db.OrderLine.Where(p => p.Product.ProductId==id);
            //product.OrderLine; 如果知道Fabrics.edmx檔Product的導覽屬性跟OrderLine的關聯 就會寫這個

            /* 錯誤示範 db.SaveChanges();只需做一次
            foreach (var item in product.OrderLine.ToList())
            {
                db.OrderLine.Remove(item);
                db.SaveChanges();
            }
            */
            db.OrderLine.RemoveRange(product.OrderLine);

            db.Product.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var product = db.Product.Find(id);
            
            return View(product);
        }
        public ActionResult Update(int id)
        {
            var product = db.Product.Find(id);
            product.ProductName += "!";

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var vErrores in entityErrors.ValidationErrors)
                    {//一般錯誤訊息都看不懂 此自訂完整訊息的錯誤-ntity Framework 發生驗證例外時的處裡方法
                        throw new DbEntityValidationException(vErrores.PropertyName + "發生錯誤" + vErrores.ErrorMessage);
                    }
                }
                throw;
            }

            return RedirectToAction("Index");
        }
        //public ActionResult Add20Percent()
        //{
        //    var data = db.Product.Where(p => p.ProductName.Contains("White"));
        //    foreach (var item in data)
        //    {
        //        if(item.Price.HasValue)
        //        {//有值才會動作
        //            item.Price = item.Price.Value * 1.2m; 
        //        }
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

            
        public ActionResult Add20Percent()
        {//效能調教版本 此方法就不用使用 db.SaveChanges();
            string str = "%White%"; //這樣比較好 避免被修改文字
            db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price=Price*1.2 WHERE ProductName LIKE @p0", str); //@p0(1.2.3....)就是帶入str字串
            return RedirectToAction("Index");
        }

        public ActionResult ClientContribution()
        {
            var data = db.vw_ClientContribution.Take(10);
            return View(data);
        }

        public ActionResult ClientContribution2(string keyword="Mary")
        {
            var data = db.Database.SqlQuery<ClientContributionViewModel>(@"
	        SELECT
                c.ClientId,
		         c.FirstName,
		         c.LastName,
		         (SELECT SUM(o.OrderTotal) 
		          FROM [dbo].[Order] o 
		          WHERE o.ClientId = c.ClientId) as OrderTotal
	        FROM 
		        [dbo].[Client] as c
            WHERE
                c.FirstName LIKE @p0", "%" + keyword + "%"
                );
            return View(data);
        }

        public ActionResult ClientContribution3(string keyword)
        {
            var data = db.usp_GetClientContribution(keyword);
            return View(data);
        }
    }
}