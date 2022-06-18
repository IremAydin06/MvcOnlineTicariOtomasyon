using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Sınıflarımızı dahil ediyoruz.

namespace MvcOnlineTicariOtomasyon.Controllers
{
   
    public class DepartmanController : Controller
    {
        // GET: Departman

        Context c = new Context(); //Context'ten nesne(c) türettik.
        public ActionResult Index()
        {
            //Tablonun içindeki verilere ulaşmaya çalışacağız.
            //var türünde oluşturuyoruz çünkü var bütün değişken türlerini kapsar.
            var degerler = c.Departmans.Where(x => x.Durum==true).ToList();
            return View(degerler);
        }

        //********************************** DEPARTMAN EKLE *****************************************

        //View yüklendiği zaman burayı çalıştır.

        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        //Bir butona tıkladımız zaman post çalışsın.
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanEkle");
            }
            //k: View tarafında göndereceğimiz parametreler.
            c.Departmans.Add(d); //Context(c)'te bulununan Departmans'a ekle d değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("Index");
        }

        //********************************** DEPARTMAN SİL *****************************************
        public ActionResult DepartmanSil(int id)
        {
            var dprt = c.Departmans.Find(id);
            dprt.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************DEPARTMAN GETİR*****************
        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir", departman);
        }

        //***************DEPARTMAN GÜNCELLE*****************
        public ActionResult DepartmanGuncelle(Departman d)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanGetir");
            }
            var dprtmn = c.Departmans.Find(d.Departmanid);
            dprtmn.DepartmanAd = d.DepartmanAd;
            dprtmn.Durum = d.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //********************************** DEPARTMAN DETAY *****************************************
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;
            return View(degerler);
        }

        //********************************** DEPARTMAN PERSONEL SATIŞ *****************************************
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var per = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dp = per;
            return View(degerler);
        }

        //********************************** PDF-EXCEL HALİNE GETİRME *****************************************
        public ActionResult DepartmanListesi()
        {
            var degerler = c.Departmans.ToList();
            return View(degerler);
        }

    }
}