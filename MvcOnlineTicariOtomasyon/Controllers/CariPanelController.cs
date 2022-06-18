using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();

         //Authorize işleminin kontrolü için ekledik.

        //********************* CARİ PROFİLİM **************************
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail; //Maili giriş yaptıktan sonra ekrana yazdırdık.

            var mailid = c.Carilers.Where(x =>x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault(); //Cariid yi ekrana yazdırma.
            ViewBag.mid = mailid;

            var toplamsatis = c.SatisHarekets.Where(x =>x.Cariid == mailid).Count(); //Satın alma sayısını ekrana yazdırma.
            ViewBag.toplamsatis = toplamsatis;

            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum( y => (decimal?)y.ToplamTutar)??0; //Carinin yaptığı bütün satın almaların toplam tutarını hesaplatma.
            ViewBag.toplamtutar = toplamtutar;

            var toplamurun = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (int?)y.Adet)??0; //Carinin satın aldığı ürün sayısı.
            ViewBag.toplamurun = toplamurun;

            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd +" "+ y.CariSoyad).FirstOrDefault(); //Carinin ad-soyadını yazdırma.
            ViewBag.adsoyad = adsoyad;

            var sehir = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariSehir).FirstOrDefault(); //Carinin şehrini yazdırma.
            ViewBag.sehir = sehir;

            return View(degerler);
        }

        //********************* CARİ SİPARİŞLERİM **************************
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault(); //Sisteme giriş yapan mail adresinin id'sini aldık.
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList(); //id'si Cariid'ye eşit olan carinin satış hareketlerini degerler adlı değişkene atadık.
            return View(degerler);
        }

        //*************** PDF-EXCEL HALİNE GETİRME *****************
        public ActionResult SiparisListesi()
        {
            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault(); //Sisteme giriş yapan mail adresinin id'sini aldık.
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList(); //id'si Cariid'ye eşit olan carinin satış hareketlerini degerler adlı değişkene atadık.
            return View(degerler);
        }

        //********************* CARİ MESAJLAR **************************

        //******** GELEN MESAJLAR *********
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList(); //sisteme giriş yapan mail adresine ait mailleri getirmesini söyledik.

            //Gelen kutusundaki mesaj sayısı
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            //Giden kutusundaki mesaj sayısı
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(mesajlar);
        }

        //******** GÖNDERİLEN MESAJLAR *********
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(z => z.MesajID).ToList(); //sisteme giriş yapan mail adresine ait mailleri getirmesini söyledik.

            //Gelen kutusundaki mesaj sayısı
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            //Giden kutusundaki mesaj sayısı
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(mesajlar);
        }

        //******** MESAJ DETAYLARI *********
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.

            //Gelen kutusundaki mesaj sayısı
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            //Giden kutusundaki mesaj sayısı
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View(degerler);
        }

        //******** YENİ MESAJLAR *********

        [HttpGet]
        public ActionResult YeniMesajlar()
        {
            var mail = (string)Session["CariMail"]; //CariMail'den gelen değeri bir Session olarak tutup işlemlerimizi CariMail'e göre yapacağız.

            //Gelen kutusundaki mesaj sayısı
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;

            //Giden kutusundaki mesaj sayısı
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;

            return View();
        }

        [HttpPost]
        public ActionResult YeniMesajlar(Mesajlar m)
        {
            var mail = (string)(Session["CariMail"]);
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }

        //********************* KARGO TAKİP **************************

        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Equals(p));
            return View(k.ToList());
        }

        //******** KARGO DETAY *********
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        //********************* LOGOUT İŞLEMİ **************************

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut(); //Giriş işleminden çık.
            Session.Abandon(); //İstekleri terket.
            return RedirectToAction("Index","Login"); //Login index e yönlendir.

        }

        //********************* GÜNCELLEME - AYARLAR **************************
      
        public PartialViewResult Ayarlar()
        {
            var mail = (string)Session["CariMail"];

            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y =>y.Cariid).FirstOrDefault();
            
            var caribul = c.Carilers.Find(id);

            return PartialView("Ayarlar", caribul);
        }

        //********* GÜNCELLEME İŞLEMİ **********
        
        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.Cariid);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariMail = cr.CariMail;
            cari.CariSehir = cr.CariSehir;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges(); 
            return RedirectToAction("Index");
        }

        //********************* DUYURULAR **************************
        public PartialViewResult Duyurular()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin" && x.Alici == "everyone").OrderByDescending(y => y.Tarih).ToList(); //Sadece göndericisi admin alıcısı everyone olan mesajları listeledik.
            return PartialView(veriler);
        }


    }
}