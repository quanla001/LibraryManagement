using LibraryManagement.Logging;
using LibraryManagement.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LibraryManagement
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void tabPageHome_Click(object sender, EventArgs e)
        {

        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            labelUsername.Text = logininfo.FullNameUser;
            listViewAuthor.View = View.Details;
            listViewAuthor.GridLines = true;
            listViewAuthor.FullRowSelect = true;
            // tao cac cot
            listViewAuthor.Columns.Add("ID", 50);
            listViewAuthor.Columns.Add("Name", 150);
            listViewAuthor.Columns.Add("Nick Name", 150);
            listViewAuthor.Columns.Add("Gender", 50);
            listViewAuthor.Columns.Add("Birthday", 50);
            listViewAuthor.Columns.Add("Address", 250);
            listViewAuthor.Columns.Add("Biography", 200);
            listViewAuthor.Columns.Add("Status", 50);
            listViewAuthor.Columns.Add("Avatar", 250);
            listViewAuthor.Columns.Add("Created At", 150);
            listViewAuthor.Columns.Add("Updated At", 150);



        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialogAvatar_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialogAvatar.InitialDirectory = "C://Picture";
            openFileDialogAvatar.Title = "Select Image to be Upload";
            openFileDialogAvatar.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialogAvatar.FilterIndex = 1;
            if (openFileDialogAvatar.ShowDialog() == DialogResult.OK)
            {
                // thuc su co upload
                if (openFileDialogAvatar.CheckFileExists)
                {
                    // gan anh upload va hien thi vao pictureBox
                    pictureBox1.Image = new Bitmap(openFileDialogAvatar.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    // lay duong dan cua anh
                    string pathImage = System.IO.Path.GetFullPath(openFileDialogAvatar.FileName);
                    txtFileName.Text = pathImage;
                }
                else
                {
                    MessageBox.Show("Can not found File");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string female = radFemale.Text.Trim();
            string birthday = dateBirthday.Value.Date.ToString("yyyy-MM-dd");
            string male = radMale.Text.Trim();
            string fullname = txtName.Text.Trim();
            string nickname = txtNickName.Text.Trim();
            string status = cboStatus.Text.Trim();
            string biography = txtBiography.Text.Trim();
            string address = txtAddress.Text.Trim();
            string gender = radFemale.Checked ? female : male;
            // xu ly upload image
            TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            long secondTime = Convert.ToInt64 (timeSpan.TotalSeconds);
            string fileName = secondTime + "-" + System.IO.Path.GetFileName(openFileDialogAvatar.FileName);
            // dam bao ko trung ten anh de upload len thu muc uploads trong project
            string pathUploadfile = "\\Upload\\";
            string fullPathUpLoadFile = pathUploadfile + fileName;
            if (string.IsNullOrEmpty(fullname)) 
            {
                MessageBox.Show("Enter Name,please");
                return;
            }
            if (string.IsNullOrEmpty(male) && string.IsNullOrEmpty(female)) 
            {
                MessageBox.Show("Choose Gender,please");
                return;
            }
            if (string.IsNullOrEmpty(birthday))
            {
                MessageBox.Show("Enter Birthday,please");
                return;
            }
            if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Enter Status,please");
                return;
            }


            Authorquery authorQuery = new Authorquery();
            int idAthor = authorQuery.InsertAuthor(fullname, nickname, gender, birthday, address, biography, status, fullPathUpLoadFile,openFileDialogAvatar);
            if (idAthor > 0)
            {
                MessageBox.Show("Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // do du lieu ra lisview
                ListViewItem newItems = new ListViewItem(idAthor.ToString());
                newItems.SubItems.Add(fullname);
                newItems.SubItems.Add(nickname);
                newItems.SubItems.Add(gender);
                newItems.SubItems.Add(birthday);
                newItems.SubItems.Add(address);
                newItems.SubItems.Add(biography);
                newItems.SubItems.Add(status);
                newItems.SubItems.Add(fullPathUpLoadFile);
                newItems.SubItems.Add(DateTime.Now.ToString("dd-MM-yyyy"));
                listViewAuthor.Items.Add(newItems);
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void tabControlDasbhboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControlDasbhboard.SelectedTab == tabControlDasbhboard.TabPages["tabPageAuthorManagement"])
            {
                listViewAuthor.Items.Clear();
                listViewAuthor.Update();
                listViewAuthor.Refresh();
                Authorquery authorQuery = new Authorquery();
                authorQuery.GetAllDataAuthor(listViewAuthor);
            }
        }

        private void listViewAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAuthor.SelectedItems.Count > 0)
            {
                // lay du lieu tung cot trong listview do vao cac textbox len form
                txtID.Text = listViewAuthor.SelectedItems
                    [0].SubItems[0].Text.Trim();
                txtName.Text = listViewAuthor.SelectedItems
                    [0].SubItems[1].Text.Trim();
                txtNickName.Text = listViewAuthor.SelectedItems
                    [0].SubItems[2].Text.Trim();
                string pathImg = listViewAuthor.SelectedItems[0].SubItems[8].Text.Trim();
                string pathApp = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 26));
                if(File.Exists(@pathApp + pathImg)) {
                    pictureBox1.Image = Image.FromFile(@pathApp + pathImg);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    txtFileName.Text = pathImg;
                }

                string gender = listViewAuthor.SelectedItems[0].SubItems[3].Text.Trim();
                if (gender == "Male")
                {
                    radMale.Checked = true;
                    radFemale.Checked = false;
                }
                else if (gender == "Female")
                {
                    radMale.Checked = false;
                    radFemale.Checked = true;
                }
                string birthDay = listViewAuthor.SelectedItems[0].SubItems[4].Text.Trim();
                dateBirthday.Value = Convert.ToDateTime(birthDay);
                txtAddress.Text = listViewAuthor.SelectedItems[0].SubItems[5].Text.Trim();
                txtBiography.Text = listViewAuthor.SelectedItems[0].SubItems[6].Text.Trim();
                cboStatus.SelectedItem = listViewAuthor.SelectedItems[0].SubItems[7].Text.Trim();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string female = radFemale.Text.Trim();
            string birthday = dateBirthday.Value.Date.ToString("yyyy-MM-dd");
            string male = radMale.Text.Trim();
            string fullname = txtName.Text.Trim();
            string nickname = txtNickName.Text.Trim();
            string status = cboStatus.Text.Trim();
            string biography = txtBiography.Text.Trim();
            string address = txtAddress.Text.Trim();
            string gender = radFemale.Checked ? female : male;
            // xu ly upload image
            string fullPathUpLoadFile;
            bool hasUploadFile;
            if (File.Exists(openFileDialogAvatar.FileName))
            {
                TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                long secondTime = Convert.ToInt64(timeSpan.TotalSeconds);
                string fileName = secondTime + "-" + System.IO.Path.GetFileName(openFileDialogAvatar.FileName);
                // dam bao ko trung ten anh de upload len thu muc uploads trong project
                string pathUploadfile = "\\Upload\\";
                fullPathUpLoadFile = pathUploadfile + fileName;
                hasUploadFile = true;
                MessageBox.Show("Co upload Anh");
            }
            else
            {
                 fullPathUpLoadFile = txtFileName.Text.Trim();
                hasUploadFile = false;
                MessageBox.Show("Khong upload Anh");

            }
            // validate data
            if (string.IsNullOrEmpty(fullname))
            {
                MessageBox.Show("Enter Name,please");
                return;
            }
            if (string.IsNullOrEmpty(male) && string.IsNullOrEmpty(female))
            {
                MessageBox.Show("Choose Gender,please");
                return;
            }
            if (string.IsNullOrEmpty(birthday))
            {
                MessageBox.Show("Enter Birthday,please");
                return;
            }
            if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Enter Status,please");
                return;
            }
            int idAuthor = Convert.ToInt32(txtID.Text.Trim());
            Authorquery authorQuery = new Authorquery();
            bool update = authorQuery.UpdateAuthor(idAuthor,fullname,nickname,gender,birthday,address,biography,status,fullPathUpLoadFile,hasUploadFile,openFileDialogAvatar);
            if (update)
            {
                var dataItem = listViewAuthor.SelectedItems[0];
                dataItem.SubItems[1].Text = fullname;


                MessageBox.Show("Update successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                listViewAuthor.Update();
                listViewAuthor.Refresh();
                MessageBox.Show("Update failure", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
