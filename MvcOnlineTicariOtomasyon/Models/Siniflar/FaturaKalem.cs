using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class FaturaKalem
    {
        [Key]
        [Display(Name = "Fatura Kalem ID")]
        public int FaturaKalemid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter yazabilirsiniz.")]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public int Miktar { get; set; }

        [Display(Name = "Birim Fiyat")]
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }

        [Display(Name = "Fatura ID")]
        public int Faturaid { get; set; }

        //Bir faturakalemin sadece 1 faturası olabilir.
        public virtual Faturalar Faturalar { get; set; }
    }
}