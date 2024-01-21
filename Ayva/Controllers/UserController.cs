using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Ayva.Models;

namespace Ayva.Controllers
{
    public class UserController : Controller
    {
        private Db_ayvaEntities db = new Db_ayvaEntities();

        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<uretimSon> result = (from ayvaUretim in db.Tbl_ayvaUretim
                         join iller in db.Tbl_iller on ayvaUretim.ilID equals iller.Id
                         where ayvaUretim.uYili >= 2021 && ayvaUretim.uYili <= 2023
                         group ayvaUretim by iller.ilAdi into groupedData
                         select new uretimSon
                         {
                             il = groupedData.Key,
                             miktar1 = groupedData.Sum(x => x.uYili == 2021 ? x.uretimMiktari : 0),
                             miktar2 = groupedData.Sum(x => x.uYili == 2022 ? x.uretimMiktari : 0),
                             miktar3 = groupedData.Sum(x => x.uYili == 2023 ? x.uretimMiktari : 0)
                         });
            return View(result.ToList());
        }


        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_ayvaUretim tbl_ayvaUretim = db.Tbl_ayvaUretim.Find(id);
            if (tbl_ayvaUretim == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ayvaUretim);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,uYili,uretimMiktari,ilID")] Tbl_ayvaUretim tbl_ayvaUretim)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_ayvaUretim.Add(tbl_ayvaUretim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_ayvaUretim);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_ayvaUretim tbl_ayvaUretim = db.Tbl_ayvaUretim.Find(id);
            if (tbl_ayvaUretim == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ayvaUretim);
        }

        // POST: User/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,uYili,uretimMiktari,ilID")] Tbl_ayvaUretim tbl_ayvaUretim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_ayvaUretim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_ayvaUretim);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_ayvaUretim tbl_ayvaUretim = db.Tbl_ayvaUretim.Find(id);
            if (tbl_ayvaUretim == null)
            {
                return HttpNotFound();
            }
            return View(tbl_ayvaUretim);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_ayvaUretim tbl_ayvaUretim = db.Tbl_ayvaUretim.Find(id);
            db.Tbl_ayvaUretim.Remove(tbl_ayvaUretim);
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
