using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Kütüphaneyi dahil ettik.

namespace MvcOnlineTicariOtomasyon.Controllers
{
   
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context(); //Nesne türettik.
        public ActionResult Index()
        {
            var urunler = c.Uruns.Where(x => x.Durum == true).ToList(); //Sadece durum=true olanları getirdik.
            return View(urunler);
        }

        //*************ÜRÜN EKLEME**************
        //View yüklendiği zaman burayı çalıştır.
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.KategoriAd, //Bize gözükecek olan kısım.
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1; //Viewbag: controller tarafına veri göndermek için kullanılır.
            return View();
        }

        //Bir butona tıkladımız zaman post çalışsın.
        [HttpPost]
        public ActionResult YeniUrun(Urun u)
        {
            //u: View tarafında göndereceğimiz parametreler.
            c.Uruns.Add(u); //Context(u)'te bulununan Uruns'e ekle u değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("Index");
        }

        //***************ÜRÜN SİL*****************
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false; //İlişkili tablolar olduğu için tamamen silmiyoruz.Sadece sayfada görünmesini engelliyoruz.
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************ÜRÜN GETİR*****************
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.KategoriAd, //Bize gözükecek olan kısım.
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }

        //***************ÜRÜN GÜNCELLE*****************
        public ActionResult UrunGuncelle(Urun u)
        {
            var urn = c.Uruns.Find(u.Urunid);
            urn.UrunAd = u.UrunAd;
            urn.Marka = u.Marka;
            urn.Stok = u.Stok;
            urn.AlisFiyat = u.AlisFiyat;
            urn.SatisFiyat = u.SatisFiyat;
            urn.Kategoriid = u.Kategoriid;
            urn.UrunGorsel = u.UrunGorsel;
            urn.Durum = u.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //*************** PDF-Excel Haline Getirme *****************
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }



        //********************************** SATIŞ YAPMA SAYFASI YENİ TASARIM ****************************************
        
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Personels.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.PersonelAd + " " + x.PersonelSoyad, //Bize gözükecek olan kısım.
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1; //Viewbag: controller tarafına veri göndermek için kullanılır.

            var deger2 = c.Uruns.Find(id);
            ViewBag.dgr2 = deger2.Urunid;
            ViewBag.dgr3 = deger2.SatisFiyat;
            return View();
        }

        //************ KAYDETME İŞLEMİ *************

        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            //p.ToplamTutar = p.Adet * p.Fiyat; 
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString()); //Tarihi otomatik alacağız.
            c.SatisHarekets.Add(p); //Context(c)'te bulununan SatisHarekets'e ekle p değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("Index", "Satis");
        }
    }
}