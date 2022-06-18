using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Sınıflarımızı dahil ediyoruz.

namespace MvcOnlineTicariOtomasyon.Controllers
{
    
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context(); //Context'imizden c isimli bir nesne türettik.
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }

        //***************FATURA EKLE*****************

        //View yüklendiği zaman burayı çalıştır.
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        //Bir butona tıkladımız zaman post çalışsın.
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            //f: View tarafında göndereceğimiz parametreler.
            c.Faturalars.Add(f); //Context(c)'te bulununan Faturalars'a ekle f değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("Index");
        }

        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************FATURA GETİR*****************
        public ActionResult FaturaGetir(int id)
        {
            var fa = c.Faturalars.Find(id);
            return View("FaturaGetir", fa);
        }

        //***************FATURA GÜNCELLE*****************
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var ft = c.Faturalars.Find(f.Faturaid); //Neyin düzeltileceğini söyledik.
            ft.FaturaSeriNo = f.FaturaSeriNo;
            ft.FaturaSıraNo = f.FaturaSıraNo;
            ft.Tarih = f.Tarih;
            ft.Saat = f.Saat;
            ft.VergiDairesi = f.VergiDairesi;
            ft.TeslimEden = f.TeslimEden;
            ft.TeslimAlan = f.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //*************** PDF-Excel Haline Getirme *****************
        public ActionResult FaturaListesi()
        {
            var degerler = c.Faturalars.ToList();
            return View(degerler);
        }

        //**********************************FATURA DETAY&KALEM İŞLEMLERİ*****************************************
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            var ftr = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            ViewBag.f = ftr;
            return View(degerler);
        }

        //***************FATURA KALEM EKLEME*****************
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem k)
        {

            c.FaturaKalems.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //************************************************** DİNAMİK FATURALAR **********************************************************************

        public ActionResult Dinamik()
        {
            //Bir sayfaya birden fazla veri çekeceğiz.
            //Ienumarable sınıfını oluşturup içindeki propertyleri çağıracağız.

            DinamikF cs = new DinamikF();
            cs.deger1 = c.Faturalars.ToList(); //deger1 faturaları listelesin.
            cs.deger2 = c.FaturaKalems.ToList(); //deger2 faturakalemleri listelesin.
            return View(cs); //layout olarak _Layout kullandık.
        }

        //*************** FATURA KAYDETME İŞLEMİ *****************

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo,DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler )
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimAlan = TeslimAlan;
            f.TeslimEden = TeslimEden;
            //f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.Faturaid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}