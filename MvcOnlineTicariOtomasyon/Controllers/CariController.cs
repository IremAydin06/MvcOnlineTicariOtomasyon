using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Sınıflarımızı dahil ettik.

namespace MvcOnlineTicariOtomasyon.Controllers
{
    
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context(); //Context'ten nesne türettik.
        public ActionResult Index()
        {

            //Tablonun içindeki verilere ulaşmaya çalışacağız.
            //var türünde oluşturuyoruz çünkü var bütün değişken türlerini kapsar.
            var degerler = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }


        //***************CARİ EKLE*****************

        //View yüklendiği zaman burayı çalıştır.
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        //Bir butona tıkladımız zaman post çalışsın.
        [HttpPost]
        public ActionResult CariEkle(Cariler ca)
        {
            if (!ModelState.IsValid)
            {
                return View("CariEKle");
            }
            //k: View tarafında göndereceğimiz parametreler.
            c.Carilers.Add(ca); //Context(c)'te bulununan Departmans'a ekle d değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("CariListe");
        }

        //***************CARİ SİL*****************
        public ActionResult CariSil(int id)
        {
            var car = c.Carilers.Find(id);
            car.Durum = false;
            c.SaveChanges();
            return RedirectToAction("CariListe");
        }

        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************CARİ GETİR*****************
        public ActionResult CariGetir(int id)
        {
            var cari = c.Carilers.Find(id);
            return View("CariGetir", cari);
        }

        //***************CARİ GÜNCELLE*****************
        public ActionResult CariGuncelle(Cariler cr)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cariler = c.Carilers.Find(cr.Cariid);
            cariler.CariAd = cr.CariAd;
            cariler.CariSoyad = cr.CariSoyad;
            cariler.CariSehir = cr.CariSehir;
            cariler.CariMail = cr.CariMail;
            //cariler.Durum = cr.Durum;
            c.SaveChanges();
            return RedirectToAction("CariListe");
        }

        //***************CARİ SATIŞ GEÇMİŞİ*****************
        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }

        //*************** PDF-Excel Haline Getirme *****************
        public ActionResult CariListesi()
        {
            var degerler = c.Carilers.ToList();
            return View(degerler);
        }

        //********************************************************************* CARİLER İÇİN YENİ TASARIM ******************************************************************

        public ActionResult CariListe()
        {
            var sorgu = c.Carilers.ToList();
            return View(sorgu);
        }
    }
}