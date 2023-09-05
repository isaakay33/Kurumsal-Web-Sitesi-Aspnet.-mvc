using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        private  KurumsalDBContext db = new KurumsalDBContext();
        // GET: Blog

        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var b = (db.Blog.Include("Kategori").ToList().OrderByDescending(x => x.BlogId));
            return View(b);
        }
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriID","Kategori_ad"); //veri taşıma işlemi
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog,HttpPostedFileBase ResimURL) 
        {
            if (ResimURL != null)
            {
                

                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                string blogimgname = Guid.NewGuid().ToString() + imgInfo.Extension;
                img.Resize(300, 200);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ResimURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b=db.Blog.Where(x=>x.BlogId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "Kategori_ad", b.KategoriId);
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit (int? id,Blog blog,HttpPostedFileBase resim_dosyasi) 
        { 
            if(ModelState.IsValid) //model isvalid olmuşsa doğrulama işlemi başarılı ise,
            {
                var b=db.Blog.Where(x=>x.BlogId==id).SingleOrDefault();

                if (resim_dosyasi != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))/// eski resim varmı 
                    {
                        System.IO.File.Delete(Server.MapPath(b.ResimURL)); // eski resmi sil
                    }

                    WebImage img = new WebImage(resim_dosyasi.InputStream);
                    FileInfo imgInfo = new FileInfo(resim_dosyasi.FileName);

                    string blogimgname = resim_dosyasi.FileName + imgInfo.Extension;
                    img.Resize(200, 200);
                    img.Save("~/Uploads/Blog/" + blogimgname);

                    b.ResimURL = "/Uploads/Blog/" + blogimgname;
                }
                b.Baslik=blog.Baslik;
                b.Icerik=blog.Icerik;
                b.KategoriId=blog.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        return View(blog);
        }
        public ActionResult Delete(int id)
        {
            var b=db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ResimURL)))/// eski resim varmı 
            {
                System.IO.File.Delete(Server.MapPath(b.ResimURL)); // eski resmi sil
            }
            db.Blog.Remove(b);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}