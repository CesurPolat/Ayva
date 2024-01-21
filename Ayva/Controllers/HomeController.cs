using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayva.Models;

namespace Ayva.Controllers
{
    public class HomeController : Controller
    {
        Db_ayvaEntities db = new Db_ayvaEntities();
        public ActionResult Index()
        {
            List<Tbl_slider> slider = db.Tbl_slider.OrderBy(c=>c.sliderOrder).ToList();
            return View(slider);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}