using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminUserUpdate : Form
    {
        public FormAdminUserUpdate()
        {
            InitializeComponent();
        }

        //kullanıcı güncelleme
        private void button1_Click(object sender, EventArgs e)
        {
            var userDAL = new UserDAL();
            //texboxlarının boş olmaması durum kontrolu
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.SelectedItem != null && (radioButton1.Checked == true || radioButton2.Checked == true) && (radioButton3.Checked == true || radioButton4.Checked == true))
            {
                //regex check
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(textBox5.Text);
                if (match.Success)
                {
                    RollDAL rollDAL = new RollDAL();
                    var selectedRol = comboBox1.SelectedItem as string;
                    var rol = rollDAL.GetRoleByName(selectedRol);
                    //kullanıcıyı güncelliyorum
                    var changeUser = userDAL.GetUserByUserInfo(username, password, email);
                    changeUser.Name = textBox1.Text;
                    changeUser.UserSurname = textBox2.Text;
                    changeUser.UserName = textBox3.Text;
                    changeUser.Password = textBox4.Text;
                    changeUser.Email = textBox5.Text;
                    changeUser.CreatedDate = DateTime.Now;
                    changeUser.ModifiedDate = DateTime.Now;
                    changeUser.IsActive = activeStatus;
                    changeUser.Premium = premiumStatus;
                    changeUser.RollID = rol.RolID;
                    userDAL.UpdateUser(changeUser);

                    MessageBox.Show("Kullanıcı Güncellendi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("geçerli bir e-posta giriniz");
                }
            }
            else
            {
                MessageBox.Show("boş alan bırakmayınız");
            }
        }

        private void FormAdminUserUpdate_Load(object sender, EventArgs e)
        {
            var userDAL = new UserDAL();
            var users = userDAL.GetAllUsers();
            RollDAL rollDAL = new RollDAL();
            var rols = rollDAL.GetAllRoles();
            foreach (var item in rols)
            {
                comboBox1.Items.Add(item.RollName);
            }
            dataGridView1.ColumnCount = 9;
            dataGridView1.Columns[0].Name = "Adı";
            dataGridView1.Columns[1].Name = "Soyadı";
            dataGridView1.Columns[2].Name = "Email";
            dataGridView1.Columns[3].Name = "Şifre";
            dataGridView1.Columns[4].Name = "Kullanıcı Adı";
            dataGridView1.Columns[5].Name = "Rolü";
            dataGridView1.Columns[6].Name = "Premium";
            dataGridView1.Columns[7].Name = "Aktif Mi";
            dataGridView1.Columns[8].Name = "Oluşturulma Tarihi";

            foreach (var item in users)
            {
                dataGridView1.Rows.Add(item.Name, item.UserSurname, item.Email, item.Password, item.UserName, item.Rol.RollName, item.Premium == true ? "Evet" : "Hayır", item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
            }
        }

        string username, password, email = null;
        //seçili kullanıcı bilgisini alma
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AdminUserAddDTO adminUserAddDTO = new AdminUserAddDTO();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[5].Value.ToString();
                username = row.Cells[4].Value.ToString();
                password = row.Cells[3].Value.ToString();
                email = row.Cells[2].Value.ToString();
                if (row.Cells[7].Value.ToString() == "Evet")
                {
                    radioButton1.Checked = true;
                }
                if (row.Cells[7].Value.ToString() == "Hayır")
                {
                    radioButton2.Checked = true;
                }
                if (row.Cells[6].Value.ToString() == "Evet")
                {
                    radioButton3.Checked = true;
                }
                if (row.Cells[6].Value.ToString() == "Hayır")
                {
                    radioButton4.Checked = true;
                }
            }
        }

        bool activeStatus = false;
        //aktif durumu
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                activeStatus = true;
            }
            else if (radioButton2.Checked == true)
            {
                activeStatus = false;
            }
        }

        bool premiumStatus = false;
        //premium durumu
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                premiumStatus = true;
            }
            else if (radioButton4.Checked == true)
            {
                premiumStatus = false;
            }
        }
    }
}
