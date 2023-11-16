using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PreecoLuckyDraw.Models;
using EntityState = System.Data.Entity.EntityState;

namespace PreecoLuckyDraw.Controllers
{
    public class USERsController : Controller
    {
        private DbPreecoLuckyDrawEntities db = new DbPreecoLuckyDrawEntities();

        // GET: USERs
        public ActionResult Index()
        {

            var i = Session["STATUS"];
            if ( (int)i == 2313 || (int)i==1)
            {
                return View(db.USERS.ToList());
            }
                return Content("Vượt quyền truy cập");

        }

        // GET: USERs/Details/5
        // GET: USERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,DATEOFBIRTH,PHONE,PASSWORD,STATUSS")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USERS.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uSER);
        }

        // GET: USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((int)Session["STATUS"] == 2313)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                USER uSER = db.USERS.Find(id);
                if (uSER == null)
                {
                    return HttpNotFound();
                }
                return View(uSER);
            }
            return Content("Vượt phạm vi truy cập");
        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,DATEOFBIRTH,PHONE,PASSWORD,STATUSS")] USER uSER)
        {
            try          
            {
                db.Entry(uSER).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { 
                return View(uSER);
            }
        }

        // GET: USERs/Delete/5

        // POST: USERs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
       
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USERS.Where(s=>s.ID==id).FirstOrDefault();
            db.USERS.Remove(uSER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
