using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PreecoLuckyDraw.Models;

namespace PreecoLuckyDraw.Controllers
{
    public class HomeController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

        public ActionResult Index()
        {
            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("tài khoản không có chức năng này");
            }
            return View();
        }

       
        public ActionResult ListWinner()
        {
            ViewBag.Message = "LuckyWheel page.";

            var giftList = _db.WINNERs.ToList();
            return View(giftList);
        }
    }
}