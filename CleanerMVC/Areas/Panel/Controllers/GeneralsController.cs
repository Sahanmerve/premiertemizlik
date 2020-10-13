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
    public class GeneralsController : Controller
    {
        private CleanerContext db = new CleanerContext();

        // GET: Panel/Generals
        public ActionResult Index()
        {
            return View(db.GeneralSettings.ToList());
        }

        // GET: Panel/Generals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General general = db.GeneralSettings.Find(id);
            if (general == null)
            {
                return HttpNotFound();
            }
            return View(general);
        }

        // GET: Panel/Generals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Generals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GeneralID,LogoURL,HeaderTitle,HeaderSubtitle,BennarTitle,BennarSubtitle")] General general, HttpPostedFileBase GeneralImage)
        {
            if (ModelState.IsValid)
            {
                if (GeneralImage != null)
                {
                    var path = Server.MapPath("/Uploads/General/");
                    GeneralImage.SaveAs(path + GeneralImage.FileName);
                    general.LogoURL = GeneralImage.FileName;
                }
                db.GeneralSettings.Add(general);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(general);
        }

        // GET: Panel/Generals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General general = db.GeneralSettings.Find(id);
            if (general == null)
            {
                return HttpNotFound();
            }
            return View(general);
        }

        // POST: Panel/Generals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GeneralID,LogoURL,HeaderTitle,HeaderSubtitle,BennarTitle,BennarSubtitle")] General general, HttpPostedFileBase GeneralImage)
        {
            if (ModelState.IsValid)
            {
                var t = db.GeneralSettings.Find(general.GeneralID);
                if (GeneralImage != null)
                {
                    var path = Server.MapPath("/Uploads/General/");

                    if (t.LogoURL != null)
                        System.IO.File.Delete(path + t.LogoURL);
                    string _new = path + GeneralImage.FileName;
                    GeneralImage.SaveAs(_new);
                    t.LogoURL = GeneralImage.FileName;
                }
                t.HeaderTitle = general.HeaderTitle;
                t.HeaderSubtitle = general.HeaderSubtitle;
                t.BennarTitle = general.BennarTitle;
                t.BennarSubtitle = general.BennarSubtitle;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(general);
        }

        // GET: Panel/Generals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            General general = db.GeneralSettings.Find(id);
            if (general == null)
            {
                return HttpNotFound();
            }
            return View(general);
        }

        // POST: Panel/Generals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            General general = db.GeneralSettings.Find(id);
            db.GeneralSettings.Remove(general);
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
