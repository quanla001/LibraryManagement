using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LibraryManagement.Queries;
using LibraryManagement.Models;
using LibraryManagement.Logging;

namespace LibraryManagement
{
    public partial class Login : Form
    {
        SqlConnection conn;
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            conn = Database.GetConnection();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            try
            {
                LoginUser loginUser = new LoginUser();
                LoginModels dataUser = loginUser.checkLogin(username, password);
                if (string.IsNullOrEmpty(dataUser.id) || string.IsNullOrEmpty
                    (dataUser.email))
                {
                    MessageBox.Show("Account invalid");
                }
                else
                {
                    MessageBox.Show("Login Success");
                    // luu thong tin cua nguoi dung de tien su dung sau nay
                    logininfo.UserID = dataUser.id;
                    logininfo.RoleID = dataUser.role_id;
                    logininfo.EmailUser = dataUser.email;
                    logininfo.FullNameUser = dataUser.fullname;

                    FormDashboard home = new FormDashboard();
                    home.Show();
                    this.Hide();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            string message = "Are you sure?";
            string title = "Close window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
