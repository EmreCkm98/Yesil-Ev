using System;
using System.Drawing;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminUpdateProduct : Form
    {
        public FormAdminUpdateProduct()
        {
            InitializeComponent();
        }
        public Guid guid;//yeni oluşturulacak barcode bilgisi
        ComboBox combo;
        public string ProductFrontImage;
        public string ProductBackImage;
        Product CmbSelectedProduct;//comboboxtan kullanıcının seçtiği ürün bilgisi
        private void FormAdminUpdateProduct_Load(object sender, EventArgs e)
        {
            //dbdeki kullanıcının tüm productlarını comboboxa ekliyorum.
            ProductDAL productDAL = new ProductDAL();
            var productList = productDAL.GetAllProduct();
            foreach (var item in productList)
            {
                comboBox1.Items.Add(item.ProductName);
            }
            //dbdeki tüm kategori bilgilerini comboboxa getiriyorum
            CategoryDAL categoryDAL = new CategoryDAL();
            var categoryList = categoryDAL.GetAllCategories();
            foreach (var item in categoryList)
            {
                comboBox3.Items.Add(item.CategoryName);
            }
            //dbdeki tüm content bilgilerini comboboxa getiriyorum
            ContentDAL contentDAL = new ContentDAL();
            var contentList = contentDAL.GetAll();
            foreach (var item in contentList)
            {
                comboBox2.Items.Add(item.ContentName);
            }
            UserDAL userDAL = new UserDAL();
            var users = userDAL.GetAllUsers();
            foreach (var item in users)
            {
                comboBox4.Items.Add(item.UserName);
            }
            //ürün içeriği seçmek için yeni bir combobox olusturdum.dbdeki içerik bilgilerini bu komboboxda gösteriyorum
            combo = new ComboBox();
            combo.Location = new System.Drawing.Point(240, 285);
            combo.Size = new System.Drawing.Size(160, 24);
            combo.Visible = false;
            this.Controls.Add(combo);
        }
        //kullanıcı ürünlernden hangisi guncelleyecekse comboboxtan seciyor ve tüm bilgileri ekrana geliyor.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.Enabled = true;
            comboBox3.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            textBox2.Enabled = true;
            comboBox2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            comboBox4.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            var product = comboBox1.SelectedItem as string;
            //komboboxtan secilen ürünü dbden aldım.
            ProductDAL productDAL = new ProductDAL();
            CmbSelectedProduct = productDAL.SearchProductByName(product);
            ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
            var ingredients = productIngredientDAL.GetAllProductIngredients();
            //secilen kullanıcı ürün bilgilerini diğer controllere aktardım.
            button1.Text = CmbSelectedProduct.Barcode.ToString();
            button2.Text = CmbSelectedProduct.Manufacturer.ManufacturerName;
            textBox1.Text = CmbSelectedProduct.ProductName;
            comboBox3.Text = CmbSelectedProduct.Category.CategoryName;
            comboBox4.Text = CmbSelectedProduct.User.UserName;
            if (CmbSelectedProduct.User.IsActive == true)
            {
                radioButton3.Checked = true;
            }
            if (CmbSelectedProduct.User.IsActive == false)
            {
                radioButton4.Checked = true;
            }
            //foreach (var item in ingredients)
            //{
            //    textBox2.Text = item.IngredientName + "," + item.IngredientDetail;
            //    comboBox2.Text = item.ContentName;
            //}
            button4.Image = Bitmap.FromFile(CmbSelectedProduct.ProductFrontImage);
            button5.Image = Bitmap.FromFile(CmbSelectedProduct.ProductBackImage);

            IngredientDAL ingredientDAL = new IngredientDAL();
            var ingredientList = ingredientDAL.GetAllUserIngredients(CmbSelectedProduct.UserID, product);
            combo.Items.Clear();
            textBox2.Text = String.Empty;
            foreach (var item in ingredientList)
            {
                textBox2.Text += item.IngredientName + "," + item.IngredientContent;
                combo.Items.Add(item.IngredientName);
            }
        }

        bool activeStatus = false;
        //aktif durumu
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                activeStatus = true;
            }
            else if (radioButton4.Checked == true)
            {
                activeStatus = false;
            }
        }
        //ürün ön fotoyu güncelleme butonu
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //product ön yüz image 
                button4.Text = "";
                button4.Image = new Bitmap(openFileDialog.FileName);
                // image file path  
                ProductFrontImage = openFileDialog.FileName;
            }
        }
        //ürün arka fotoyu güncelleme butonu
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //product arka yüz image 
                button5.Text = "";
                button5.Image = new Bitmap(openFileDialog.FileName);

                // image file path  
                ProductBackImage = openFileDialog.FileName;
            }
        }
        //yeni barkod oluşturma butonu
        private void button1_Click(object sender, EventArgs e)
        {
            guid = Guid.NewGuid();
            button1.Text = guid.ToString();
        }
        //yeni üretici oluşturma butonu
        private void button2_Click(object sender, EventArgs e)
        {
            FormManifacture formManifacture = new FormManifacture();
            formManifacture.Show();
        }
        //ürün güncelleme butonu
        private void button3_Click(object sender, EventArgs e)
        {
            //girilen textlerinin boş olmamasını kontrol ediyorum
            if (textBox1.Text != "" && ((radioButton1.Checked == true && textBox2.Text != "") || (radioButton2.Checked == true && combo.SelectedItem != null)) && !(radioButton1.Checked == false && radioButton2.Checked == false) &&
             comboBox3.SelectedItem != null && comboBox2.SelectedItem != null && ProductFrontImage != ""
             && ProductBackImage != "" && guid != null && comboBox3.SelectedItem != null && (radioButton3.Checked == true || radioButton4.Checked == true))
            {
                //secilen ürünün bilgilerini güncelliyorum.
                ProductDAL productDAL = new ProductDAL();
                var productupdate = productDAL.GetProductByID(CmbSelectedProduct.ProductID);
                productupdate.ShowUser = checkBox1.Checked == true ? true : false;
                productDAL.UpdateProduct(productupdate);
                //yeni içerik radiobutonu checkli ise yeni içerik girdiyorum
                if (radioButton1.Checked == true)
                {
                    int countWord = 0;//textboxta ilk girilen kelime ürün içerik adı,2.kelime ise içerik detayı olacak
                    string ingredientName = "", ingredientDetail = "";
                    string[] ingredients = textBox2.Text.Split(',');
                    foreach (var item in ingredients)
                    {
                        if (countWord % 2 == 0)//içerik adı
                        {
                            ingredientName = item;
                            IngredientDAL ingredientDAL = new IngredientDAL();
                            if (ingredientDAL.IngredientAnyExist(ingredientName))//dbde varsa;
                            {
                                var ingredient = ingredientDAL.GetIngredientByFilter(item);
                                ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                                var productingredient = productIngredientDAL.GetProductIngredientByProductID(productupdate.ProductID, ingredient.IngredientID);
                                //ürün içerik tablosunu güncelliyorum
                                productingredient.IngredientID = ingredient.IngredientID;
                                productingredient.ContentID = selectedContent.ContentID;
                                productIngredientDAL.UpdateProductIngredient(productingredient);

                            }
                        }
                        if (countWord % 2 != 0)//içerik detayı
                        {
                            ingredientDetail = item;
                            IngredientDAL ingredientDAL = new IngredientDAL();
                            if (!(ingredientDAL.IngredientAnyExist(ingredientName)))//dbde yoksa;
                            {
                                var selectedContent = comboBox2.SelectedItem as string;
                                ContentDAL contentDAL = new ContentDAL();
                                var contentdb = contentDAL.GetContentByFilter(selectedContent);
                                var content = contentDAL.GetContentByID(contentdb.ContentID);
                                var ingredientdb = ingredientDAL.GetIngredientByFilter(ingredientName);
                                //içerik tablosunu güncelliyorum.
                                IngredientAddDTO ingredientAddDTO = new IngredientAddDTO()
                                {
                                    IngredientName = ingredientName,
                                    IngredientContent = ingredientDetail,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now
                                };
                                var addedIngredient = ingredientDAL.AddIngredient(ingredientAddDTO);
                                ProductIngredientAddDTO productIngredient = new ProductIngredientAddDTO()
                                {
                                    ProductID = productupdate.ProductID,
                                    IngredientID = addedIngredient.IngredientID,
                                    ContentID = content.ContentID,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now
                                };
                                ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                                productIngredientDAL.AddProductIngredient(productIngredient);

                            }
                        }
                        countWord++;
                    }
                }
                //içerik seç radiobutonu checkli ise comboboxtan içerik sectiriyorum
                else if (radioButton2.Checked == true)
                {
                    var selectedIgrendient = combo.SelectedItem as string;
                    IngredientDAL ingredientDAL = new IngredientDAL();
                    var ingredientDb = ingredientDAL.GetIngredientByFilter(selectedIgrendient);
                    ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                    var productingredientdb = productIngredientDAL.GetProductIngredientByIngredientID(ingredientDb.IngredientID);
                    productingredientdb.ProductID = productupdate.ProductID;
                    productingredientdb.IngredientID = ingredientDb.IngredientID;
                    productingredientdb.ContentID = selectedContent.ContentID;
                    productIngredientDAL.UpdateProductIngredient(productingredientdb);
                }
                MessageBox.Show("Yeni Bilgileriniz başarıyla kaydedildi.Onay Bekleniyor..");
                this.Close();
            }
            else
            {
                MessageBox.Show("Boş alanları doldurunuz!!");
            }
        }
        public Category selectedCategory = new Category();
        //kategori comboboxından secilen kategori bilgisini alıyorum.
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedComboCategory = comboBox3.SelectedItem as string;
            CategoryDAL categoryDAL = new CategoryDAL();
            var categoryDb = categoryDAL.GetCategoryByFilter(selectedComboCategory);
            selectedCategory.CategoryID = categoryDb.CategoryID;
            selectedCategory.CategoryName = categoryDb.CategoryName;
        }
        //yeni içerik radio butonu checkli ise ekranda gosterilecek controlleri ayarlıyorum
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selected = sender as RadioButton;
            if (selected.Checked == true && selected.Text == "Yeni İçerik")
            {
                combo.Visible = false;
                label4.Text = "Ürün İçeriği";
                textBox2.Visible = true;
                label4.Visible = true;
            }
            else
            {
                combo.Visible = true;
                textBox2.Visible = false;

                label4.Text = "İçerik Seç";
            }
        }
        public Content selectedContent = new Content();
        //risk seviyesi comboboxından secilen risk bilgisini alıyorum.
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedComboContent = comboBox2.SelectedItem as string;
            ContentDAL contentDAL = new ContentDAL();
            var contentDb = contentDAL.GetContentByFilter(selectedComboContent);
            selectedContent.ContentID = contentDb.ContentID;
            selectedContent.ContentName = contentDb.ContentName;
        }
        public User selectedUser = new User();
        //secili user bilgisi alma
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedComboUser = comboBox4.SelectedItem as string;
            UserDAL userDAL = new UserDAL();
            var userDb = userDAL.GetUserByName(selectedComboUser);
            if (userDb == null)
            {
                MessageBox.Show("Ürün kaydınız bulunamadı.Öncelikle ürün ekleyin!!");
            }
            else
            {
                selectedUser.UserID = userDb.UserID;
                selectedUser.UserName = userDb.UserName;
            }
        }
    }
}
