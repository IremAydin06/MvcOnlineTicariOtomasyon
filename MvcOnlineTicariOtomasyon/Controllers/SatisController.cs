using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Modellerimizi çağırdık.
namespace MvcOnlineTicariOtomasyon.Controllers
{
  
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context(); //Context'ten bir c nesnesi türettik.
        public ActionResult Index()
        {
            //var degerler = c.SatisHarekets.Where(x => x.Durum == true).ToList();
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }
        //***************SATIŞ EKLE*****************

        //View yüklendiği zaman burayı çalıştır.
        [HttpGet]
        public ActionResult SatisEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.UrunAd, //Bize gözükecek olan kısım.
                                               Value = x.Urunid.ToString()
                                           }).ToList();
           

            List<SelectListItem> deger2 = (from x in c.Carilers.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.CariAd + " " + x.CariSoyad, //Bize gözükecek olan kısım.
                                               Value = x.Cariid.ToString()
                                           }).ToList();


            List<SelectListItem> deger3 = (from x in c.Personels.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.PersonelAd + " " + x.PersonelSoyad, //Bize gözükecek olan kısım.
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1; //Viewbag: controller tarafına veri göndermek için kullanılır.
            ViewBag.dgr2 = deger2; 
            ViewBag.dgr3 = deger3; 
            return View();
        }

        //Bir butona tıkladığımız zaman post çalışsın.
        [HttpPost]
        public ActionResult SatisEkle(SatisHareket s)
        {
            //k: View tarafında göndereceğimiz parametreler.
            //s.ToplamTutar = s.Adet * s.Fiyat;
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString()); //Tarihi otomatik alacağız.
            c.SatisHarekets.Add(s); //Context(c)'te bulununan SatisHarekets'e ekle s değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("Index");
        }

        //***************SATIŞ SİL*****************
        //public ActionResult SatisSil(int id)
        //{
        //    var deger = c.SatisHarekets.Find(id);
        //    deger.Durum = false; //İlişkili tablolar olduğu için tamamen silmiyoruz.Sadece sayfada görünmesini engelliyoruz.
        //    c.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************SATIŞ GETİR*****************
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.UrunAd, //Bize gözükecek olan kısım.
                                               Value = x.Urunid.ToString()
                                           }).ToList();
            

            List<SelectListItem> deger2 = (from x in c.Carilers.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.CariAd + " " + x.CariSoyad, //Bize gözükecek olan kısım.
                                               Value = x.Cariid.ToString()
                                           }).ToList();
          

            List<SelectListItem> deger3 = (from x in c.Personels.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.PersonelAd + " " + x.PersonelSoyad, //Bize gözükecek olan kısım.
                                               Value = x.Personelid.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1; //Viewbag: controller tarafına veri göndermek için kullanılır.
            ViewBag.dgr2 = deger2; //Viewbag: controller tarafına veri göndermek için kullanılır.
            ViewBag.dgr3 = deger3; //Viewbag: controller tarafına veri göndermek için kullanılır.
            var sts = c.SatisHarekets.Find(id);
            return View("SatisGetir", sts);
        }

        //***************SATIŞ GÜNCELLE*****************
        public ActionResult SatisGuncelle(SatisHareket st)
        {
            var stsh = c.SatisHarekets.Find(st.Satisid);
            stsh.Urunid = st.Urunid;
            stsh.Cariid = st.Cariid;
            stsh.Personelid = st.Personelid;
            stsh.Adet = st.Adet;
            stsh.Fiyat = st.Fiyat;
            stsh.ToplamTutar = st.ToplamTutar;
            stsh.Tarih = st.Tarih;
            //prs.Durum = pr.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //***************SATIŞ DETAY*****************

        public  ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(degerler);
        }

        //*************** PDF-Excel Haline Getirme *****************
        public ActionResult SatisListesi()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        } 

    }
}