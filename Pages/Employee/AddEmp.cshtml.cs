using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_CRUD.Pages.Employee
{
    public class AddEmpModel : PageModel
    {
        public EmpInfo empInfo = new EmpInfo();
        public string err = "";
        public string str = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            
            empInfo.name = Request.Form["name"];
            empInfo.desig = Request.Form["desig"];
            empInfo.email = Request.Form["email"];
            empInfo.phone = Request.Form["phone"];


            if (empInfo.name.Length == 0 || empInfo.desig.Length == 0 || empInfo.email.Length == 0 || empInfo.phone.Length == 0)
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

                    string query = "SELECT COUNT(*) FROM employees";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                       empInfo.id = (int)cmd.ExecuteScalar();

                    }

                    string sql = "INSERT INTO employees (empid, empname, designation, email, phone) VALUES " +
                                  "(@id, @name ,@desig, @email, @phone);";

                    using (SqlCommand cmd1 = new SqlCommand(sql, conn))
                    {
                        

                        cmd1.Parameters.AddWithValue("@id", empInfo.id);
                        cmd1.Parameters.AddWithValue("@name", empInfo.name);
                        cmd1.Parameters.AddWithValue("@desig", empInfo.desig);
                        cmd1.Parameters.AddWithValue("@email", empInfo.email);
                        cmd1.Parameters.AddWithValue("@phone", empInfo.phone);

                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return;
            }
            empInfo.name = "";
            empInfo.desig = "";
            empInfo.email = "";
            empInfo.phone = "";

            str = "Client Added successfully";
            Response.Redirect("/Employee/Index");

        }
    }
}
