﻿using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
       MvcDbStokEntities db=new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler=db.TBLKATEGORILER.ToList();
            return View(degerler);
        }
        [HttpGet] 
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();
        }
    }
}