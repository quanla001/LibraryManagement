﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagement.Queries
{
    public class CategoriesQueries
    {
        public int InsertItemCategory(string nameCate, string desCate, string statusCate)
        {
            string sqlInsert = "INSERT INTO categories (name,description,parent_id,status,created_at) VALUES(@name, @description, @parent_id, @status, @created_at) SELECT SCOPE_IDENTITY()";
            // khi ma insert du lieu xong, tra luon id vuamoi insert
            int LastIDInsert;
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                // kiem tra du lieu dau vao
                cmd.Parameters.AddWithValue("@name", nameCate ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@description", desCate ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@parent_id", 0);
                cmd.Parameters.AddWithValue("@status", statusCate ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@created_at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                // lay ra duoc id moi nhat cua insert thanh cong
                LastIDInsert = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return LastIDInsert;
        }
        public void GetAllDataCategories(ListView Categories)
        {
            using(SqlConnection conn = Database.GetConnection())
            {
                string sqlQuery = "SELECT * FROM categories";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {   
                    var itemCategory = Categories.Items.Add(reader["id"].ToString());
                    itemCategory.SubItems.Add(reader["name"].ToString());
                    itemCategory.SubItems.Add(reader["description"].ToString());
                    itemCategory.SubItems.Add(reader["status"].ToString());
                    itemCategory.SubItems.Add(reader["created_at"].ToString());
                    itemCategory.SubItems.Add(reader["updated_at"].ToString());
                }
                conn.Close();
            }
        }
    }
}
