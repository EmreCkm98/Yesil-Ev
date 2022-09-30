using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminUserAdd : Form
    {
        public FormAdminUserAdd()
        {
            InitializeComponent();
        }

        //kullanıcı oluşturma
        private void button1_Click(object sender, EventArgs e)
        {
            var userDAL = new UserDAL();
            //texboxlarının boş olmaması durum kontrolu
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.SelectedItem != null && (radioButton1.Checked == true || radioButton2.Checked == true) && (radioButton3.Checked == true || radioButton4.Checked == true))
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(textBox5.Text);
                if (match.Success)
                {
                    RollDAL rollDAL = new RollDAL();
                    var selectedRol = comboBox1.SelectedItem as string;
                    var rol = rollDAL.GetRoleByName(selectedRol);
                    //yeni kullanıcı ekliyorum.
                    userDAL.AdminRegister(new AdminUserAddDTO
                    {
                        Name = textBox1.Text,
                        UserSurname = textBox2.Text,
                        UserName = textBox3.Text,
                        Password = textBox4.Text,
                        Email = textBox5.Text,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = activeStatus,
                        Premium = premiumStatus,
                        RollID = rol.RolID
                    });
                    MessageBox.Show("Kullanıcı Oluşturuldu");
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

        private void FormAdminUserAdd_Load(object sender, EventArgs e)
        {
            RollDAL rollDAL = new RollDAL();
            var rols = rollDAL.GetAllRoles();
            foreach (var item in rols)
            {
                comboBox1.Items.Add(item.RollName);
            }
            radioButton1.Checked = true;
            radioButton4.Checked = true;
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
