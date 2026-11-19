using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SchoolManagementMvc.Models;

namespace SchoolManagementMvc.Controllers
{
    public abstract class CrudController<TEntity> : SecureController where TEntity : class, new()
    {
        protected readonly SchoolDbContext Db = new SchoolDbContext();
        protected abstract DbSet<TEntity> Set { get; }
        protected abstract int IdOf(TEntity item);
        protected virtual IQueryable<TEntity> Query() { return Set; }
        protected string EntityName => typeof(TEntity).Name;

        public ActionResult Index() { ViewBag.EntityName = EntityName; return View("~/Views/Shared/CrudIndex.cshtml", Query().AsNoTracking().ToList()); }
        public ActionResult Details(int? id) { if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); var item = Set.Find(id); if (item == null) return HttpNotFound(); ViewBag.EntityName = EntityName; return View("~/Views/Shared/CrudDetails.cshtml", item); }
        public ActionResult Create() { ViewBag.EntityName = EntityName; ViewBag.IsCreate = true; return View("~/Views/Shared/CrudEdit.cshtml", new TEntity()); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(TEntity item) { if (!ModelState.IsValid) { ViewBag.EntityName = EntityName; ViewBag.IsCreate = true; return View("~/Views/Shared/CrudEdit.cshtml", item); } Set.Add(item); Db.SaveChanges(); return RedirectToAction("Index"); }
        public ActionResult Edit(int? id) { if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); var item = Set.Find(id); if (item == null) return HttpNotFound(); ViewBag.EntityName = EntityName; return View("~/Views/Shared/CrudEdit.cshtml", item); }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(TEntity item) { if (!ModelState.IsValid) { ViewBag.EntityName = EntityName; return View("~/Views/Shared/CrudEdit.cshtml", item); } Db.Entry(item).State = EntityState.Modified; Db.SaveChanges(); return RedirectToAction("Index"); }
        public ActionResult Delete(int? id) { if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); var item = Set.Find(id); if (item == null) return HttpNotFound(); ViewBag.EntityName = EntityName; return View("~/Views/Shared/CrudDelete.cshtml", item); }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) { var item = Set.Find(id); if (item != null) { Set.Remove(item); Db.SaveChanges(); } return RedirectToAction("Index"); }
        protected override void Dispose(bool disposing) { if (disposing) Db.Dispose(); base.Dispose(disposing); }
    }
}
