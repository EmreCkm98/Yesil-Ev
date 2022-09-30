using System;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormUpdatePassword : Form
    {
        public User _user;//kullanıcı bilgisi tutuyorum
        public FormUpdatePassword()
        {
            InitializeComponent();
        }
        public FormUpdatePassword(User user) : this()
        {
            _user = user;
        }

        //sifre guncelleme butonu
        private void button1_Click(object sender, EventArgs e)
        {
            var user = new User();
            //texboxlarının boş olmama kontrolu
            if (maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && maskedTextBox3.Text != "")
            {
                UserDAL userDAL = new UserDAL();
                user = userDAL.GetUserByID(_user.UserID);
                //eski şifrenin yeni şifreden farklı olması ve yeni şifreyle yeni şifre tekrarrın aynı olması kontrolu
                if (user.Password.ToString() == maskedTextBox1.Text && (maskedTextBox2.Text == maskedTextBox3.Text && (maskedTextBox2.Text != maskedTextBox1.Text || maskedTextBox3.Text != maskedTextBox1.Text)))
                {
                    //yeni şifreyi ekliyorum
                    user = userDAL.GetUserByID(_user.UserID);
                    user.Password = maskedTextBox3.Text;
                    userDAL.UpdateUser(user);
                    MessageBox.Show("Şifreniz başarıyla kaydedildi");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Eski şifrenizle yeni şifreniz aynı olamaz");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bilgileri eksiksiz giriniz");
            }
        }

        //ana ekrana gitme butonu
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FormMain anaEkran = new FormMain();
            anaEkran.Show();
            this.Close();
        }

        //kullanıcı profiline gitme butonu
        private void button5_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.Show();
            this.Close();
        }
    }
}
