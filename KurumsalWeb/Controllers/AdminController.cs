﻿using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDBContext db=new KurumsalDBContext();   
        // GET: Admin
        public ActionResult Index()
        {
           var sorgu=db.Kategori.ToList();
            return View(sorgu);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login=db.Admin.Where(x=>x.Eposta==admin.Eposta).SingleOrDefault();
            if(admin.Sifre!=null && admin.Eposta!=null)
            if (login.Eposta==admin.Eposta && login.Sifre==admin.Sifre)
            {
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index","Admin");
            }
            ViewBag.Uyari = "Kullanıcı Adı yada şifre yanlış"; //hata verdirme işlemi yapar
            return View();

        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();  //tanımladığımız Session'ları sonlandırarak böylece sunucumuzdaki yüküde azalmış oluruz.
            return RedirectToAction("Login", "Admin");

            
        }
    }
}