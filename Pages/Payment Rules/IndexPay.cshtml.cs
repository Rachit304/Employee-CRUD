using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_CRUD.Pages.Payment_Rules
{
    public class IndexPayModel : PageModel
    {
        public List<PayInfo> listPay = new List<PayInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=employee;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM payment_rules";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                PayInfo payInfo = new PayInfo();
                                payInfo.id = dr.GetInt32(0);
                                payInfo.name = dr.GetString(1);
                                payInfo.desc = dr.GetString(2);
                                payInfo.amt = dr.GetDecimal(3);
                                listPay.Add(payInfo);

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

    public class PayInfo
    {
        public int id;
        public string name;
        public string desc;
        public decimal amt;

    }
}

