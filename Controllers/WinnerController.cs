using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PreecoLuckyDraw.Models;
using System.Data.SqlClient;

namespace PreecoLuckyDraw.Controllers
{
    public class WinnerController : Controller
    {
        DbPreecoLuckyDrawEntities _db = new DbPreecoLuckyDrawEntities();

        // GET: Winner
        public ActionResult Index()
        {
            var i = Session["IDCUS"];
            if (Session["PHONE"] != null)
            {
                if ((int)Session["STATUS"] == 1)
                {
                    return View(_db.WINNERs.OrderBy(s => s.CAMPAIGN_DETAIL.CAMPAIGN.NAME).ToList());

                }
                var db=_db.WINNERs.Where(s => s.CAMPAIGN_DETAIL.CAMPAIGN.ID==(int)i);
                return View(db.OrderBy(s => s.CAMPAIGN_DETAIL.CAMPAIGN.NAME).ToList());
            }
            return RedirectToAction("Login", "Login");
             
        }
        public ActionResult WinCampaign(int id)
        {
            //if (Session["PHONE"] != null)
            //{
            return View(_db.WINNERs.Where(s => s.CAMPAIGN_DETAIL.CAMPAIGN.IDCAMPAIGN==id).ToList());
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Login");
            //}
        }
        public ActionResult DeleteListWinners()
        {
            var i = Session["IDCUS"];

            try
            {
                var listJoiners = _db.WINNERs.Where(s => s.CAMPAIGN_DETAIL.CAMPAIGN.ID == (int)i).ToList();

                foreach (var item in listJoiners)
                {
                    _db.WINNERs.Remove(item);

                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
        public ActionResult Delete(string id, WINNER win)
        {
           
            try
            {
                // TODO: Add delete logic here
                win = _db.WINNERs.Where(s => s.PHONE == id).FirstOrDefault();
                var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == win.CAMPAIGN_DETAIL.IDCAMPAIGN).FirstOrDefault();
                camp.WINNERS -= 1;
                _db.WINNERs.Remove(win);
                
                _db.SaveChanges();
                return RedirectToAction("Testing");

            }
            catch
            {
                return Content("This data using other table, Error Delete");
            }
        }
        public void UpdateWinner(List<WINNER> winners)
        {
            //var temp = winners;
            if (winners != null)
            {
                foreach (WINNER winner in winners)
                {



                    WINNER dto_winner = new WINNER();
                    dto_winner.IDDETAIL = winner.IDDETAIL;
                    dto_winner.NAME = winner.NAME;
                    dto_winner.PHONE = winner.PHONE;
                    dto_winner.WINDATE = winner.WINDATE;
                    _db.WINNERs.Add(dto_winner);

                    var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == winner.IDCAMP).FirstOrDefault();
                    camp.WINNERS += 1;


                    //var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == idcamp).FirstOrDefault();

                    var decamp = _db.CAMPAIGN_DETAIL.Where(s => s.IDDETAIL == dto_winner.IDDETAIL).FirstOrDefault();
                    decamp.USAGELIMIT -= 1;
                    _db.SaveChanges();
                }
            }
        }
        //public ActionResult UpdateWinner(List<WINNER> winners)
        //{ 
        //    //var temp = winners;
        //    if (winners != null)
        //    {
        //        foreach (WINNER winner in winners)
        //        {



        //            WINNER dto_winner = new WINNER();
        //            dto_winner.IDDETAIL = winner.IDDETAIL;
        //            dto_winner.NAME = winner.NAME;
        //            dto_winner.PHONE = winner.PHONE;
        //            dto_winner.WINDATE = winner.WINDATE;
        //            _db.WINNERs.Add(dto_winner);

        //                var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == winner.IDCAMP).FirstOrDefault();
        //                camp.WINNERS += 1;


        //            //var camp = _db.CAMPAIGNs.Where(s => s.IDCAMPAIGN == idcamp).FirstOrDefault();

        //            var decamp = _db.CAMPAIGN_DETAIL.Where(s => s.IDDETAIL == dto_winner.IDDETAIL).FirstOrDefault();
        //            decamp.USAGELIMIT -= 1;
        //            _db.SaveChanges();
        //        }
        //    }

        //    return RedirectToAction("Index","Campaign");
        //}
        [HttpGet]
        public JsonResult listWinners()
        {

            //var id = (int)Session["IDCAMPAIGN"];
            var joiners = _db.WINNERs.ToList();
            IList<WINNER> dataList = new List<WINNER>();
            foreach (var item in joiners)
            {
                var joiner = new WINNER();
                joiner.ID = item.ID;
                joiner.IDDETAIL = item.IDDETAIL;
                joiner.PHONE = item.PHONE;
                dataList.Add(joiner);
            }
            _db.SaveChanges();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Testing()
        {
            return View();
        }
        public ActionResult Create(WINNER campaign)
        {
            int x=20;
            try
            {for(int i = 0; i <= x; i++) { 
                campaign.PHONE = i.ToString();
                campaign.IDDETAIL = 5;
                _db.WINNERs.Add(campaign);
                _db.SaveChanges();

                }
                return RedirectToAction("Testing");
            }
            catch
            {
                return View();
            }

        }
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        WINNER campaign = _db.WINNERs.Where(s => s.PHONE == id.ToString()).FirstOrDefault();
        //        _db.WINNERs.Remove(campaign);
        //        _db.SaveChanges();
        //        return View("Testing");
        //    }
        //    catch
        //    {
        //        return Content("This data using other table, Error Delete");
        //    }
        //}

    }
}