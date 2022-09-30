using System;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormUpdateEmail : Form
    {
        public User _user;//kullanıcı bilgilerini tutuyorum
        public FormUpdateEmail()
        {
            InitializeComponent();
        }
        public FormUpdateEmail(User user) : this()
        {
            _user = user;
        }

        //
        private void button1_Click(object sender, EventArgs e)
        {
            var user = new User();
            if (textBox1.Text != "")//textbox boş değilse kullanıcın email bilgisini güncelliyorum
            {
                UserDAL userDAL = new UserDAL();
                user = userDAL.GetUserByID(_user.UserID);
                if (user.Email != textBox1.Text)
                {
                    user.Email = textBox1.Text;
                    userDAL.UpdateUser(user);
                    MessageBox.Show("E-postanız değiştirildi!");
                }
                else
                {
                    MessageBox.Show("E-postanız zaten kayıtlı!");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bilgileri eksiksiz giriniz");
            }
        }

    }
}
