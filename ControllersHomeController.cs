using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TARGET_Envanter_Web.Controllers
{
    public class HomeController : Controller
    {
        private string connStr = ConfigurationManager.ConnectionStrings["connTarget"].ConnectionString;

        public ActionResult Dashboard()
        {
            if (Session["User"] == null) return RedirectToAction("Index", "Login");

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Cihazlar";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ViewBag.User = Session["User"].ToString();
            ViewBag.Role = Session["Role"].ToString();
            return View(dt);
        }
    }
}
