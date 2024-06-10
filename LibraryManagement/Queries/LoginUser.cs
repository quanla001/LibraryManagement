using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Queries
{
    public class LoginUser
    {
        public LoginModels checkLogin(string email, string password)
        {
            LoginModels matchingData = new LoginModels();
            using (SqlConnection conn = Database.GetConnection()) 
            {
                string querySql = "SELECT * FROM students WHERE email = @email AND password = @password"; 
                SqlCommand cmd = new SqlCommand(querySql, conn);
                // truyen tham so 
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open(); // mo ket noi database
                //thuc thi cau lenh sql
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        // do du lieu tu table database sang model
                        matchingData.id = reader["id"].ToString();
                        matchingData.role_id = reader["role_id"].ToString();
                        matchingData.code = reader["code"].ToString();
                        matchingData.email = reader["email"].ToString();
                        matchingData.phone = reader["phone"].ToString();
                        matchingData.address = reader["address"].ToString();
                        matchingData.fullname = reader["fullname"].ToString();
                    }
                    conn.Close(); //dong ket noi (giai phong tai nguyen)
                }
            }
            return matchingData;
        }
    }
}
