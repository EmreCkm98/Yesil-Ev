using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormManifacture : Form
    {
        public static Manufacturer addedManufacture;//eklenen üretici bilgisini tutuyorum.
        public FormManifacture()
        {
            addedManufacture = new Manufacturer();
            InitializeComponent();
        }

        //üretici ekleme butonu
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || maskedTextBox1.Text != "" || textBox3.Text != "")
            {
                var c = maskedTextBox1.Text.Length;
                //üreticiyi ekliyorum
                Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match matchEmail = regexEmail.Match(textBox3.Text);
                Regex regexPhone = new Regex(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");
                Match matchPhone = regexPhone.Match(maskedTextBox1.Text);
                if (matchEmail.Success && matchPhone.Success)
                {
                    ManifactureAddDTO manifactureAddDTO = new ManifactureAddDTO()
                    {
                        ManufacturerName = textBox1.Text,
                        Phone = maskedTextBox1.Text,
                        Email = textBox3.Text,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    ManifactureDAL manifactureDAL = new ManifactureDAL();
                    manifactureDAL.AddManufacture(manifactureAddDTO);
                    ManufacturerDoldur();
                    this.Close();
                }
                else
                {
                    if (!matchEmail.Success)
                    {
                        MessageBox.Show("geçerli bir e-posta giriniz");
                    }
                    else
                    {
                        MessageBox.Show("geçerli bir telefon giriniz");
                    }

                }

            }
            else
            {
                MessageBox.Show("boş alan bırakmayınız");
            }
        }
        //listboxa var olan üreticileri listeliyorum
        void ManufacturerDoldur()
        {
            dataGridView1.Rows.Clear();
            ManifactureDAL manifactureDAL = new ManifactureDAL();
            var manifactures = manifactureDAL.GetAll();
            foreach (var item in manifactures)
            {
                dataGridView1.Rows.Add(item.ManufacturerName, item.Phone, item.Email);
            }
        }

        private void FormManifacture_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Şirket Adı";
            dataGridView1.Columns[1].Name = "Telefonu";
            dataGridView1.Columns[2].Name = "E-Postası";
            ManufacturerDoldur();
        }

        //şirket sec butonu
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şirket seçildi");
            this.Close();
        }

        //datagridviewden seçilen şrket bilgisini alıyorum
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ManifactureDAL manifactureDAL = new ManifactureDAL();
            string manifactureName = "";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                manifactureName = row.Cells[0].Value.ToString();
            }
            addedManufacture = manifactureDAL.GetManifactureByFilter(manifactureName);
        }

        //ana ekrana gitme butonu
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
            this.Close();
        }

        //kullanıcı profiline giden buton
        private void button5_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.Show();
        }
    }
}
