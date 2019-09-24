using TabloIslemleri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data.SqlClient; //sql baglantısı için
using System.Data; //DataTable nesnesini yaratmak için
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace TabloIslemleri.Controllers
{
    public class HomesController : Controller
    {

        public SqlConnection baglanti1 = new SqlConnection();
        private MvcProjesiContext Context = new MvcProjesiContext();
        // GET: Homes
        public ActionResult Index()
        {
            baglanti1.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            SqlCommand komut = new SqlCommand("Select * from NotKaydı   ", baglanti1);
            DataTable Tablo2 = new DataTable();
            SqlDataAdapter Adaptor = new SqlDataAdapter(komut);
            Adaptor.Fill(Tablo2);
            int sıra = Convert.ToInt32(Tablo2.Rows.Count);

            List<NotKaydı> NotListesi = new List<NotKaydı>();
            NotKaydı ogr = new NotKaydı();
            for (int j = 0; j < sıra; j++)
            {
                ogr = new NotKaydı();
                ogr.ID = Convert.ToInt32(Tablo2.Rows[j]["ID"]);
                ogr.ders = Convert.ToString(Tablo2.Rows[j]["ders"]);
                ogr.notlar = Convert.ToInt32(Tablo2.Rows[j]["notlar"]);
                NotListesi.Add(ogr);
            }
            return View(NotListesi);
       
        }
        
        // GET: Homes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        [HttpPost]
        public ActionResult Create(NotKaydı NotlarModel)
        {
            baglanti1.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            string sql = @"Insert into NotKaydı (ders,notlar) values (@ders,@notlar)";
            SqlCommand komut = new SqlCommand(sql, baglanti1);

            komut.Parameters.AddWithValue("@ID", NotlarModel.ID);
            komut.Parameters.AddWithValue("@ders", NotlarModel.ders);
            komut.Parameters.AddWithValue("@notlar", NotlarModel.notlar);     
            komut.Connection.Open();
            try
            {
                komut.ExecuteNonQuery();//sorgunun çalışma kontrolu
                ViewBag.Message = "Kayıt Basarılı Bir Şekilde Gerçekleştirilmiştir. ";
            }
            catch (Exception Hata)
            {
                ViewBag.Message = Hata.Message;//kayıt oluşmadıysa          
            }
            return View("Create");
        }

        // GET: Homes/Edit/5
        public ActionResult Edit(int id)
        {
            var notkaydı = Context.NotListesi.Find(id);
            return View(notkaydı);
        }

        // POST: Homes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, NotKaydı NotlarModel)
        {

            baglanti1.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            string sql = @"Update NotKaydı set ders=@ders,notlar=@notlar where ID=@ID ";
            SqlCommand komut = new SqlCommand(sql, baglanti1);

            komut.Parameters.AddWithValue("@ID", NotlarModel.ID);
            komut.Parameters.AddWithValue("@ders", NotlarModel.ders);
            komut.Parameters.AddWithValue("@notlar", NotlarModel.notlar);
            komut.Connection.Open();
            try
            {
                komut.ExecuteNonQuery();//sorgunun çalışma kontrolü
                ViewBag.Message = "Kayıt Basarılı Bir Şekilde Düzenlenmiştir. ";

            }
            catch (Exception Hata)
            {
                ViewBag.Message = Hata.Message;//kayıt oluşmadıysa 

            }
            return View();
        }

        // GET: Homes/Delete/5
        public ActionResult Delete(int id)
        {
            NotKaydı notkaydı = Context.NotListesi.Find(id);
            return View(notkaydı);
        }

        // POST: Homes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, NotKaydı NotlarModel)
        {
            baglanti1.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            string sql = @"Delete NotKaydı where ID=@ID";
            SqlCommand komut = new SqlCommand(sql, baglanti1);
            komut.Parameters.AddWithValue("@ID", NotlarModel.ID.ToString());
            try
            {
                baglanti1.Open();
                ViewBag.Message = "Kayıt Başarılı Bir Şekilde Silinmiştir.";
                komut.ExecuteNonQuery();
                baglanti1.Close();

                return RedirectToAction("Index");
            }
            catch (Exception Hata)

            {
                ViewBag.Message = Hata.Message;//silinmediyse
            }
            return View();
        }
    }
}
