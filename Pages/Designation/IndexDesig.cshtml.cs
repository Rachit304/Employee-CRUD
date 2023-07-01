using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_CRUD.Pages.Designation
{
    public class IndexDesigModel : PageModel
    {
        public List<DesigInfo> listDesig = new List<DesigInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=employee;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM designations";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DesigInfo desigInfo = new DesigInfo();
                                desigInfo.id = dr.GetInt32(0);
                                desigInfo.name = dr.GetString(1);
                                desigInfo.desc = dr.GetString(2);

                                listDesig.Add(desigInfo);

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

        public class DesigInfo
        {
            public int id;
            public string name;
            public string desc;

        }
    }
