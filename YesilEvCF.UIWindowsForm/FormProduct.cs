using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormProduct : Form
    {
        public Guid _guid;
        public Product _product;
        public ProductListDTO _productDTO;
        public FormProduct()
        {
            InitializeComponent();
        }
        public FormProduct(Guid id) : this()
        {
            _guid = id;
        }
        public FormProduct(Product product) : this()
        {
            _product = product;
        }
        public FormProduct(ProductListDTO product) : this()
        {
            _productDTO = product;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                ProductDAL productDAL = new ProductDAL();
                var productguid = productDAL.BarcodeSearch(_guid);
                if (_guid != Guid.Empty)
                {
                    pictureBox1.BackgroundImage = Image.FromFile(productguid.ProductFrontImage);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                if (_productDTO != null)
                {
                    pictureBox1.BackgroundImage = Image.FromFile(_productDTO.ProductFrontImage);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else if (radioButton2.Checked == true)
            {
                ProductDAL productDAL = new ProductDAL();
                var productguid = productDAL.BarcodeSearch(_guid);
                if (_guid != Guid.Empty)
                {
                    pictureBox1.BackgroundImage = Image.FromFile(productguid.ProductBackImage);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                if (_productDTO != null)
                {
                    pictureBox1.BackgroundImage = Image.FromFile(_productDTO.ProductBackImage);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (_guid != Guid.Empty || _productDTO != null)
            {
                var userp = new Product();
                List<ProductDetailDTO> userProduct = new List<ProductDetailDTO>();
                List<ContentCountDTO> userContentCount = new List<ContentCountDTO>();
                List<IngredientDTO> userProductIngredient = new List<IngredientDTO>();
                if (_guid != Guid.Empty)
                {
                    ProductDAL productDAL = new ProductDAL();
                    userp = productDAL.GetProductByBarcode(_guid);
                    userProduct = productDAL.GetAllUserProductCategory(FormLogin.userLogin.UserID, userp.ProductID);
                    foreach (var item in userProduct)
                    {
                        label1.Text = item.CategoryName;
                        label2.Text = item.ManufactureName;
                        label3.Text = item.ProductName;
                    }
                    ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                    userContentCount = productIngredientDAL.GetAllUserProductIngredientsContentCount(FormLogin.userLogin.UserID, userp.ProductID);

                    userProductIngredient = productIngredientDAL.GetAllUserProductIngredients(FormLogin.userLogin.UserID, userp.ProductID);
                    if (userp.ShowUser == true)
                    {
                        label14.Text = "Görüntülediğiniz ürün bilgileri <<" + FormLogin.userLogin.Name + ">> isimli üyemiz tarafından sağlanmıştır.";
                    }


                }
                else if (_productDTO != null)
                {
                    ProductDAL productDAL = new ProductDAL();
                    userp = productDAL.GetProductByBarcode(_guid);
                    userProduct = productDAL.GetAllUserProductCategory(FormLogin.userLogin.UserID, _productDTO.ProductID);
                    foreach (var item in userProduct)
                    {
                        label1.Text = item.CategoryName;
                        label2.Text = item.ManufactureName;
                        label3.Text = item.ProductName;
                    }
                    ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                    userContentCount = productIngredientDAL.GetAllUserProductIngredientsContentCount(FormLogin.userLogin.UserID, _productDTO.ProductID);

                    userProductIngredient = productIngredientDAL.GetAllUserProductIngredients(FormLogin.userLogin.UserID, _productDTO.ProductID);
                    if (_productDTO.ShowUser == true)
                    {
                        label14.Text = "Görüntülediğiniz ürün bilgileri <<" + FormLogin.userLogin.Name + ">> isimli üyemiz tarafından sağlanmıştır.";
                    }
                }

                //groupbydan kurtar.
                foreach (var item in userContentCount)
                {
                    if (item.ContentName == "Kara Liste İçerik")
                    {
                        label9.Text = item.Count.ToString();
                    }
                    else if (item.ContentName == "Riskli İçerik")
                    {
                        label10.Text = item.Count.ToString();
                    }
                    else if (item.ContentName == "Orta Riskli İçerik")
                    {
                        label11.Text = item.Count.ToString();
                    }
                    else if (item.ContentName == "Az Riskli İçerik")
                    {
                        label12.Text = item.Count.ToString();
                    }
                    else if (item.ContentName == "Temiz İçerik")
                    {
                        label13.Text = item.Count.ToString();
                    }
                }
                int count = 30;
                int yCizgi = 0;
                int yText = 0;
                foreach (var item in userProductIngredient)
                {
                    Label label = new Label();
                    Label label2 = new Label();
                    label.Name = "label" + count;
                    label.BackColor = Color.Transparent;
                    label2.Name = "label" + (count + 1);
                    label2.BackColor = Color.Transparent;
                    label2.Click += Label2_Click;
                    label.Location = new Point(87, 360 + yCizgi);
                    label2.Location = new Point(87, 380 + yText);
                    if (item.ContentName == "Kara Liste İçerik")
                    {
                        label2.Text = item.IngredientName;
                        label.Size = new Size(280, 16);
                        label.Text = "--------------------------------------------------------------------------------------------------------------------------------------";
                        label2.ForeColor = Color.Black;
                    }
                    else if (item.ContentName == "Riskli İçerik")
                    {
                        label2.Text = item.IngredientName;
                        label.Size = new Size(280, 16);
                        label.Text = "----------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                        label2.ForeColor = Color.Red;
                    }
                    else if (item.ContentName == "Orta Riskli İçerik")
                    {
                        label2.Text = item.IngredientName;
                        label.Size = new Size(280, 16);
                        label.Text = "----------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                        label2.ForeColor = Color.Orange;
                    }
                    else if (item.ContentName == "Az Riskli İçerik")
                    {
                        label2.Text = item.IngredientName;
                        label.Size = new Size(280, 16);
                        label.Text = "----------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                        label2.ForeColor = Color.YellowGreen;
                    }
                    else if (item.ContentName == "Temiz İçerik")
                    {
                        label2.Text = item.IngredientName;
                        label.Size = new Size(280, 16);
                        label.Text = "----------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                        label2.ForeColor = Color.Green;
                    }
                    this.Controls.Add(label);
                    this.Controls.Add(label2);
                    yText += 39;
                    yCizgi += 39;
                    count += 2;
                }
                FavoriteDAL favoriteDAL = new FavoriteDAL();
                var favorites = favoriteDAL.GetAllFavorites();
                foreach (var item in favorites)
                {
                    comboBox1.Items.Add(item.FavoriteName);
                }
            }

        }
        private void Label2_Click(object sender, EventArgs e)
        {
            var selectedIngredient = sender as Label;

            if (_guid != Guid.Empty)
            {
                FormProductIngredient ingredientDetay = new FormProductIngredient(_guid);
                ingredientDetay.Show();
            }
            else if (_productDTO != null)
            {
                FormProductIngredient ingredientDetay = new FormProductIngredient(selectedIngredient);
                ingredientDetay.Show();
            }
        }

        //ana ekrana giden buton
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
            this.Close();
        }

        //kullanıcı profiline giden buton
        private void button5_Click(object sender, EventArgs e)
        {
            FormUserProfile formMain = new FormUserProfile();
            formMain.Show();
            this.Close();
        }

        //ürünü favoriye ekleme
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            radioButton3.Visible = true;
            radioButton4.Visible = true;
        }

        //ürünü favoriye ekleme
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true && textBox1.Text != "")
            {
                FavoriteDAL favoriteDAL = new FavoriteDAL();
                FavoriteDTO favoriteName = new FavoriteDTO()
                {
                    FavoriteName = textBox1.Text,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                var addedFavorite = favoriteDAL.AddFavorite(favoriteName);
                UserFavoriteDTO userFavoriteDTO = new UserFavoriteDTO()
                {
                    UserID = FormLogin.userLogin.UserID,
                    FavoriteID = addedFavorite.FavoriteID,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ModifiedDate = DateTime.Now,
                    ProductID = _productDTO.ProductID,
                };
                favoriteDAL.AddUserFavorite(userFavoriteDTO);
                MessageBox.Show("Ürün favorilerinize eklendi");
                textBox1.Text = String.Empty;
            }
            else if (radioButton4.Checked == true && comboBox1.SelectedItem != null)
            {
                var comboSelected = comboBox1.SelectedItem as string;
                FavoriteDAL favoriteDAL = new FavoriteDAL();
                var favoriteNameDb = favoriteDAL.GetFavoriteByName(comboSelected);
                UserFavoriteDTO userFavoriteDTO = new UserFavoriteDTO()
                {
                    UserID = FormLogin.userLogin.UserID,
                    FavoriteID = favoriteNameDb.FavoriteID,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ModifiedDate = DateTime.Now,
                    ProductID = _productDTO.ProductID,
                };
                favoriteDAL.AddUserFavorite(userFavoriteDTO);
                MessageBox.Show("Ürün favorilerinize eklendi");
            }
            else
            {
                MessageBox.Show("Favori grup adı giriniz");
            }
        }

        //favori grup seçme yada favori grup ekleme
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                label18.Visible = true;
                textBox1.Visible = true;
                button1.Visible = true;
                button1.Location = new Point(420, 220);
                label19.Visible = false;
                comboBox1.Visible = false;
            }
            if (radioButton4.Checked == true)
            {
                label19.Visible = true;
                comboBox1.Visible = true;
                button1.Visible = true;
                button1.Location = new Point(580, 220);
                label18.Visible = false;
                textBox1.Visible = false;
            }
        }
    }
}
