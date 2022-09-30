using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        //kayıt olma butonu
        private void button1_Click(object sender, EventArgs e)
        {
            var userDAL = new UserDAL();
            //texboxlarının boş olmaması durum kontrolu
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(textBox5.Text);
                if (match.Success)
                {
                    //yeni kullanıcı ekliyorum.
                    userDAL.Register(new RegisterDTO
                    {
                        Name = textBox1.Text,
                        UserSurname = textBox2.Text,
                        UserName = textBox3.Text,
                        Password = textBox4.Text,
                        Email = textBox5.Text
                    });
                    MessageBox.Show("Tebrikler! Kayıt oldunuz");
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
    }
}
