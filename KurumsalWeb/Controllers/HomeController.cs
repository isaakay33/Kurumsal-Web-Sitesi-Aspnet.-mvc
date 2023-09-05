using KurumsalWeb.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    
    public class HomeController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

           

            return View();
        }
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderId));
        }
        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        public ActionResult Hizmetlerimiz()
        {
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "akayisa955@gmail.com";
                WebMail.Password = "9059i.a33a";
                WebMail.SmtpPort = 587;
                WebMail.Send("akayisa955@gmail.com", konu, email + "</br>" + mesaj);
                ViewBag.Uyari = "Mesajınız Başarıyla Gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu.Tekrar deneyiniz.";
            }
            return View();
        }

       

        public ActionResult Iletisim()
        {
            //if (adsoyad != null && email != null)
            //{
            //    WebMail.SmtpServer = "smtp.gmail.com";
            //    WebMail.EnableSsl = true;
            //    WebMail.UserName = "akayisa955@gmail.com";
            //    WebMail.Password = "9059i.a33a";
            //    WebMail.SmtpPort = 587;
            //    WebMail.Send("akayisa955@gmail.com", konu, email + "</br>" + mesaj);
            //    ViewBag.Uyari = "Mesajınız Başarıyla Gönderilmiştir.";
            //}
            //else
            //{
            //    ViewBag.Uyari = "Hata oluştu.Tekrar deneyiniz.";
            //}
            return View();
        }
        public ActionResult Blog(int sayfa=1)
        {
            return View(db.Blog.Include("Kategori").OrderByDescending(x=>x.BlogId).ToPagedList(sayfa,5));
        }

        public ActionResult BlogDetay(int id)
        {
            var b = db.Blog.Include("Kategori").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }

        public JsonResult YorumYap(string adsoyad, string eposta,string icerik,int blogid)
        {
            if (icerik==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId =blogid ,Onay=false});
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Blog.Include("Kategori").ToList().OrderBy(x=>x.Kategori.Kategori_ad));
        }

        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x=>x.BlogId));
        }

        public ActionResult Hakkimizda()
        {
           
            return View(db.Hakkimizda.SingleOrDefault());
        }
       public ActionResult FooterPartial()
        {
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);

            return PartialView();
        }
    }
}