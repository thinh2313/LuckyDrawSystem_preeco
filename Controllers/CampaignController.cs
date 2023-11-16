using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PreecoLuckyDraw.Models;
using System.Data;
using OfficeOpenXml;
using System.IO;
using EO.WebBrowser.DOM;
using EntityState = System.Data.Entity.EntityState;

namespace PreecoLuckyDraw.Controllers
{
    public class CampaignController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();
        public ActionResult Index()
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
                    return View(_db.CAMPAIGNs.ToList());

                }
                return View(_db.CAMPAIGNs.Where(s => s.ID == (int)i).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public ActionResult Details(int id)
        {
            if ((string)Session["NAMECUS"] == "storyLog" || Session["PHONE"] == null)
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var idcus = Session["IDCUS"];
            var namecus = Session["NAMECUS"];
            CAMPAIGN camp = new CAMPAIGN();
            camp.IDCAMPAIGN = id;

            Session["CAMPAIGN"] = camp.IDCAMPAIGN;


            return View(_db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault());
        }

        public ActionResult Create()
        {
            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var i = Session["PHONE"];
            if (i != null)
            {
                CAMPAIGN campaign = new CAMPAIGN();
                return View(campaign);
            }

            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Create(CAMPAIGN campaign)
        {

            try
            {
                campaign.STARTDATE = DateTime.Now;
                campaign.WINNERS = 0;
                campaign.CREATEDATE = DateTime.Now;
                campaign.ID = (int)Session["IDCUS"];
                _db.CAMPAIGNs.Add(campaign);
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
            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return Content("Vượt quyền truy cập, vui lòng thử lại sau");
            }
            var i = Session["PHONE"];
            if (i != null)
            {
                CAMPAIGN campaign = new CAMPAIGN();
                if (campaign.UploadBackground != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(campaign.UploadBackground.FileName);
                    string extension = Path.GetExtension(campaign.UploadBackground.FileName);
                    fileName = fileName + extension;
                    campaign.BACKGROUND = "/Assets/img/" + fileName;
                    campaign.UploadBackground.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
                if (campaign.UploadLogo != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(campaign.UploadLogo.FileName);
                    string extension = Path.GetExtension(campaign.UploadLogo.FileName);
                    fileName = fileName + extension;
                    campaign.LOGO = "/Assets/img/" + fileName;
                    campaign.UploadLogo.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
                campaign.STATUS = true;
                return View(_db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault());
            }
            return RedirectToAction("Login", "Login");

        }

        [HttpPost]
        public ActionResult Edit(CAMPAIGN campaign)
        {
            try
            {

                if (campaign.UploadBackground != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(campaign.UploadBackground.FileName);
                    string extension = Path.GetExtension(campaign.UploadBackground.FileName);
                    fileName = fileName + extension;
                    campaign.BACKGROUND = "/Assets/img/" + fileName;
                    campaign.UploadBackground.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
                if (campaign.UploadLogo != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(campaign.UploadLogo.FileName);
                    string extension = Path.GetExtension(campaign.UploadLogo.FileName);
                    fileName = fileName + extension;
                    campaign.LOGO = "/Assets/img/" + fileName;
                    campaign.UploadLogo.SaveAs(Path.Combine(Server.MapPath("/Assets/img/"), fileName));
                }
                if (campaign.COLORBG == campaign.COLORTEXT)
                {
                    campaign.COLORTEXT = null;
                    campaign.COLORBG = null;
                }
                if (campaign.COLORBTN_TEXT == campaign.COLORBTN_BG)
                {
                    campaign.COLORBTN_BG = null;
                    campaign.COLORBTN_TEXT = null;
                }
                campaign.STATUS = true;
                campaign.ID = (int)Session["IDCUS"];
                _db.Entry(campaign).State = EntityState.Modified;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id, CAMPAIGN campaign)
        {
            try
            {
                campaign = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
                _db.CAMPAIGNs.Remove(campaign);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
        public ActionResult DeleteJoiners(int id)
        {
            try
            {
                JOINER joiner = _db.JOINERs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
                var listJoiner = _db.JOINERs.Where(s => s.IDCAMPAIGN == id).ToList();
                foreach (var item in listJoiner)
                {
                    _db.JOINERs.Remove(item);

                }
                _db.SaveChanges();
                return RedirectToAction("Details/" + id);
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
        public ActionResult UpdateGiftActive(CAMPAIGN camp, int id)
        {

            camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();

            if (camp.STATUS == true)
            {
                camp.STATUS = false;
                _db.Entry(camp).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else if (camp.STATUS == false || camp.STATUS == null)
            {
                camp.STATUS = true;
                _db.Entry(camp).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return RedirectToAction("Index"); 
        }
        public ActionResult CreateDetail()
        {
            return PartialView();
        }
        public ActionResult AddGift(int idprize, int idgift, int limit)
        {

            idprize = Convert.ToInt32(Request.Form["idprize"]);
            idgift = Convert.ToInt32(Request.Form["idgift"]);

            var x = Session["CAMPAIGN"].ToString();
            int idCamp = Convert.ToInt32(x);
            PRIZE prize = _db.PRIZEs.Where(s => s.IDPRIZE == idprize).FirstOrDefault();
            GIFT gift = _db.GIFTs.Where(s => s.IDGIFT == idprize).FirstOrDefault();
            var campdetail = new CAMPAIGN_DETAIL()
            {
                IDCAMPAIGN = idCamp,
                IDPRIZE = idprize,
                IDGIFT = idgift,
                USAGELIMIT = limit
            };
            _db.CAMPAIGN_DETAIL.Add(campdetail);

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NestleCharity(int min, int max, string name, string id, string address)
        {

            var x = Session["CAMPAIGN"];

            var usersList = new List<JOINER>();
            //min = Convert.ToInt32(Request.Form["mincharity"]);
            //max = Convert.ToInt32(Request.Form["maxcharity"]);
            //name = Request.Form["namecharity"];
            //id = Request.Form["idcharity"];
            //address = Request.Form["addresscharity"];

            for (int i = min; i <= max; i++)
            {
                var user = new JOINER();

                user.IDCAMPAIGN = (int)x;
                user.NAME = name;
                user.ADDRESS = address;
                user.IDJOINER = id;
                user.PHONE = i.ToString();
                usersList.Add(user);
            }
            foreach (var item in usersList)
            {
                _db.JOINERs.Add(item);
            }
            _db.SaveChanges();

            return RedirectToAction("/Details/" + x);

        }

        public ActionResult SelectGift()
        {
            int id = (int)Session["IDCUS"];
            GIFT se_camp = new GIFT();
            se_camp.listGift = _db.GIFTs.Where(s => s.ID == id).ToList<GIFT>();
            return PartialView(se_camp);
        }
        public ActionResult SelectPrize()
        {
            PRIZE se_camp = new PRIZE();
            se_camp.listPrize = _db.PRIZEs.ToList<PRIZE>();
            return PartialView(se_camp);
        }

        //}
        public ActionResult DeleteGift(int id)
        {
            try
            {
                CAMPAIGN_DETAIL decamp = new CAMPAIGN_DETAIL();
                decamp = _db.CAMPAIGN_DETAIL.Where(s => s.IDDETAIL == id).FirstOrDefault();
                _db.CAMPAIGN_DETAIL.Remove(decamp);
                _db.SaveChanges();
                string view = "/Details/" + decamp.IDCAMPAIGN;
                return RedirectToAction(view);

            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }



        }
        public ActionResult listJoiners(int id)
        {
            
            id = (int)Session["IDCAMPAIGN"];
            JOINER joiners = new JOINER();
            return View(_db.JOINERs.Where(s => s.IDCAMPAIGN == id).ToList());
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            var x = Session["CAMPAIGN"];
            var usersList = new List<JOINER>();
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
                            var user = new JOINER();
                            if (workSheet.Cells[rowIterator, 1].Value == null)
                            { user.IDJOINER = null; }
                            else
                            { user.IDJOINER = workSheet.Cells[rowIterator, 1].Value.ToString(); }

                            if (workSheet.Cells[rowIterator, 2].Value == null)
                            { user.PHONE = null; }
                            else
                            {
                                user.PHONE = workSheet.Cells[rowIterator, 2].Value.ToString();
                            }
                            if (workSheet.Cells[rowIterator, 3].Value == null)
                            { user.NAME = null; }
                            else
                            {
                                user.NAME = workSheet.Cells[rowIterator, 3].Value.ToString();
                            }

                            if (workSheet.Cells[rowIterator, 4].Value == null)
                            {
                                user.DATEOFBIRTH = null;
                            }
                            else
                            {
                                user.DATEOFBIRTH = workSheet.Cells[rowIterator, 4].Value.ToString();
                            }
                            if (workSheet.Cells[rowIterator, 5].Value == null)
                            { user.EMAIL = null; }
                            else
                            { user.EMAIL = workSheet.Cells[rowIterator, 5].Value.ToString(); }
                            if (workSheet.Cells[rowIterator, 6].Value == null)
                            { user.ADDRESS = null; }
                            else { user.ADDRESS = workSheet.Cells[rowIterator, 6].Value.ToString(); }

                            user.IDCAMPAIGN = (int)x;
                            usersList.Add(user);
                        }
                    }
                }
            }
            using (_db)
            {
                foreach (var item in usersList)
                {
                    _db.JOINERs.Add(item);
                }
                _db.SaveChanges();

            }
            return RedirectToAction("/Details/" + x);
        }
        public ActionResult Contact()
        {
            return Content("Contact Preeco for use");
        }

    }
}

