using TabloIslemleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace TabloIslemleri.Models
{
    public class MvcProjesiContext:DbContext 
    {
        public MvcProjesiContext() : base("MvcProjesiContext")
        {

        }
        public DbSet<Rehber> RehberListesi { get; set; }
        public DbSet<NotKaydı> NotListesi { get; set; }

       
    }
}