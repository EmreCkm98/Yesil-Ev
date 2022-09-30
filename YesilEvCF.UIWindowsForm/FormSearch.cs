using System;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            CategoryDAL categoryDAL = new CategoryDAL();
            var categoryList = categoryDAL.GetAllCategories();
            //comnoboxa dbdeki kategorileri getiriyorum.
            foreach (var item in categoryList)
            {
                comboBox1.Items.Add(item.CategoryName);
            }
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Ürün Adı";
            dataGridView1.Columns[1].Name = "Barcode Numarası";
        }

        //barkoda ya da ürün adına göre arama butonu
        private void button1_Click(object sender, EventArgs e)
        {
            SearchHistory searchHistory = null;
            if (textBox1.Text != "" && radioButton1.Checked == true)//ürün arama şartı
            {

                dataGridView1.Rows.Clear();
                ProductDAL productDAL = new ProductDAL();
                //girilen adda ürün varmı bakıyorum.varsa lisboxa yazdırıyorum
                //aynı zamanda aranan bu ürünü aramageçmişi tablosuna yazıyorum
                var productList = productDAL.SearchProductName(textBox1.Text);

                if (productList != null)
                {
                    foreach (var item in productList)
                    {
                        dataGridView1.Rows.Add(item.ProductName, item.Barcode);
                        productDAL.AddSearchHistory(new SearchHistory
                        {
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            SearchDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ProductID = item.ProductID
                        });
                    }
                }
                else
                {
                    MessageBox.Show("aradığınız adda ürün bulunamadı");
                }
            }
            else if (textBox1.Text != "" && radioButton2.Checked == true)//barkod arama şartı
            {

                dataGridView1.Rows.Clear();
                ProductDAL productDAL = new ProductDAL();
                //girilen barkoda göre ürün varmı bakıyorum.varsa lisboxa yazdırıyorum
                //aynı zamanda aranan bu ürünü aramageçmişi tablosuna yazıyorum
                var productList = productDAL.SearchProductBarcodeName(textBox1.Text);

                if (productList != null)
                {
                    foreach (var item in productList)
                    {
                        dataGridView1.Rows.Add(item.ProductName, item.Barcode);
                        productDAL.AddSearchHistory(new SearchHistory
                        {
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            SearchDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ProductID = item.ProductID
                        });
                    }
                }
                else
                {
                    MessageBox.Show("aradığınız barkoda göre ürün bulunamadı");
                }
            }
            else
            {
                MessageBox.Show("Aranacak kelime giriniz");
            }
        }
        //datagridviewden seçili ürünü alıyorum.seçili ürün bilgilerini ürün sayfasına yolluyorum
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string productName, barcode;
            ProductListDTO productDTO = new ProductListDTO();
            ProductDAL productDAL = new ProductDAL();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                productName = row.Cells[0].Value.ToString();
                barcode = row.Cells[1].Value.ToString();
                var product = productDAL.SearchProductBarcodeName(barcode);
                foreach (var item in product)
                {
                    productDTO = new ProductListDTO()
                    {
                        ProductName = item.ProductName,
                        Barcode = item.Barcode,
                        ShowUser = item.ShowUser,
                        ProductBackImage = item.ProductBackImage,
                        ProductFrontImage = item.ProductFrontImage,
                        ProductID = item.ProductID,
                    };
                }

            }

            FormProduct formProduct = new FormProduct(productDTO);
            formProduct.Show();
        }

        //comboboxtan seçili kategoriyi alıyorum.bu kategoride olan ürünler getiriyorum
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = comboBox1.SelectedItem as string;
            dataGridView1.Rows.Clear();
            ProductDAL productDAL = new ProductDAL();
            var productList = productDAL.GetProductFromCategory(category);
            if (productList != null)
            {
                foreach (var item in productList)
                {
                    dataGridView1.Rows.Add(item.ProductName, item.Barcode);
                }
            }
            else
            {
                MessageBox.Show("Seçtiğiniz kategoride ürün bulunamadı");
            }
        }

        //ana ekrana gtme butonu
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
            this.Close();
        }

        //kullanıcı profili ekranına gitme butonu
        private void button5_Click(object sender, EventArgs e)
        {
            FormUserProfile userProfile = new FormUserProfile();
            userProfile.Show();
            this.Close();
        }
    }
}
