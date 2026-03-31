using OfficeOpenXml;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace TARGET_Envanter_Web.Controllers
{
    public class ReportsController : Controller
    {
        private string connStr = ConfigurationManager.ConnectionStrings["connTarget"].ConnectionString;

        public ActionResult ExportExcel()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Cihazlar";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Envanter");
                sheet.Cells["A1"].LoadFromDataTable(dt, true);
                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TARGET_Envanter.xlsx");
            }
        }
    }
}
