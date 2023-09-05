using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalDBContext db=new KurumsalDBContext();   
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
            return View(kimlik);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase file_LogoURL)
        {
            if ((ModelState.IsValid))
            {
                var k =db.Kimlik.Where(x=>x.KimlikId==id).SingleOrDefault();

                if(file_LogoURL!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(kimlik.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(kimlik.LogoURL));
                    }

                    WebImage img= new WebImage(file_LogoURL.InputStream);
                    FileInfo imgInfo = new FileInfo(file_LogoURL.FileName);

                    string logoName = file_LogoURL.FileName + imgInfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Kimlik/" + logoName);

                    k.LogoURL = "/Uploads/Kimlik/" + logoName;
                }
                k.Title= kimlik.Title;
                k.Keywords= kimlik.Keywords;
                k.Description= kimlik.Description;
                k.Unvan= kimlik.Unvan;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

             
            return View(kimlik);
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}