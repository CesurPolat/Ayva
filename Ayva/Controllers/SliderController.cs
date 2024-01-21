using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ayva.Models;

namespace Ayva.Controllers
{
    public class SliderController : Controller
    {
        private Db_ayvaEntities db = new Db_ayvaEntities();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Tbl_slider.ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_slider tbl_slider = db.Tbl_slider.Find(id);
            if (tbl_slider == null)
            {
                return HttpNotFound();
            }
            return View(tbl_slider);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,pictureUrl,sliderOrder")] Tbl_slider tbl_slider)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_slider.Add(tbl_slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_slider tbl_slider = db.Tbl_slider.Find(id);
            if (tbl_slider == null)
            {
                return HttpNotFound();
            }
            return View(tbl_slider);
        }

        // POST: Slider/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,pictureUrl,sliderOrder")] Tbl_slider tbl_slider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_slider tbl_slider = db.Tbl_slider.Find(id);
            if (tbl_slider == null)
            {
                return HttpNotFound();
            }
            return View(tbl_slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_slider tbl_slider = db.Tbl_slider.Find(id);
            db.Tbl_slider.Remove(tbl_slider);
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
