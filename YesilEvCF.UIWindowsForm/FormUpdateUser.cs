using System;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormUpdateUser : Form
    {
        public User _user;
        public FormUpdateUser()
        {
            InitializeComponent();
        }
        public FormUpdateUser(User user) : this()
        {
            _user = user;
        }
        //kullanıcı bilgileri güncelleme
        private void button1_Click(object sender, EventArgs e)
        {
            //textboxlar boş ? check
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                UserDAL userDAL = new UserDAL();
                var user = userDAL.GetUserByID(_user.UserID);
                //aynı user bilgileriyle güncelleme yapmayı engelleme
                if (user.Name != textBox1.Text || user.UserSurname != textBox2.Text || user.UserName != textBox3.Text)
                {
                    user.Name = textBox1.Text;
                    user.UserSurname = textBox2.Text;
                    user.UserName = textBox3.Text;
                    userDAL.UpdateUser(user);
                    MessageBox.Show("Bilgileriz başarıyla güncellendi!");
                }
                else
                {
                    MessageBox.Show("Aynı kayıttan zaten var!!");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bilgileri eksiksiz giriniz");
            }
        }

        private void FormUpdateUser_Load(object sender, EventArgs e)
        {
            textBox1.Text = _user.Name;
            textBox2.Text = _user.UserSurname;
            textBox3.Text = _user.UserName;
        }

        //ana ekrana gitme butonu
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FormMain anaEkran = new FormMain();
            anaEkran.Show();
            this.Close();
        }
        //kullanıcı profiline giden buton
        private void button6_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.Show();
            this.Close();
        }
    }
}
