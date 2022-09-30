using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormUserProfile : Form
    {
        public FormUserProfile()
        {
            InitializeComponent();
        }

        private void FormUserProfile_Load(object sender, EventArgs e)
        {
            label24.Text = FormLogin.userLogin.UserName;
            string date = FormLogin.userLogin.CreatedDate.ToString();

            string[] datePart = date.Substring(0, 10).Split('.');
            if (datePart[0].StartsWith("0"))
            {
                datePart[0] = datePart[0].Substring(1, 1);
            }
            if (datePart[1].StartsWith("0"))
            {
                datePart[1] = datePart[1].Substring(1, 1);
            }
            var monthEn = CultureInfo.CreateSpecificCulture("tr-TR").DateTimeFormat.GetMonthName(Int32.Parse(datePart[1]));
            label2.Text = datePart[0] + " " + monthEn + " " + datePart[2];

            ProductDAL productDAL = new ProductDAL();
            var productCount = productDAL.GetUsersProductCount(FormLogin.userLogin.UserID);
            button1.Text = productCount.ToString();
            if (FormLogin.userLogin.Premium == true)
            {
                pictureBox1.Visible = true;
            }
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Ürün Adı";
            dataGridView1.Columns[1].Name = "İçerik Adı";
            dataGridView1.Columns[2].Name = "İçerik Detayı";
            dataGridView1.Columns[3].Name = "Risk Durumu";
        }

        //kullanıcının eklediği ürün sayısına tıklayınca listviewde ürünleri gösterme
        private void button1_Click(object sender, EventArgs e)
        {
            ProductDAL productDAL = new ProductDAL();
            var userProduct = productDAL.GetAllUserProductDetail(FormLogin.userLogin.UserID);
            dataGridView1.Visible = true;
            foreach (var item in userProduct)
            {
                dataGridView1.Rows.Add(item.ProductName, item.IngredientName, item.IngredientDetail, item.ContentName);
            }
            button1.Enabled = false;
        }

        //çıkış butonu
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Close();
        }

        //arama geçmişi ekranına gider
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FormSearchHistory aramaGecmisi = new FormSearchHistory();
            aramaGecmisi.Show();
            this.Close();
        }
        //favori ekranına gider
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            FormSearchHistory aramaGecmisi = new FormSearchHistory();
            aramaGecmisi.Show();
            this.Close();
        }

        //kullanıcı güncelleme butonu
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            FormUpdateUser userGuncelle = new FormUpdateUser(FormLogin.userLogin);
            userGuncelle.Show();
        }

        //şifre güncelleme butonu
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            FormUpdatePassword sifreGuncelle = new FormUpdatePassword(FormLogin.userLogin);
            sifreGuncelle.Show();
        }

        //eposta güncelleme butonu
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FormUpdateEmail epostaGuncelle = new FormUpdateEmail(FormLogin.userLogin);
            epostaGuncelle.Show();
        }

        //karalisteye ekleme butonu
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            FormBlackList karaListe = new FormBlackList(FormLogin.userLogin);
            karaListe.Show();
        }

        //ana ekrana gitme butonu
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FormMain anaEkran = new FormMain();
            anaEkran.Show();
            this.Close();
        }

        int oldRowIndex = 0;
        //data grid view seçili satırın rengini değiştirme
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int currentRowIndex = (sender as DataGridView).CurrentRow.Index;
            if (currentRowIndex != oldRowIndex)
            {
                dataGridView1.Rows[oldRowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            dataGridView1.Rows[currentRowIndex].DefaultCellStyle.SelectionBackColor = Color.LightBlue;

            oldRowIndex = currentRowIndex;
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
