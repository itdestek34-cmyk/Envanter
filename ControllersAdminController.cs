using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TARGET_Envanter_Web.Controllers
{
    public class AdminController : Controller
    {
        private string connStr = ConfigurationManager.ConnectionStrings["connTarget"].ConnectionString;

        public ActionResult Index()
        {
            if (Session["Role"]?.ToString() != "Admin")
                return RedirectToAction("Dashboard", "Home");

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Kullanicilar";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return View(dt);
        }
    }
}
