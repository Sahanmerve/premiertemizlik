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
    public class ReferansController : Controller
    {
        private CleanerContext db = new CleanerContext();

        // GET: Panel/Referans
        public ActionResult Index()
        {
            return View(db.Referanslar.ToList());
        }

        // GET: Panel/Referans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referans referans = db.Referanslar.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            return View(referans);
        }

        // GET: Panel/Referans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Referans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReferansID,FirmaAd,ReferansURL,ReferansImageUrl")] Referans referans, HttpPostedFileBase ReferansImage)
        {
            if (ModelState.IsValid)
            {
                if (ReferansImage != null)
                {
                    var path = Server.MapPath("/Uploads/Referans/");
                    ReferansImage.SaveAs(path + ReferansImage.FileName);
                    referans.ReferansImageUrl = ReferansImage.FileName;
                }
                db.Referanslar.Add(referans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(referans);
        }

        // GET: Panel/Referans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referans referans = db.Referanslar.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            return View(referans);
        }

        // POST: Panel/Referans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReferansID,FirmaAd,ReferansURL,ReferansImageUrl")] Referans referans, HttpPostedFileBase ReferansImage)
        {
            if (ModelState.IsValid)
            {
                var p = db.Referanslar.Find(referans.ReferansID);
                if (ReferansImage != null)
                {
                    var path = Server.MapPath("/Uploads/Referans/");

                    if (p.ReferansImageUrl != null)
                        System.IO.File.Delete(path + p.ReferansImageUrl);

                    //string old = path + ;
                    string _new = path + ReferansImage.FileName;
                    ReferansImage.SaveAs(_new);
                    p.ReferansImageUrl = ReferansImage.FileName;
                }
                p.FirmaAd = referans.FirmaAd;
                p.ReferansURL = referans.ReferansURL;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(referans);
        }

        // GET: Panel/Referans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referans referans = db.Referanslar.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            return View(referans);
        }

        // POST: Panel/Referans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referans referans = db.Referanslar.Find(id);
            db.Referanslar.Remove(referans);
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
