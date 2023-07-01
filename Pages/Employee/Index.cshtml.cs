using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_CRUD.Pages.Employee
{
    public class IndexModel : PageModel
    {
        public List<EmpInfo> listClients = new List<EmpInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=employee;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM employees";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EmpInfo empInfo = new EmpInfo();
                                empInfo.id = dr.GetInt32(0);
                                empInfo.name = dr.GetString(1);
                                empInfo.desig = dr.GetString(2);
                                empInfo.email = dr.GetString(3);
                                empInfo.phone = dr.GetString(4);
                 
                                listClients.Add(empInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }

        }
    }

    public class EmpInfo
    {
        public int  id;
        public string name;
        public string desig;
        public string email;
        public string phone;

    }
}
