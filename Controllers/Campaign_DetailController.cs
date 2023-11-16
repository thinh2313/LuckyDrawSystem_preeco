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
    public class Campaign_DetailController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

   
        public ActionResult Create(CAMPAIGN_DETAIL cd)
        {
            _db.CAMPAIGN_DETAIL.Add(cd);
            _db.SaveChanges();
            return RedirectToAction("Index", "Campaign");

        }
       
        public ActionResult SelectCampaign()
        {
            CAMPAIGN se_camp = new CAMPAIGN();
            se_camp.listCampaign = _db.CAMPAIGNs.ToList<CAMPAIGN>();
            return PartialView(se_camp);
        }
        public ActionResult SelectPrizeCamp(int id)
        {   
            id = (int) Session["IDCAMPAIGN"];
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id ).FirstOrDefault();
            
            //var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();

            se_camp.listCampDetail = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            return PartialView(se_camp);

        }
        public ActionResult RunProgram(int id)
        {
            //if (Session["PHONE"] != null)
            //{ 
                Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault(); 
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;

            return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult RunProgram3(int id)
        {
            //if (Session["PHONE"] != null)
            //{
                Session["check"] = 0;
                var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
                var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
                var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
                Session["check"] = se_camp;
                Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
                Session["NAME"] = camp.NAME;
                Session["BACKGROUND"] = camp.BACKGROUND;
                Session["COLORBG"] = camp.COLORBG;
                Session["COLORTEXT"] = camp.COLORTEXT;
                Session["COLORBTN_BG"] = camp.COLORBTN_BG;
                Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
                Session["LOGO"] = camp.LOGO;

                return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult RunProgram3HadStop(int id)
        {
            //if (Session["PHONE"] != null)
            //{
                Session["check"] = 0;
                var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
                var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
                var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
                Session["check"] = se_camp;
                Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
                Session["NAME"] = camp.NAME;
                Session["BACKGROUND"] = camp.BACKGROUND;
                Session["COLORBG"] = camp.COLORBG;
                Session["COLORTEXT"] = camp.COLORTEXT;
                Session["COLORBTN_BG"] = camp.COLORBTN_BG;
                Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
                Session["LOGO"] = camp.LOGO;

                return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult RunProgram3HadStop_min_max(int id)
        {
            //if (Session["PHONE"] != null)
            //{
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;

            return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult RunProgram4HadStop(int id)
        {
            //if (Session["PHONE"] != null)
            //{
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;
            return View(list_camp);
        }
        public ActionResult RunProgram4Charity(int id)
        {
            //if (Session["PHONE"] != null)
            //{
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;
            return View(list_camp);
        }
        public ActionResult RunProgramHadStop(int id)
        {
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;

            return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult ProgramVIFON(int id)
        {
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;

            return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult RunProgram8HadStop(int id)
        {
            //if (Session["PHONE"] != null)
            //{
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;

            return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        public ActionResult RunProgramByName(int id)
        {
           
            //if (Session["PHONE"] != null)
            //{
            Session["check"] = 0;
            var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
            var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
            var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
            Session["check"] = se_camp;
            Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
            Session["NAME"] = camp.NAME;
            Session["BACKGROUND"] = camp.BACKGROUND;
            Session["COLORBG"] = camp.COLORBG;
            Session["COLORTEXT"] = camp.COLORTEXT;
            Session["COLORBTN_BG"] = camp.COLORBTN_BG;
            Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
            Session["LOGO"] = camp.LOGO;
            return View(list_camp);
            //}
            //return RedirectToAction("Login", "Login");

        }
        //public ActionResult RunProgramHadStop(int id)
        //{
        //    //if (Session["PHONE"] != null)
        //    //{
        //    //    if ((int)Session["STATUS"] == 1 || (int)Session["STATUS"] == 2313|| (int)Session["STATUS"] == 2)
        //    //    {


        //            Session["check"] = 0;
        //    var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == id).FirstOrDefault();
        //    var se_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).FirstOrDefault();
        //    var list_camp = _db.CAMPAIGN_DETAIL.Where(s => s.CAMPAIGN.IDCAMPAIGN == id).ToList();
        //    Session["check"] = se_camp;
        //    Session["IDCAMPAIGN"] = camp.IDCAMPAIGN;
        //    Session["NAME"] = camp.NAME;
        //    Session["BACKGROUND"] = camp.BACKGROUND;
        //    Session["COLORBG"] = camp.COLORBG;
        //    Session["COLORTEXT"] = camp.COLORTEXT;
        //    Session["COLORBTN_BG"] = camp.COLORBTN_BG;
        //    Session["COLORBTN_TEXT"] = camp.COLORBTN_TEXT;
        //    Session["LOGO"] = camp.LOGO;
        //    return View(list_camp);
        //        }


        //     ;   return Content("Liên hệ PreecoStudio để được tư vấn về dịch vụ");
        //    }
        //    return RedirectToAction("Login", "Login");
        //}
        public ActionResult Detail(int id)
        {
            return PartialView(_db.CAMPAIGN_DETAIL.Where(s => s.IDDETAIL == id).FirstOrDefault());
        }
        public ActionResult ViewDefaultSelect()
        {
            return View();
        }
    }
}