using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;

            var deger5 = c.Uruns.Sum(x =>  x.Stok).ToString();
            ViewBag.d5 = deger5;

            /*var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();*/ //Ürünler içinden markayı seç ve tekrarsız olarak say.
            var deger6 = c.Uruns.Select(x => x.Marka).Distinct().Count();
            ViewBag.d6 = deger6;

            var deger7 = c.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;

            /*var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();*/ //Ürünler tablosunda satış fiyatı büyükten küçüğe sırala ve en büyük değerin adını seç.
            var deger8 = c.Uruns.Select(x => x.SatisFiyat).Max();
            ViewBag.d8 = deger8;

            /*var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();*/
            var deger9 = c.Uruns.Select(x => x.SatisFiyat).Min();
            ViewBag.d9 = deger9;

            //Ürün tablom içinde markaları z'den a'ya gruplandır. Sıralamanın kaç tane olduğunu hesapla.
            //Gruplardığın değerinin ismini seç.(Key)
            var deger10 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault(); //MAX MARKA: ÜRÜn tablosu içinde ismi en fazla geçen marka.
            ViewBag.d10 = deger10;

            var deger11 = c.Uruns.Where(x => x.UrunAd == "Buzdolabi").Sum(y => (decimal?)y.Stok).ToString();
            //var deger11 =c.Uruns.Count(x => x.UrunAd == "Buzdolabi").ToString();
            ViewBag.d11 = deger11;

            var deger12 =c.Uruns.Where(x =>x.UrunAd == "Firin").Sum(y => (decimal?)y.Stok).ToString();
            //var deger12 = c.Uruns.Count(x => x.UrunAd == "Firin").ToString();
            ViewBag.d12 = deger12;

            //SatışHarekets tablosunda Urunid'ye göre count'u a'dan z'ye gruplandır.
            //En üstteki değeri al.Seçilen değerin adını getir.
            var deger13 = c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault(); //En Çok Satan
            ViewBag.d13 = deger13;

            var deger14 = c.SatisHarekets.Sum(x =>x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;

            DateTime bugun = DateTime.Now.Date;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
            ViewBag.d15 = deger15;

            //var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(x => x.ToplamTutar).ToString();
            var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar).ToString(); //decimal? yazdık çünkü null olduğunda hata fırlatıyor. Bunu yazınca null dönse bile hara vermiyor.
            if (deger15 == "0")
            {
                deger16 = "0";
            }
            
            ViewBag.d16 = deger16;

            return View();
        }

        //********************KOLAY TABLOLAR**********************
        public ActionResult KolayTablolar()
        {
            return View();
        }

        //*****PARTIAL VIEW İLE KOLAY TABLOLAR*****
        //Partial View sayesinde tablonun istediğimiz kısmına farklı tablolardan veri çekebileceğiz.
        //Lego gibi sanırım.
        public PartialViewResult Partial() //Müşteri Şehir Tablosu
        {
            var sorgu = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return PartialView(sorgu.ToList());
        }
        public PartialViewResult Partial1() //Departman-Personel Sayısı Tablosu
        {
            var sorgu1 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         { 
                             Departman = g.Key,
                             Sayi =g.Count()
                         };
            return PartialView(sorgu1.ToList());
        }

        public PartialViewResult Partial2() //Cariler Tablosu
        {
            var sorgu2 = c.Carilers.ToList();
            return PartialView(sorgu2);
        }

        public PartialViewResult Partial3() //Ürünler Tablosu
        {
            
            var sorgu3 = c.Uruns.ToList();
            return PartialView(sorgu3);
        }

        public PartialViewResult Partial4() //Kategori-Ürün Sayısı Tablosu
        {
            var sorgu4 = from x in c.Uruns
                         group x by x.Kategori.KategoriAd into g
                         select new SinifGrup4
                         {
                             Kategori = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu4.ToList());
            
        }
        public PartialViewResult Partial5() //Marka Ürün Adet Tablosu
        {
            var sorgu5 = from x in c.Uruns
                        group x by x.Marka into g
                        select new SinifGrup3
                        {
                            Marka = g.Key,
                            Sayi = g.Count()
                        };
            return PartialView(sorgu5.ToList());
        }
    }
}