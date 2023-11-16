using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreecoLuckyDraw.Models;
using EntityState = System.Data.Entity.EntityState;

namespace PreecoLuckyDraw.Controllers
{
  
        public class LoginController : Controller
        {
            DbPreecoLuckyDrawEntities database = new DbPreecoLuckyDrawEntities();

            // GET: Login
            public ActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public ActionResult LoginAccount(USER _user)
            {
            var checkPassw = "null";
            var loginUser = database.USERS.Where(s => s.PHONE == _user.PHONE && s.PASSWORD == _user.PASSWORD).FirstOrDefault();
            var checkUser = database.USERS.Where(s => s.PHONE == _user.PHONE).FirstOrDefault();
            if (checkUser == null)
            {
                ViewBag.ErrorInfo = "Sai tên đăng nhập";
                return View("Login");
            }
             checkPassw = checkUser.PASSWORD;
            if (checkUser.PHONE != _user.PHONE)
            {
                ViewBag.ErrorInfo = "Sai tên đăng nhập";
                return View("Login");

            }
            else if (_user.PASSWORD != checkPassw)
            {
                ViewBag.ErrorPass = "Sai mật khẩu";
                return View("Login");
            }
            database.Configuration.ValidateOnSaveEnabled = false;
                    var i = loginUser.STATUSS;
                    Session["PHONE"] = loginUser.PHONE;
                    Session["NAMECUS"] = loginUser.NAME;
                    Session["STATUS"] = loginUser.STATUSS;
                    Session["PASSWORD"] = loginUser.PASSWORD;
                    Session["IDCUS"] = loginUser.ID;
            if ((string)Session["NAMECUS"] == "storyLog")
            {
                return View("Log");
            }
                    return RedirectToAction("Index", "Home");
                //return View();
            }
            public ActionResult RegisterUser()
            {
                return View();
            }

            [HttpPost]
            public ActionResult RegisterUser(USER _user)
            {
            if (ModelState.IsValid)
            { }
            //_user.STATUS = 3;

            var check_ID = database.USERS.Where(s => s.PHONE == _user.PHONE).FirstOrDefault();
            if (check_ID == null && _user.PHONE == null)
            {
                return View();
            }
                 if (check_ID == null)
            {
                    if (_user.PHONE.Length>10|| _user.PHONE.Length <10)
                    {
                        ViewBag.ErrorPhone = "Nhập số điện thoại";
                        return View();
                    }
                _user.STATUSS = 3;
                database.Configuration.ValidateOnSaveEnabled = false;
                database.USERS.Add(_user);
                database.SaveChanges();
                return RedirectToAction("Login");
            }
            if (_user.PHONE == check_ID.PHONE)
            {
                ViewBag.ErrorRegister = "This ID is exist";
                return View();
            }


            return View();
            }
            public ActionResult EditAccount(int ID)
            {
                var detailUser = database.USERS.Where(m => m.ID == ID).FirstOrDefault();
                return View(detailUser);
            }

        [HttpPost]
        public ActionResult EditAccount(USER USER)
        {
            var detail = database.USERS.Where(m => m.ID == USER.ID);

            if (detail == null)
            {
                return HttpNotFound();
            }

            try
            {
                database.Entry(USER).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }
        }
            public bool CheckExistAccount(USER cus)
            {
                var check = database.USERS.Where(s => s.PHONE == cus.PHONE && s.PASSWORD == cus.PASSWORD).FirstOrDefault();
                if (check != null)
                {
                    return true;
                }

                return false;
            }
            public ActionResult Logout()
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            // Start OTP Generation function
            protected string Generate_otp()
            {
                char[] charArr = "0123456789".ToCharArray();
                string strrandom = string.Empty;
                Random objran = new Random();
                for (int i = 0; i < 4; i++)
                {
                    //It will not allow Repetation of Characters
                    int pos = objran.Next(1, charArr.Length);
                    if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                    else i--;
                }
                return strrandom;
            }
            // End OTP Generation function
            // Start Send OTP code on button click

            // End Get Response function
        }
    }
