using System;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        public static User userLogin;

        //login olma sayfası
        private void button1_Click(object sender, EventArgs e)
        {
            var userDAL = new UserDAL();
            if (textBox1.Text != "" || textBox2.Text != "")//texboxlarının boş olmama durumu
            {
                var user = userDAL.Login(new LoginDTO
                {
                    UserName = textBox1.Text,
                    Password = textBox2.Text,
                });
                if (user != null)
                {
                    if (user.RollID == 2)
                    {
                        FormAdmin formAdmin = new FormAdmin();
                        formAdmin.Show();
                        this.Hide();
                    }
                    else
                    {
                        userLogin = user;
                        FormMain formMain = new FormMain();
                        formMain.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("girdiğiniz bilgilerde bir kayıt bulunamadı");
                }
            }
            else
            {
                MessageBox.Show("boş alan bırakmayınız");
            }
        }

        //kayıt ekranına giden buton
        private void button2_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.Show();
        }

        //şifreyi görünür yapma
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = char.MinValue;
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

    }
}
