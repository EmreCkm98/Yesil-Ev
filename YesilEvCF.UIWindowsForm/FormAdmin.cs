using System;
using System.Windows.Forms;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }
        bool userstat = false;
        //kullanıcı
        private void button1_Click(object sender, EventArgs e)
        {
            userstat = !userstat;
            if (userstat)
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        //kullanıcı ekle
        private void button5_Click(object sender, EventArgs e)
        {
            FormAdminUserAdd formAdminUserAdd = new FormAdminUserAdd();
            formAdminUserAdd.Show();

        }

        //kullanıcı güncelle
        private void button4_Click(object sender, EventArgs e)
        {
            FormAdminUserUpdate formAdminUserUpdate = new FormAdminUserUpdate();
            formAdminUserUpdate.Show();
        }

        //kullanıcı silme
        private void button3_Click(object sender, EventArgs e)
        {
            FormAdminUserDelete formAdminUserDelete = new FormAdminUserDelete();
            formAdminUserDelete.Show();
        }

        bool productstat = false;
        //product
        private void button2_Click(object sender, EventArgs e)
        {
            productstat = !productstat;
            if (productstat)
            {
                panel3.Visible = true;
            }
            else
            {
                panel3.Visible = false;
            }
        }
        //ürün güncelleme
        private void button7_Click(object sender, EventArgs e)
        {
            FormAdminUpdateProduct formAdminUpdateProduct = new FormAdminUpdateProduct();
            formAdminUpdateProduct.Show();
        }

        //ürün ekleme
        private void button6_Click(object sender, EventArgs e)
        {
            FormAdminProductAdd formAdminProductAdd = new FormAdminProductAdd();
            formAdminProductAdd.Show();
        }
        //ürün sil
        private void button8_Click(object sender, EventArgs e)
        {
            FormAdminProductDelete formAdminProductDelete = new FormAdminProductDelete();
            formAdminProductDelete.Show();
        }
        //kullanıcı listeleme
        private void button9_Click(object sender, EventArgs e)
        {
            FormAdminUserList formAdminUserList = new FormAdminUserList();
            formAdminUserList.Show();
        }

        //ürün listeleme
        private void button10_Click(object sender, EventArgs e)
        {
            FormAdminProductList formAdminProductList = new FormAdminProductList();
            formAdminProductList.Show();
        }
    }
}
