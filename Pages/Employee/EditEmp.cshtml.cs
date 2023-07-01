using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_CRUD.Pages.Employee
{
    public class EditEmpModel : PageModel
    {
        public EmpInfo empInfo = new EmpInfo();
        public string err = "";
        public string str = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=employee;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM employees WHERE empid=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                empInfo.id = dr.GetInt32(0);
                                empInfo.name = dr.GetString(1);
                                empInfo.desig = dr.GetString(2);
                                empInfo.email = dr.GetString(3);
                                empInfo.phone = dr.GetString(4);
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

        public void OnPost()
        {
            empInfo.id = int.Parse(Request.Form["id"]);
            empInfo.name = Request.Form["name"];
            empInfo.desig = Request.Form["desig"];
            empInfo.email = Request.Form["email"];
            empInfo.phone = Request.Form["phone"];


            if (empInfo.name.Length == 0 || empInfo.email.Length == 0 || empInfo.phone.Length == 0 || empInfo.desig.Length == 0)
            {
                err = "All the Fields are Required";
                return;
            }

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=employee;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE employees " +
                                    "SET empname=@name, designation=@desig, phone=@phone, email=@email " +
                                    "WHERE empid=@id";

                    using (SqlCommand cmd1 = new SqlCommand(sql, conn))
                    {
                        cmd1.Parameters.AddWithValue("@name", empInfo.name);
                        cmd1.Parameters.AddWithValue("@desig", empInfo.desig);
                        cmd1.Parameters.AddWithValue("@email", empInfo.email);
                        cmd1.Parameters.AddWithValue("@phone", empInfo.phone);
                        cmd1.Parameters.AddWithValue("@id", empInfo.id);
                        cmd1.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                err = ex.Message;
                return;
            }

            Response.Redirect("/Employee/Index");
        }
    }
}
