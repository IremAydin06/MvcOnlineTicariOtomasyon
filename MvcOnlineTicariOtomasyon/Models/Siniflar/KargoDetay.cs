using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        //Bir tane müşteri paneli olacak
        //Bir tane satış yapan personel için panel olacak
        //Normalde bir tane de kargo firması için panel olması gerekiyor. Ama o kargo firmasının sorunu bizim değil :)

        [Key]
        public int KargoDetayid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Aciklama { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string TakipKodu { get; set; }  //1234123AB gibi

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Personel { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Alici { get; set; }
        public DateTime Tarih { get; set; }
    }
}