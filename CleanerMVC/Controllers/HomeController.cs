using CleanerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CleanerMVC.Controllers
{
    public class HomeController : Controller
    {
        CleanerContext db = new CleanerContext();
        WorksViewModel data = new WorksViewModel();

        [HttpGet]
        public ActionResult Index()
        {

            
                ViewBag.SiteTitle = "Premier Temizlik ve Organizasyon";
                ViewBag.GeneralSettings = db.GeneralSettings.First();
                return View();

        }
        [HttpPost]
        public ActionResult Index(string name, string phone, string email, string comments)
        {
           
                ViewBag.SiteTitle = "Premier Temizlik ve Organizasyon";
                ViewBag.GeneralSettings = db.GeneralSettings.First();

                if (ModelState.IsValid)
                {
                    Contact con = new Contact();
                    con.CustomerName = name;
                    con.CustomerPhone = phone;
                    con.CustomerEmail = email;
                    con.CustomerDesc = comments;
                    db.Contacts.Add(con);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View();
           
      
        }
        public ActionResult _Works()
        {
            data.Categories = db.Categories.ToList();
            data.Projects = db.Projects.ToList();
            return View(data);
        }
        public ActionResult _Services()
        {      
            data.Services = db.Services.ToList();
            return View(data);
        }
        public ActionResult _Team()
        {            
            data.TeamMembers = db.TeamMembers.ToList();
            return View(data);
        }
        public ActionResult _General()
        {
            
            return View(db.Referanslar.ToList());
        }
        public ActionResult _Login()
        {          
            return View();
        }
    }
}