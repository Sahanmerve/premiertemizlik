using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CleanerMVC.Models;

namespace CleanerMVC.Areas.Panel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServicesController : Controller
    {
        private CleanerContext db = new CleanerContext();

        // GET: Panel/Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: Panel/Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Panel/Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceID,ServiceName,Desc,ImageURL")] Service service, HttpPostedFileBase ServiceImage)
        {
            if (ModelState.IsValid)
            {
                if (ServiceImage != null)
                {
                    var path = Server.MapPath("/Uploads/Service/");
                    ServiceImage.SaveAs(path + ServiceImage.FileName);
                    service.ImageURL = ServiceImage.FileName;
                }
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Panel/Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Panel/Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceID,ServiceName,Desc,ImageURL")] Service service, HttpPostedFileBase ServiceImage)
        {
            if (ModelState.IsValid)
            {
                var s = db.Services.Find(service.ServiceID);
                if (ServiceImage != null)
                {
                    var path = Server.MapPath("/Uploads/Service/");

                    if (s.ImageURL != null)
                        System.IO.File.Delete(path + s.ImageURL);

                    //string old = path + ;
                    string _new = path + ServiceImage.FileName;
                    ServiceImage.SaveAs(_new);
                    s.ImageURL = ServiceImage.FileName;


                }
                s.ServiceName = service.ServiceName;
                s.Desc = service.Desc;
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Panel/Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Panel/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
