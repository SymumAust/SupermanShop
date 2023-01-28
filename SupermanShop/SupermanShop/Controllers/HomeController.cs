using SupermanShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SupermanShop.Controllers
{
    public class HomeController : Controller
    {

        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBLUserInfoes.ToList());
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(TBLUserInfo tBLUserInfo)
        {
            if(db.TBLUserInfoes.Any(X=>X.UsernameUs == tBLUserInfo.UsernameUs))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }

            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();


                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UsernameSS"] = tBLUserInfo.UsernameUs.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(TBLUserInfo tBLUserInfo)
        {
            var checkLogin = db.TBLUserInfoes.Where(x => x.UsernameUs.Equals(tBLUserInfo.UsernameUs) && x.PasswordUs.Equals(tBLUserInfo.PasswordUs)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UsernameSS"] = tBLUserInfo.UsernameUs.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }
    }
}