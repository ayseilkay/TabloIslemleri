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
    public class HomeController : Controller
    {
        public SqlConnection baglanti = new SqlConnection();
        private MvcProjesiContext Context = new MvcProjesiContext();


        // GET: Home
        public ActionResult Index()
        {
            baglanti.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            SqlCommand komut = new SqlCommand("Select * from Rehbers order by Id desc", baglanti);
            DataTable Tablo = new DataTable();
            SqlDataAdapter Adaptor = new SqlDataAdapter(komut);
            Adaptor.Fill(Tablo);
            int satır = Convert.ToInt32(Tablo.Rows.Count);

            List<Rehber> rehberlistesi = new List<Rehber>();
            Rehber kisi = new Rehber();
            for (int j = 0; j < satır; j++)
            {
                kisi = new Rehber();
                kisi.Id = Convert.ToInt32(Tablo.Rows[j]["Id"]);
                kisi.Name = Convert.ToString(Tablo.Rows[j]["Name"]);
                kisi.City = Convert.ToString(Tablo.Rows[j]["City"]);
                kisi.Address = Convert.ToString(Tablo.Rows[j]["Address"]);
                rehberlistesi.Add(kisi);
            }
            return View(rehberlistesi);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Rehber RehberModel)
        {
            baglanti.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            string sql = @"Insert into Rehbers (Name,City,Address) values (@Name,@City,@Address)";
            SqlCommand komut = new SqlCommand(sql, baglanti);

            komut.Parameters.AddWithValue("@Id", RehberModel.Id);
            komut.Parameters.AddWithValue("@Name", RehberModel.Name);
            komut.Parameters.AddWithValue("@City", RehberModel.City);
            komut.Parameters.AddWithValue("@Address", RehberModel.Address);
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
        
            //try
            //{
            //    Context.RehberListesi.Add(RehberModel);

            //    try
            //    {
            //        Context.SaveChanges();
            //    }
            //    catch (ObjectDisposedException e)
            //    {
            //        string a = e.Message;

            //    }
            //    catch (NotSupportedException e)
            //    {
            //        string a = e.Message;
            //    }

            //    ViewBag.Message = "Kayıt Basarılı Bir Şekilde Gerçekleşmiştir.";
            //    ModelState.Clear();


            //    return View("Create");
            //}
            //catch(Exception e)
            //{
            //    ViewBag.Message = "Kayıt Başarısız Olmuştur";
            //    return View();
            //}

        }

        public ActionResult Edit(int id)
        {
            var rehber = Context.RehberListesi.Find(id);
            return View(rehber);
        }

        [HttpPost]
        public ActionResult Edit(Rehber RehberModel)
        {


            baglanti.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            string sql = @"Update Rehbers set Name=@Name,City=@City,Address=@Address where Id=@Id ";
            SqlCommand komut = new SqlCommand(sql, baglanti);

            komut.Parameters.AddWithValue("@Id", RehberModel.Id);
            komut.Parameters.AddWithValue("@Name", RehberModel.Name);
            komut.Parameters.AddWithValue("@City", RehberModel.City);
            komut.Parameters.AddWithValue("@Address", RehberModel.Address);
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Rehber rehber = Context.RehberListesi.Find(id);
            return View(rehber);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Rehber RehberModel)
        {
            baglanti.ConnectionString = "Data Source=DESKTOP-O1AM6QO ; Initial Catalog=Tablolar ; Integrated Security=True;Persist Security Info=True";
            string sql = @"Delete Rehbers where Id=@Id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@Id", RehberModel.Id.ToString());
            try
            {
                baglanti.Open();
                ViewBag.Message = "Kayıt Başarılı Bir Şekilde Silinmiştir.";
                komut.ExecuteNonQuery();
                baglanti.Close();

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
