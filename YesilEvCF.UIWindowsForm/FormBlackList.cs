using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormBlackList : Form
    {
        public User _user;
        public FormBlackList()
        {
            InitializeComponent();
        }
        public FormBlackList(User user) : this()
        {
            _user = user;
        }

        //kullanıcının karaliste ürünlerini listeleme
        private void FormBlackList_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Ürün Adı";
            dataGridView1.Columns[1].Name = "Kullanıcı Adı";
            dataGridView1.Columns[2].Name = "İçerik Adı";
            dataGridView1.Columns[3].Name = "Risk Seviye";
            List<string> ingredients = new List<string>();
            ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
            var userProductIngredient = productIngredientDAL.GetAllUserProductIngredients(FormLogin.userLogin.UserID);
            foreach (var item in userProductIngredient)
            {
                dataGridView1.Rows.Add(item.ProductName, item.UserName, item.IngredientName, item.ContentName);
                if (item.ContentName != "Kara Liste İçerik")
                {
                    ingredients.Add(item.IngredientName);
                }

            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Kara Liste için seçin";
            btn.Text = "Kara Listeye Alın";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //ana ekrana gitme butonu
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FormMain anaEkran = new FormMain();
            anaEkran.Show();
            this.Close();
        }

        //kullanıcı profiline giden buton
        private void button5_Click(object sender, EventArgs e)
        {
            FormUserProfile user = new FormUserProfile();
            user.Show();
            this.Close();
        }
        //kara listeye alma
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IngredientDAL ingredientDAL = new IngredientDAL();
            BlackListDAL blackListDAL = new BlackListDAL();
            ProductDAL productDAL = new ProductDAL();
            IngredientListDTO ingredient = null;
            Ingredient ingredientdb = null;
            string product = null;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                product = row.Cells[0].Value.ToString();
                ingredient = ingredientDAL.GetAllUserIngredientsByIngredientName(FormLogin.userLogin.UserID, row.Cells[0].Value.ToString(), row.Cells[2].Value.ToString());
            }
            ingredientdb = ingredientDAL.GetIngredientByFilter(ingredient.IngredientName);
            AddBlackListDTO addBlackListDTO = new AddBlackListDTO()
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                UserID = FormLogin.userLogin.UserID,
                IngredientID = ingredientdb.IngredientID
            };
            if (e.ColumnIndex == 4)
            {
                ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                var productdb = productDAL.GetProductByName(product);
                blackListDAL.AddBlackList(addBlackListDTO);
                var proing = productIngredientDAL.GetProductIngredientByProductID(productdb.ProductID, ingredientdb.IngredientID);
                proing.ContentID = 1;
                productIngredientDAL.UpdateProductIngredient(proing);
                MessageBox.Show("İçerik kara listeye alındı!!");
                dataGridView1.Rows.Clear();
                List<string> ingredients = new List<string>();

                var userProductIngredient = productIngredientDAL.GetAllUserProductIngredients(FormLogin.userLogin.UserID);
                foreach (var item in userProductIngredient)
                {
                    dataGridView1.Rows.Add(item.ProductName, item.UserName, item.IngredientName, item.ContentName);
                    if (item.ContentName != "Kara Liste İçerik")
                    {
                        ingredients.Add(item.IngredientName);
                    }

                }
            }
        }
    }
}
