using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TARGET_Envanter_Web.Controllers
{
    public class LoginController : Controller
    {
        private string connStr = ConfigurationManager.ConnectionStrings["connTarget"].ConnectionString;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Eposta, string Sifre)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT AdSoyad, Rol FROM Kullanicilar WHERE Eposta=@mail AND Sifre=@pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mail", Eposta);
                cmd.Parameters.AddWithValue("@pass", Sifre);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["User"] = reader["AdSoyad"].ToString();
                    Session["Role"] = reader["Rol"].ToString();
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ViewBag.Error = "Geçersiz kullanıcı adı veya şifre.";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
