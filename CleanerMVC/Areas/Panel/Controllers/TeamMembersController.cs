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
    public class TeamMembersController : Controller
    {
        private CleanerContext db = new CleanerContext();

        // GET: Panel/TeamMembers
        public ActionResult Index()
        {
            return View(db.TeamMembers.ToList());
        }

        // GET: Panel/TeamMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // GET: Panel/TeamMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/TeamMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamMemberID,ImageURL,NameSurname,Title,SMLinks")] TeamMember teamMember,HttpPostedFileBase TeamImage)
        {
            if (ModelState.IsValid)
            {
                if (TeamImage != null)
                {
                    var path = Server.MapPath("/Uploads/TeamMember/");
                    TeamImage.SaveAs(path + TeamImage.FileName);
                    teamMember.ImageURL = TeamImage.FileName;
                }
                db.TeamMembers.Add(teamMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teamMember);
        }

        // GET: Panel/TeamMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // POST: Panel/TeamMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamMemberID,ImageURL,NameSurname,Title,SMLinks")] TeamMember teamMember, HttpPostedFileBase TeamImage)
        {
            if (ModelState.IsValid)
            {
                var t = db.TeamMembers.Find(teamMember.TeamMemberID);
                if (TeamImage != null)
                {
                    var path = Server.MapPath("/Uploads/TeamMember/");

                    if (t.ImageURL != null)
                        System.IO.File.Delete(path + t.ImageURL);                   
                    string _new = path + TeamImage.FileName;
                    TeamImage.SaveAs(_new);
                    t.ImageURL = TeamImage.FileName;
                }
                t.NameSurname = teamMember.NameSurname;
                t.Title = teamMember.Title;
                t.SMLinks = teamMember.SMLinks;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamMember);
        }

        // GET: Panel/TeamMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // POST: Panel/TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMember teamMember = db.TeamMembers.Find(id);
            db.TeamMembers.Remove(teamMember);
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
