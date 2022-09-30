using System;
using System.Windows.Forms;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        //barkod ekranına gider
        private void button1_Click(object sender, EventArgs e)
        {
            FormProductBarcode formProductBarcode = new FormProductBarcode();
            formProductBarcode.Show();
            this.Close();
        }

        //arama ekranına gider
        private void button2_Click(object sender, EventArgs e)
        {
            FormSearch formSearch = new FormSearch();
            formSearch.Show();
            this.Close();
        }

        //yeni ürün ekleme ekranına gider
        private void button3_Click(object sender, EventArgs e)
        {
            FormProductAdd formProductAdd = new FormProductAdd();
            formProductAdd.Show();

        }

        //rapor ekranına gider
        private void button6_Click(object sender, EventArgs e)
        {
            FormReport formReport = new FormReport();
            formReport.Show();
            this.Close();
        }

        //arama geçmişi ekranına gider
        private void button4_Click(object sender, EventArgs e)
        {
            FormSearchHistory formSearchHistory = new FormSearchHistory();
            formSearchHistory.Show();
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
