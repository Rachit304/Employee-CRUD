using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_CRUD.Pages.WorkHour
{
    public class IndexWorkModel : PageModel
    {
        public WorkInfo workInfo = new WorkInfo();
        public string err = "";
        public string str = "";

        public void OnGet()
        {
            int id = int.Parse(Request.Query["id"]);
            string name = (string)Request.Query["name"];    

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=employee;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Working_hours WHERE empid = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                workInfo.id = dr.GetInt32(0);
                                workInfo.company_hr = dr.GetInt32(1);
                                workInfo.emp_hr = dr.GetInt32(2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return;
            }
        }
    }

    public class WorkInfo
    {
        public int id { get; set; }
        public int company_hr { get; set; }
        public int emp_hr { get; set; }
    }
}
