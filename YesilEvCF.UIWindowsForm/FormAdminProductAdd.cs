﻿using System;
using System.Drawing;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminProductAdd : Form
    {
        public FormAdminProductAdd()
        {
            InitializeComponent();
        }
        ComboBox combo;
        public Guid _id;
        public Guid TrackNumber;
        public string ProductFrontImage;
        public string ProductBackImage;
        //girilen ürünü oluşturma butonu
        private void button3_Click(object sender, EventArgs e)
        {
            //girilen textlerinin boş olmamasını kontrol ediyorum
            if (textBox1.Text != "" && ((radioButton1.Checked == true && textBox2.Text != "") || (radioButton2.Checked == true && combo.SelectedItem != null)) && !(radioButton1.Checked == false && radioButton2.Checked == false) &&
                comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && ProductFrontImage != null
                && ProductBackImage != null && _id != null && comboBox3.SelectedItem != null && (radioButton3.Checked == true || radioButton4.Checked == true))
            {
                //ürün oluşturuyorum
                ProductAddDTO product = new ProductAddDTO()
                {
                    ProductName = textBox1.Text,
                    Barcode = _id,
                    ManufacturerID = FormManifacture.addedManufacture.ManufacturerID,
                    ProductFrontImage = ProductFrontImage,
                    ProductBackImage = ProductBackImage,
                    CategoryID = selectedCategory.CategoryID,
                    UserID = selectedUser.UserID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = activeStatus,
                    ShowUser = checkBox1.Checked == true ? true : false,
                };
                ProductDAL productDAL = new ProductDAL();
                var addedProduct = productDAL.AddProduct(product);

                ProductStatusDAL productStatusDAL = new ProductStatusDAL();
                TrackNumber = Guid.NewGuid();
                ProductStatus productStatus = new ProductStatus()
                {
                    ApprovementStatusID = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    TrackingNumber = TrackNumber,
                    Detail = "Onay Bekleniyor...",
                    ProductID = addedProduct.ProductID
                };
                var addedProductStatus = productStatusDAL.AddProductStatus(productStatus);
                if (radioButton1.Checked == true)//yeni içerik radiobutonu checkli ise yeni içerik girdiyorum
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
                            ContentDAL contentDAL = new ContentDAL();

                            if (ingredientDAL.IngredientAnyExist(ingredientName))//dbde varsa;
                            {
                                var ingredient = ingredientDAL.GetIngredientByFilter(item);
                                //ürün içerik tablosunu dolduruyorum
                                ProductIngredientAddDTO productIngredient = new ProductIngredientAddDTO()
                                {
                                    ProductID = addedProduct.ProductID,
                                    IngredientID = ingredient.IngredientID,
                                    ContentID = selectedContent.ContentID,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now
                                };
                                ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                                productIngredientDAL.AddProductIngredient(productIngredient);
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
                                //içerik tablosunu dolduruyorum.
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
                                    ProductID = addedProduct.ProductID,
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
                else if (radioButton2.Checked == true)//içerik seç radiobutonu checkli ise comboboxtan içerik sectiriyorum
                {
                    var selectedIgrendient = combo.SelectedItem as string;
                    IngredientDAL ingredientDAL = new IngredientDAL();
                    var ingredientDb = ingredientDAL.GetIngredientByFilter(selectedIgrendient);
                    ProductIngredientAddDTO productIngredient = new ProductIngredientAddDTO()
                    {
                        ProductID = addedProduct.ProductID,
                        IngredientID = ingredientDb.IngredientID,
                        ContentID = selectedContent.ContentID,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                    productIngredientDAL.AddProductIngredient(productIngredient);
                }
                MessageBox.Show("Bilgileriniz başarıyla kaydedildi.Onay Bekleniyor..");
                this.Close();
            }
            else
            {
                MessageBox.Show("Boş alanları doldurunuz!!");
            }
        }

        private void FormAdminProductAdd_Load(object sender, EventArgs e)
        {
            CategoryDAL categoryDAL = new CategoryDAL();
            var categories = categoryDAL.GetAllCategories();
            foreach (var item in categories)//comboboxa dbdeki kategori bilgilerini getiriyorum.
            {
                comboBox1.Items.Add(item.CategoryName);
            }
            //ürün içeriği seçmek için yeni bir combobox olusturdum.dbdeki içerik bilgilerini bu komboboxda gösteriyorum
            combo = new ComboBox();
            combo.Location = new System.Drawing.Point(235, 250);
            combo.Size = new System.Drawing.Size(160, 24);
            combo.Visible = false;
            this.Controls.Add(combo);

            IngredientDAL ingredientDAL = new IngredientDAL();
            var ingredients = ingredientDAL.GetAllIngredients();
            foreach (var item in ingredients)
            {
                combo.Items.Add(item.IngredientName);
            }
            //risk seviyesi comboboxuna dbdeki risk seviyeleri verilerni getiriyorum
            ContentDAL contentDAL = new ContentDAL();
            var contents = contentDAL.GetAllContents();
            foreach (var item in contents)
            {
                comboBox2.Items.Add(item.ContentName);
            }
            UserDAL userDAL = new UserDAL();
            var users = userDAL.GetAllUsers();
            foreach (var item in users)
            {
                comboBox3.Items.Add(item.UserName);
            }
        }
        //barkod no olusturma butonu
        //eğer barcode okuma ekranından gelen bir barcode varsa bu barcode nosunu kullanıyorum.yoksa;
        //butona tıklanınca yeni bir barcode olusturuyorum.
        private void button6_Click(object sender, EventArgs e)
        {
            button6 = (Button)sender;
            _id = Guid.NewGuid();
            button6.Text = _id.ToString();
        }

        //üretici bilgilerini oluşturma butonu
        private void button2_Click(object sender, EventArgs e)
        {
            FormManifacture formManifacture = new FormManifacture();
            formManifacture.Show();
        }

        public Category selectedCategory = new Category();
        //kategori comboboxından secilen kategori bilgisini alıyorum.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedComboCategory = comboBox1.SelectedItem as string;
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

        //ürünün ön yüz fotosunu alan buton
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
        //ürünün arka yüz fotosunu alan buton
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //product arka yüz image 
                button1.Text = "";
                button1.Image = new Bitmap(openFileDialog.FileName);

                // image file path  
                ProductBackImage = openFileDialog.FileName;
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
        public User selectedUser = new User();
        //secili user bilgisi alma
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedComboUser = comboBox3.SelectedItem as string;
            UserDAL userDAL = new UserDAL();
            var userDb = userDAL.GetUserByName(selectedComboUser);
            selectedUser.UserID = userDb.UserID;
            selectedUser.UserName = userDb.UserName;
        }
    }
}
