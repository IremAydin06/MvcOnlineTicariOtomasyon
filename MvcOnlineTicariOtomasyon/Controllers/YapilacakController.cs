using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Kütüphanemizi dahil ettik

namespace MvcOnlineTicariOtomasyon.Controllers
{

    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context c = new Context(); //Nesnemizi türettik.
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString(); //Toplam cari sayısı                            
            var deger2 = c.Uruns.Count().ToString(); //Toplam ürün sayısı
            var deger3 = c.Kategoris.Count().ToString(); //Toplam kategori sayısı
            var deger4 = (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString(); //Toplam şehir sayısı
            ViewBag.d1 = deger1;
            ViewBag.d2 = deger2;
            ViewBag.d3 = deger3;
            ViewBag.d4 = deger4;


            //****************** YAPILACAKLAR LİSTESİ *************************

            var yapilacaklar = c.Yapilacaks.ToList();

            return View(yapilacaklar);
        }
    }
}