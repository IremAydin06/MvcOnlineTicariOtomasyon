using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Sınıflara erişmek için ekledik.

namespace MvcOnlineTicariOtomasyon.Controllers
{
   
    public class KategoriController : Controller
    {
        // GET: Kategori

        Context c = new Context(); //Context adlı sınıfımızdan (tabloların tutulduğu sınıf) c isimli bir nesne türetelim.
        public ActionResult Index()
        {
            //Tablonun içindeki verilere ulaşmaya çalışacağız.
            //var türünde oluşturuyoruz çünkü var bütün değişken türlerini kapsar.
            var degerler = c.Kategoris.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }

        //***************KATEGORİ EKLE*****************

        //View yüklendiği zaman burayı çalıştır.
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        //Bir butona tıkladımız zaman post çalışsın.
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            //k: View tarafında göndereceğimiz parametreler.
            c.Kategoris.Add(k); //Context(c)'te bulununan Kategoris'e ekle k değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("Index");
        }

        //***************KATEGORİ SİL*****************
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            ktg.Durum = false;
            //c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************KATEGORİ GETİR*****************
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir", kategori);
        }

        //***************KATEGORİ GÜNCELLE*****************
        public ActionResult KategoriGuncelle(Kategori k)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriGetir");
            }
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            ktgr.Durum = k.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //**********************************KATEGORİ DETAY***************************************
        public ActionResult KategoriDetay(int id)
        {
            var degerler = c.Uruns.Where(x => x.Kategoriid == id).ToList();
            var ktgr = c.Kategoris.Where(x => x.KategoriID == id).Select(y => y.KategoriAd).FirstOrDefault();
            ViewBag.k = ktgr;
            return View(degerler);
        }

        //************************KATEGORİ ÜRÜN SATIŞ******************************************
        public ActionResult KategoriUrunSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Urunid == id).ToList();
            var e = c.Uruns.Where(x => x.Urunid == id).Select(y => y.UrunAd).FirstOrDefault();
            ViewBag.kt = e;
            return View(degerler);
        }

        //*************** PDF-Excel Haline Getirme *****************
        public ActionResult KategoriListesi()
        {
            var degerler = c.Kategoris.ToList();
            return View(degerler);
        }

        //************************ KATEGORİ ÜRÜN CASCADING DROPDOWNLIST ******************************************

        //Burada birbiriyle bağlantışlı dropdownlistler oluşturacağız.
        //Bir tane kategori için bir tane ürünler için dropdown olacak.
        //Kategori seçildiğinde ürünler için olan dropdownda sadece o kategoriye ait ürünler listelenecek.

        public ActionResult Cascade() //Deneme
        {
            Cascading cs = new Cascading();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cs);
        }

        public JsonResult KategoriUrun(int p) //UrunGetir
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}