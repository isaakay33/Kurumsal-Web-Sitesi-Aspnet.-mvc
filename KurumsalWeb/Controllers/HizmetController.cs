using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        public DbSet<Hizmet> Remove { get; private set; }

        // GET: Hizmet
        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase file_LogoURL)
        {
            if (ModelState.IsValid)
            {
                if (file_LogoURL != null)
                {
                    // Bu çok önemli bir değişiklik!

                    WebImage img = new WebImage(file_LogoURL.InputStream);
                    FileInfo imgInfo = new FileInfo(file_LogoURL.FileName);

                    string logoName = file_LogoURL.FileName + imgInfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Hizmet/" + logoName);

                    hizmet.ResimUrl = "/Uploads/Hizmet/" + logoName;
                }
                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }
        public ActionResult Edit(int? id)
        {
            if (id== null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var hizmet=db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();

            }
            return View(hizmet) ;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id,Hizmet hizmet,HttpPostedFileBase ResimURL) 
        {
            
            if(ModelState.IsValid)
            {
                var h = db.Hizmet.Where(x => x.HizmetId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(h.ResimUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(h.ResimUrl));
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoName = ResimURL.FileName + imgInfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Kimlik/" + logoName);

                    h.ResimUrl = "/Uploads/Kimlik/" + logoName;
                }
                h.Baslik = hizmet.Baslik;
                h.Aciklama = hizmet.Aciklama;
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hizmet);
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var h=db.Hizmet.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            db.Hizmet.Remove(h);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
         
        }
    }
}