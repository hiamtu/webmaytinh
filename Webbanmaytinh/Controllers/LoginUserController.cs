using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webbanmaytinh.Models;


namespace Webbanmaytinh.Controllers
{
    public class LoginUserController : Controller
    {
        DBSportStoreEntities database = new DBSportStoreEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(AdminUser _user)
        {
            var check = database.AdminUsers.
                Where(s => s.NameUser == _user.NameUser && s.PasswordUser==_user.PasswordUser).FirstOrDefault();

            if (check == null)//sai thong tin
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index", _user);
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.NameUser;
                return RedirectToAction("Index", "Product");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_id = database.AdminUsers.FirstOrDefault(s => s.ID == _user.ID);
                if (check_id == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.AdminUsers.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Index","LoginUser");
                }
                else
                {
                    ViewBag.ErrorRegister = "ID already exists";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }

    }
}
