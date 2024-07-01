using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTourDuLich.Models;

namespace QLTourDuLich.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Statistical
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails on o.Id equals od.OrderId
                        join p in db.Products on od.ProductId equals p.Id
                        select new
                        {
                            CreatedDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice
                        };

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.Parse(fromDate);
                query = query.Where(x => DbFunctions.TruncateTime(x.CreatedDate) >= DbFunctions.TruncateTime(startDate));
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.Parse(toDate);
                query = query.Where(x => DbFunctions.TruncateTime(x.CreatedDate) <= DbFunctions.TruncateTime(endDate));
            }

            // Tính tổng TotalBuy và TotalSell
            decimal totalBuy = query.Sum(y => y.Quantity * y.OriginalPrice);
            decimal totalSell = query.Sum(y => y.Quantity * y.Price);

            // Tạo kết quả cuối cùng
            var result = new
            {
                DoanhThu = totalSell,
                LoiNhuan = totalSell - totalBuy
            };

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetNumberOfTour()
        {
            int count = db.Products.Count();
            return Json(new { Data = count }, JsonRequestBehavior.AllowGet);
        }

    }
}