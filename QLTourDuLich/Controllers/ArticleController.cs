﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTourDuLich.Models;
using QLTourDuLich.Models.EF;

namespace QLTourDuLich.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            var pageSize = 1;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Posts> items = db.Posts.Where(x=> x.IsActive ==true).OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        } 
        public ActionResult Detail(int id)
        {
            var item = db.Posts.Find(id);
            return View(item);
        }
        public ActionResult Partial_News_Home()
        {
            var items = db.Posts.Where(x => x.IsActive == true).Take(3).ToList();
            return PartialView(items);
        }
    }
}