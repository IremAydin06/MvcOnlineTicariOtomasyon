using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class DinamikF //Class4
    {
        public IEnumerable <Faturalar> deger1 { get; set; }
        public IEnumerable <FaturaKalem> deger2 { get; set; }
    }

    /*Bir sayfaya birden farklı tablodan veri çekmek için 2 yöntem vardır.
     * 1.PartialView kullanmak.
     * 2.IEnumerable türünde property oluşturup bunu view in içine implemente edip Model.deger1 şeklinde çağırmak.
    */
}