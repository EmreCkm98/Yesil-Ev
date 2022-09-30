using System;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormProductBarcode : Form
    {
        public FormProductBarcode()
        {
            InitializeComponent();
        }

        //barkod arama butonu
        private void button1_Click(object sender, EventArgs e)
        {
            var productDAL = new ProductDAL();
            if (textBox1.Text != "")//textbox boş olmama kontrolü{
            {
                if (Guid.TryParse(textBox1.Text, out _))
                {
                    Guid id = new Guid(textBox1.Text);
                    if (id != Guid.Empty)
                    {
                        var product = productDAL.BarcodeSearch(id);
                        if (product != null)//eğer dbde girilen barcode bulunursa bu ürünü urun sayfasına yolluyorum
                        {
                            FormProduct formProduct = new FormProduct(id);
                            formProduct.Show();
                            this.Close();
                        }
                        else
                        {//eğer dbde girilen barcode bulunmazsa bu barcode noyu ürün ekleme ekranına  yolluyorum
                            FormProductAdd yeniUrun = new FormProductAdd(id);
                            yeniUrun.Show();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir barkod giriniz");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir barkod giriniz");
                }


            }
            else
            {
                MessageBox.Show("Lütfen aranacak barkodu giriniz");
            }
        }

        //ana ekrana gitme butonu
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
            this.Close();
        }

        //kullanıcı profil ekranına gider
        private void button5_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.Show();
            this.Close();
        }
    }
}
