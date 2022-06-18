using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cascading //Class3
    {
        //Burada birbiriyle bağlantışlı dropdownlistler oluşturacağız.
        //Bir tane kategori için bir tane ürünler için dropdown olacak.
        //Kategori seçildiğinde ürünler için olan dropdownda sadece o kategoriye ait ürünler listelenecek.

        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IEnumerable<SelectListItem> Urunler { get; set; }
    }
}