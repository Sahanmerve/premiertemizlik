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
    public class ProjectsController : Controller
    {
        private CleanerContext db = new CleanerContext();

        // GET: Panel/Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.ParentCategory);
            return View(projects.ToList());
        }

        // GET: Panel/Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Panel/Projects/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Panel/Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,ShortDesc,LongDesc,ImageURL,CategoryID")] Project project, HttpPostedFileBase ProjectImage)
        {
            if (ModelState.IsValid)
            {
                if (ProjectImage != null)
                {
                    var path = Server.MapPath("/Uploads/Project/");
                    ProjectImage.SaveAs(path + ProjectImage.FileName);
                    project.ImageURL = ProjectImage.FileName;
                }
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", project.CategoryID);
            return View(project);
        }

        // GET: Panel/Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", project.CategoryID);
            return View(project);
        }

        // POST: Panel/Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,ShortDesc,LongDesc,ImageURL,CategoryID")] Project project, HttpPostedFileBase ProjectImage)
        {
            if (ModelState.IsValid)
            {
                var p = db.Projects.Find(project.ProjectID);
                if (ProjectImage != null)
                {
                    var path = Server.MapPath("/Uploads/Project/");

                    if (p.ImageURL != null)
                        System.IO.File.Delete(path + p.ImageURL);

                    //string old = path + ;
                    string _new = path + ProjectImage.FileName;
                    ProjectImage.SaveAs(_new);
                    p.ImageURL = ProjectImage.FileName;


                }
                p.LongDesc = project.LongDesc;
                p.CategoryID = project.CategoryID;
                p.ProjectName = project.ProjectName;
                p.ShortDesc = project.ShortDesc;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", project.CategoryID);
            return View(project);
        }

        // GET: Panel/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Panel/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
