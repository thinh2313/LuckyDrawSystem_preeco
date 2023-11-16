using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PreecoLuckyDraw.Models;
using System.IO;


namespace PreecoLuckyDraw.Controllers
{
    public class JoinerController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

        // GET: Joiner
        public ActionResult Index()
        {
            return View(_db.JOINERs.OrderBy(s => s.IDJOINER).ToList());
            //return Json(_db.JOINERs.Where(s => s.IDCAMPAIGN == id).ToList());

        }
        [HttpGet]
        public JsonResult listJoiners()
        {

           //var id = (int)Session["IDCAMPAIGN"];
            var joiners = _db.JOINERs.ToList();
            IList<JOINER> dataList = new List<JOINER>();
            foreach (var item in joiners)
            {
                var joiner = new JOINER();
                //joiner.IDCAMPAIGN = id;

                joiner.IDCAMPAIGN = 1;
                joiner.ID = item.ID;
                joiner.IDJOINER = item.IDJOINER;
                joiner.NAME = item.NAME;
                joiner.PHONE = item.PHONE;
                dataList.Add(joiner);
            }
            _db.SaveChanges();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Upload(FormCollection formCollection)
        //{
        //    var usersList = new List<JOINER>();
        //    if (Request != null)
        //    {
        //        HttpPostedFileBase file = Request.Files["UploadedFileEx"];
        //        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
        //        {
        //            string fileName = file.FileName;
        //            string fileContentType = file.ContentType;
        //            byte[] fileBytes = new byte[file.ContentLength];
        //            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
        //            ExcelPackage.LicenseContext = LicenseContext.Commercial;

        //            using (var package = new ExcelPackage(file.InputStream))
        //            {
        //                var currentSheet = package.Workbook.Worksheets;
        //                var workSheet = currentSheet.First();
        //                var noOfCol = workSheet.Dimension.End.Column;
        //                var noOfRow = workSheet.Dimension.End.Row;
        //                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
        //                {
        //                    var user = new JOINER();
        //                    user.IDJOINER= workSheet.Cells[rowIterator, 1].Value.ToString();
        //                    user.PHONE = workSheet.Cells[rowIterator, 2].Value.ToString();
        //                    user.NAME = workSheet.Cells[rowIterator, 3].Value.ToString();
        //                    user.DATEOFBIRTH = null;
        //                    user.ADDRESS = workSheet.Cells[rowIterator, 5].Value.ToString();
        //                    user.EMAIL = workSheet.Cells[rowIterator, 6].Value.ToString();
        //                    user.IDCAMPAIGN =1;
        //                    usersList.Add(user);
        //                }
        //            }
        //        }
        //    }
        //    using (_db)
        //    {
        //        foreach (var item in usersList)
        //        {
        //            _db.JOINERs.Add(item);
        //        }
        //        _db.SaveChanges();

        //    }
        //    return RedirectToAction("Index");
        //}
    }
}