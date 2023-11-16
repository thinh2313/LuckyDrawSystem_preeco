using OfficeOpenXml;
using PreecoLuckyDraw.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PreecoLuckyDraw.Controllers
{
    public class LuckynumberController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

        // GET: Luckynumber
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Setting_employee()
        {if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var i = Session["IDCUS"];
            if (Session["PHONE"] != null)
            {
                if ((int)Session["STATUS"] == 1 || (int)Session["STATUS"] == 2313)
                {
                    return View(_db.EMPLOYEEs.ToList());

                }
                return View(_db.CAMPAIGNs.Where(s => s.ID == (int)i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult Setting_luckynumber()
        {
            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var i = Session["IDCUS"];
            if (Session["PHONE"] != null)
            {
                if ((int)Session["STATUS"] == 1 || (int)Session["STATUS"] == 2313)
                {
                    return View(_db.LUCKYNUMBERs.ToList());
                }
                return View(_db.CAMPAIGNs.Where(s => s.ID == (int)i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult Nestle_YEP()
        {

            return View();
        }
        // GET: Luckynumber/Details/5
        public ActionResult Details(string id)
        {
            return View(_db.EMPLOYEEs.Where(s => s.ID == id).FirstOrDefault());
        }
       
        // GET: Luckynumber/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Luckynumber/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Luckynumber/Edit/5
        

        // POST: Luckynumber/Edit/5
        public int Luckyperson()
        {
            Random rd = new Random();
           var list = _db.LUCKYNUMBERs.Where(s=>s.STATUS==false).ToList();
           int numb = rd.Next(list.Count);
            //var x = _db.LUCKYNUMBERs.Where(s => s.ID == numb).FirstOrDefault();
            var x = list[numb];
            x.STATUS = true;
            _db.SaveChanges();
            return (int)x.NUMB;
        }
        public void Edit(string id)
        {
            EMPLOYEE emp = _db.EMPLOYEEs.Where(s => s.ID == id).FirstOrDefault();
            emp.TIMEDRAW--;
            try
            {
                _db.Entry(emp).State =EntityState.Modified;

                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                
            }
        }

        // GET: Luckynumber/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Luckynumber/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Wishes(string id)
        {
            EMPLOYEE emp = _db.EMPLOYEEs.Where(s => s.ID == id).FirstOrDefault();

            return View(emp);
        }
        
            public ActionResult UploadLuckynumber(FormCollection formCollection)
        {
            var usersList = new List<LUCKYNUMBER>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFileEx"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new LUCKYNUMBER();
                            if (workSheet.Cells[rowIterator, 1].Value == null)
                            { user.ID = 0; }
                            else
                            { user.ID =Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value); }

                            if (workSheet.Cells[rowIterator, 2].Value == null)
                            { user.NUMB = null; }
                            else
                            {
                                user.NUMB = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            }
                            if (workSheet.Cells[rowIterator, 3].Value == null)
                            { user.CONTENTS = null; }
                            else
                            {
                                user.CONTENTS = workSheet.Cells[rowIterator, 3].Value.ToString();
                            }

                            if (workSheet.Cells[rowIterator, 4].Value == null)
                            {
                                user.IMAGE = null;
                            }
                            else
                            {
                                user.IMAGE = workSheet.Cells[rowIterator, 4].Value.ToString();
                            }
                            if (workSheet.Cells[rowIterator, 5].Value == null)
                            { user.STATUS = false; }
                            else
                            { user.STATUS = false; }
                            if (workSheet.Cells[rowIterator, 6].Value == null)
                            { user.NOTE = null; }
                            else { user.NOTE = workSheet.Cells[rowIterator, 6].Value.ToString(); }
                            
                            usersList.Add(user);
                        }
                    }
                }
            }
            using (_db)
            {
                foreach (var item in usersList)
                {
                    _db.LUCKYNUMBERs.Add(item);
                }
                _db.SaveChanges();

            }
            return RedirectToAction("Setting_luckynumber");
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            var usersList = new List<EMPLOYEE>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFileEx"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new EMPLOYEE();
                            if (workSheet.Cells[rowIterator, 1].Value == null)
                            { user.ID = null; }
                            else
                            { user.ID = workSheet.Cells[rowIterator, 1].Value.ToString(); }

                            if (workSheet.Cells[rowIterator, 2].Value == null)
                            { user.LASTNAME = null; }
                            else
                            {
                                user.LASTNAME = workSheet.Cells[rowIterator, 2].Value.ToString();
                            }
                            if (workSheet.Cells[rowIterator, 3].Value == null)
                            { user.FIRSTNAME = null; }
                            else
                            {
                                user.FIRSTNAME = workSheet.Cells[rowIterator, 3].Value.ToString();
                            }

                            if (workSheet.Cells[rowIterator, 4].Value == null)
                            {
                                user.BU = null;
                            }
                            else
                            {
                                user.BU = workSheet.Cells[rowIterator, 4].Value.ToString();
                            }
                            if (workSheet.Cells[rowIterator, 5].Value == null)
                            { user.TIMEDRAW = 3; }
                            else
                            { user.TIMEDRAW =Convert.ToInt32(workSheet.Cells[rowIterator, 5].Value.ToString()); }
                            if (workSheet.Cells[rowIterator, 6].Value == null)
                            { user.GIFT = null; }
                            else { user.GIFT = workSheet.Cells[rowIterator, 6].Value.ToString(); }
                            if (workSheet.Cells[rowIterator, 7].Value == null)
                            { user.RESULT = null; }
                            else { user.RESULT = workSheet.Cells[rowIterator, 7].Value.ToString(); }
                            
                            usersList.Add(user);
                        }
                    }
                }
            }
            using (_db)
            {
                foreach (var item in usersList)
                {
                    _db.EMPLOYEEs.Add(item);
                }
                _db.SaveChanges();

            }
            return RedirectToAction("Setting_employee");
        }
        public ActionResult clearEmployee()
        {

            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var i = Session["IDCUS"];
            if (Session["PHONE"] != null)
            {
                if ((int)Session["STATUS"] == 2313)
                {

                    try
                    {
                        var listJoiners = _db.EMPLOYEEs.ToList();

                        foreach (var item in listJoiners)
                        {
                            _db.EMPLOYEEs.Remove(item);

                        }
                        _db.SaveChanges();
                        return RedirectToAction("Setting_employee");
                    }
                    catch
                    {
                        return Content("This data using other table, Error Delete");
                    }
                }
                return View(_db.CAMPAIGNs.Where(s => s.ID == (int)i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult clearluckynumber()
        {

            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var i = Session["IDCUS"];
            if (Session["PHONE"] != null)
            {
                if ((int)Session["STATUS"] == 2313)
                {

                    try
                    {
                        var listJoiners = _db.LUCKYNUMBERs.ToList();

                        foreach (var item in listJoiners)
                        {
                            _db.LUCKYNUMBERs.Remove(item);

                        }
                        _db.SaveChanges();
                        return RedirectToAction("Setting_luckynumber");
                    }
                    catch
                    {
                        return Content("This data using other table, Error Delete");
                    }
                }
                return View(_db.CAMPAIGNs.Where(s => s.ID == (int)i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
    }
}
