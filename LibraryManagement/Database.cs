using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace LibraryManagement
{
    internal class Database
    {
        protected static string getStrConnection()
        {
            string strConnection = @"Data Source=BINHQUAN\BINHQUAN;
                Initial Catalog=LibaryOnline;Integrated Security=True";
            return strConnection;
        }
        public static SqlConnection GetConnection() 
        {
            string strConnection = Database.getStrConnection();
            SqlConnection conn = new SqlConnection(strConnection);
            return conn;
        }
    }
}
