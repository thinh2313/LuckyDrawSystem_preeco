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
    public class GiftController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

        // GET: Gift
        public ActionResult Index()
        {
            var i = Session["IDCUS"];
            if (Session["PHONE"] != null)
            {
                return View(_db.GIFTs.Where(s => s.ID == (int)i).ToList());
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
            GIFT prize = new GIFT();
            return View(prize);
        }

        [HttpPost]
        public ActionResult Create(GIFT prize)
        {
            try
            {
                prize.CREATEDATE = DateTime.Now;
                prize.ID = (int)Session["IDCUS"];
                if (prize.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(prize.UploadImage.FileName);
                    string extension = Path.GetExtension(prize.UploadImage.FileName);
                    fileName = fileName + extension;
                    prize.IMAGE = "/Assets/img/" + fileName;
                    prize.UploadImage.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
                _db.GIFTs.Add(prize);
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
            var prize = _db.GIFTs.Where(s => s.IDGIFT == id).FirstOrDefault();

            if ((string)Session["NAMECUS"] == "storyLog" || Session["PHONE"] == null)
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            //if (prize.UploadImage != null)
            //{
            //    string fileName = Path.GetFileNameWithoutExtension(prize.UploadImage.FileName);
            //    string extension = Path.GetExtension(prize.UploadImage.FileName);
            //    fileName = fileName + extension;
            //    prize.IMAGE = "/Assets/img/" + fileName;
            //    prize.UploadImage.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
            //}
            return View(prize);
        }

        [HttpPost]
        public ActionResult Edit(GIFT prize)
        {
            try
            {
                if (prize.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(prize.UploadImage.FileName);
                    string extension = Path.GetExtension(prize.UploadImage.FileName);
                    fileName = fileName + extension;
                    prize.IMAGE = "/Assets/img/" + fileName;
                    prize.UploadImage.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
                prize.CREATEDATE = DateTime.Now;
                prize.ID = (int)Session["IDCUS"];
                if (prize.UploadImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(prize.UploadImage.FileName);
                    string extension = Path.GetExtension(prize.UploadImage.FileName);
                    fileName = fileName + extension;
                    prize.IMAGE = "/Assets/img/" + fileName;
                    prize.UploadImage.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
              


                _db.Entry(prize).State = EntityState.Modified;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id, GIFT prize)
        {
            try
            {
                prize = _db.GIFTs.Where(s => s.IDGIFT == id).FirstOrDefault();
                _db.GIFTs.Remove(prize);
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