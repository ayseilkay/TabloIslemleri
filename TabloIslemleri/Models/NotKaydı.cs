using TabloIslemleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //[key] etiketlerini kullanabilmek amacıyla
namespace TabloIslemleri.Models
{
    public class NotKaydı
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "ders adını giriniz")]
        [StringLength(50)]
        public string ders { get; set; }
        public int notlar { get; set; }
    }
}