using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar; //Sınıfları dahil ettik.

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context(); //c isimli bir nesne türettik.
        public ActionResult Index()
        {
            /*var degerler = c.Personels.Where(x => x.Durum == true).ToList();*/ //Sadece durum=true olanları getirdik.
            var degerler = c.Personels.ToList();
            return View(degerler);
        }

        //***************PERSONEL EKLE*****************

        //View yüklendiği zaman burayı çalıştır.
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = x.DepartmanAd, //Bize gözükecek olan kısım.
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1; //Viewbag: controller tarafına veri göndermek için kullanılır.
            return View();
        }

        //Bir butona tıkladımız zaman post çalışsın.
        [HttpPost]
        public ActionResult PersonelEkle(Personel pe)
        {
            //*********** Görseli Dosya Olarak Çekme ***********
            if(Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files [0].FileName);
                string yol = "/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                pe.PersonelGorsel = "/Image/" +dosyaadi+ uzanti;
            }
            //k: View tarafında göndereceğimiz parametreler.
            c.Personels.Add(pe); //Context(c)'te bulununan Personels'e ekle pe değerini.
            c.SaveChanges(); //Bu değerleri veritabanına kaydet.
            return RedirectToAction("PersonelListe");
        }

        //***************PERSONEL SİL*****************
        public ActionResult PersonelSil(int id)
        {
            var deger = c.Personels.Find(id);
            deger.Durum = false; //İlişkili tablolar olduğu için tamamen silmiyoruz.Sadece sayfada görünmesini engelliyoruz.
            c.SaveChanges();
            return RedirectToAction("PersonelListe");
        }

        //**********************************GÜNCELLEME İŞLEMİ*****************************************

        //***************PERSONEL GETİR*****************
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger2 = (from y in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               //Dropdown yaptık.
                                               Text = y.DepartmanAd, //Bize gözükecek olan kısım.
                                               Value = y.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            var pers = c.Personels.Find(id);
            return View("PersonelGetir", pers);
        }

        //***************PERSONEL GÜNCELLE*****************
        public ActionResult PersonelGuncelle(Personel pr)
        {
            //*********** Görseli Dosya Olarak Çekme ***********
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                pr.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            var prs = c.Personels.Find(pr.Personelid);
            prs.PersonelAd = pr.PersonelAd;
            prs.PersonelSoyad = pr.PersonelSoyad;
            prs.PersonelGorsel = pr.PersonelGorsel;
            prs.Departmanid = pr.Departmanid;
            //prs.Durum = pr.Durum;
            c.SaveChanges();
            return RedirectToAction("PersonelListe");
        }

        //*************** PDF-Excel Haline Getirme *****************
        public ActionResult PersonelListesi()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }

        //********************************************************************* PERSONEL İÇİN YENİ TASARIM ******************************************************************

        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }


    }
}