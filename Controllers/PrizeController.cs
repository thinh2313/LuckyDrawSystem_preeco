using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PreecoLuckyDraw.Models;
using System.IO;
using System.Data;
using EntityState = System.Data.Entity.EntityState;

namespace PreecoLuckyDraw.Controllers
{
    public class PrizeController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

        public ActionResult Index()
        {
            if (Session["PHONE"] != null)
            {
               
                    return View(_db.PRIZEs.ToList());
                
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult Create()
        {
            if ((string)Session["NAMECUS"] == "storyLog" || Session["PHONE"] == null)
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            PRIZE prize = new PRIZE();
            return View(prize);
        }

        [HttpPost]
        public ActionResult Create(PRIZE prize)
        {
            try
            {
                // TODO: Add insert logic here
                prize.CREATEDATE = DateTime.Now;
                _db.PRIZEs.Add(prize);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            if ((string)Session["NAMECUS"] == "storyLog" || Session["PHONE"] == null)
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            return View(_db.PRIZEs.Where(s => s.IDPRIZE == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(PRIZE prize)
        {
            try
            {

                _db.Entry(prize).State = EntityState.Modified;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id, PRIZE prize)
        {
            try
            {
                prize = _db.PRIZEs.Where(s => s.IDPRIZE == id).FirstOrDefault();
                _db.PRIZEs.Remove(prize);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
        public PartialViewResult GiftPartial()
        {
            var giftList = _db.PRIZEs.ToList();
            return PartialView(giftList);
        }
     
    }
}