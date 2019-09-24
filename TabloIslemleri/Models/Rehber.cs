using TabloIslemleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //[key] etiketlerini kullanabilmek amacıyla

namespace TabloIslemleri.Models
{
    public class Rehber
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name giriniz")]
        [StringLength(50)]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "City giriniz")]
        [StringLength(50)]
        public string City { get; set; } 

        
        [Required(ErrorMessage = "Adresi giriniz")]
        [StringLength(100)]
        public string Address { get; set; }
    }
}