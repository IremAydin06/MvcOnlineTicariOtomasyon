using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        //**************************** GRAFİK ÇİZDİRME ***************

        //Manuel Verilerle Grafik Oluşturma

        //    public ActionResult GrafikCiz()
        //    {
        //        var grafikciz = new Chart(1000, 1000);
        //        grafikciz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Degerler",
        //            xValue: new[] { "B.Eşya", "Telefon", "K.Ev.A.", "PC", "Mobilya" },
        //            yValues: new[] { 58, 65, 32, 14 ,12}).Write();
        //        return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        //    }

        public ActionResult GrafikCiz()    //Index3
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x =>xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y =>yvalue.Add(y.Stok));
            var grafik = new Chart(width:500,height:500)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name:"Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

        }

        //****************************** GOOGLE CHART ************************************

        //*********** MANUEL *********
        public ActionResult GoogleChart() //Index4
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }
        public List<GoogleChart> Urunlistesi()
        {
            List<GoogleChart> snf = new List<GoogleChart>();
            snf.Add(new GoogleChart()
            {
                urunad = "Bilgisayar",
                stok = 120
            });
            snf.Add(new GoogleChart()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
            snf.Add(new GoogleChart()
            {
                urunad = "Mobilya",
                stok = 70
            });
            snf.Add(new GoogleChart()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });
            snf.Add(new GoogleChart()
            {
                urunad = "Mobil Cihazlar",
                stok = 90
            });
            return snf;
        }

        //********* KATEGORİ-ÜRÜN DİNAMİK ********

        public ActionResult GoogleChart2() //Index5
        {
            return View();
        }
        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunListesi2(), JsonRequestBehavior.AllowGet);
        }
        public List<GoogleChart2> UrunListesi2()
        {
            List<GoogleChart2> snf = new List<GoogleChart2>();
            using (var context = new Context())
            {
                snf = c.Uruns.Select(x => new GoogleChart2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }
            return snf;
        }

        public ActionResult LineChart() //Index6
        {
            return View();
        }

        public ActionResult ColumnChart()
        {
            return View();
        }
    }
}