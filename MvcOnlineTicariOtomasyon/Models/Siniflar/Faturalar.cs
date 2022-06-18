using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Faturalar
    {
        [Key]
        [Display(Name = "Fatura ID")]
        public int Faturaid { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1, ErrorMessage = "En fazla 1 karakter yazabilirsiniz.")]
        [Display(Name = "Fatura Seri No")]
        public string FaturaSeriNo { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(6, ErrorMessage = "En fazla 6 karakter yazabilirsiniz.")]
        [Display(Name = "Fatura Sıra No")]
        public string FaturaSıraNo { get; set; }

        public DateTime Tarih { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(60, ErrorMessage = "En fazla 60 karakter yazabilirsiniz.")]
        [Display(Name = "Vergi Dairesi")]
        public string VergiDairesi { get; set; }


        [Column(TypeName = "char")]
        [StringLength(5, ErrorMessage = "En fazla 5 karakter yazabilirsiniz.")]
        public string Saat { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        [Display(Name = "Teslim Eden")]
        public string TeslimEden { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz.")]
        [Display(Name = "Teslim Alan")]
        public string TeslimAlan { get; set; }

        public decimal Toplam { get; set; }


        //Bir faturanın 1'den çok kalemi olabilir.
        public ICollection<FaturaKalem> FaturaKalems { get; set; }
    }
}