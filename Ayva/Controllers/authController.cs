using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ayva.Models;

namespace Ayva.Controllers
{
    public class authController : Controller
    {
        private Db_ayvaEntities db = new Db_ayvaEntities();

        // GET: auth
        public ActionResult Index()
        {
            return View(db.Tbl_users.ToList());
        }
        static string sha256(string pass)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(pass));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        // GET: auth/Login
        public ActionResult Login()
        {
            
            return View();
        }

        // POST: auth/Login
        [HttpPost]
        public ActionResult Login(Tbl_users user)
        {
            if (ModelState.IsValid)
            {
                user.password = sha256(user.password);
                Tbl_users tmpUser = db.Tbl_users.Where(u=>u.email==user.email && u.password==user.password).FirstOrDefault();
                if (tmpUser!=null)
                {
                    FormsAuthentication.SetAuthCookie(tmpUser.email, false);
                    
                    //Roles.CreateRole(tmpUser.role);
                    //Roles.AddUserToRole(tmpUser.email, tmpUser.role);
                    return RedirectToAction("Index","Home");
                }
                ViewBag.err = "Mail veya Şifre Hatalı girildi.";
            }
            return View();
        }

        // GET: auth/LogOut
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        // GET: auth/LogOut

        public ActionResult SignUp()
        {

            return View();
        }

        // POST: auth/LogOut
        [HttpPost]
        public ActionResult SignUp(Tbl_users user)
        {
            if (ModelState.IsValid)
            {
                user.password = sha256(user.password);
                user.role = "user";
                Tbl_users tmpUser = db.Tbl_users.Where(u => u.email == user.email).FirstOrDefault();
                if (tmpUser == null)
                {

                    db.Tbl_users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                ViewBag.err = "Bu mail adresini kullanan bir hesap bulunmaktadır.";
            }
            return View();
        }

        // GET: auth/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_users tbl_users = db.Tbl_users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users);
        }

        // GET: auth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: auth/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,email,password,role")] Tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_users.Add(tbl_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_users);
        }

        // GET: auth/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_users tbl_users = db.Tbl_users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users);
        }

        // POST: auth/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,email,password,role")] Tbl_users tbl_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_users);
        }

        // GET: auth/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_users tbl_users = db.Tbl_users.Find(id);
            if (tbl_users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users);
        }

        // POST: auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_users tbl_users = db.Tbl_users.Find(id);
            db.Tbl_users.Remove(tbl_users);
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
