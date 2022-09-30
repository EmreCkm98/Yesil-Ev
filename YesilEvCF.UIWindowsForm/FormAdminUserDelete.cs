using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminUserDelete : Form
    {
        public FormAdminUserDelete()
        {
            InitializeComponent();
        }

        private void FormAdminUserDelete_Load(object sender, EventArgs e)
        {
            var userDAL = new UserDAL();
            var users = userDAL.GetAllUsers();

            dataGridView1.ColumnCount = 9;
            dataGridView1.Columns[0].Name = "Adı";
            dataGridView1.Columns[1].Name = "Soyadı";
            dataGridView1.Columns[2].Name = "Email";
            dataGridView1.Columns[3].Name = "Şifre";
            dataGridView1.Columns[4].Name = "Kullanıcı Adı";
            dataGridView1.Columns[5].Name = "Rolü";
            dataGridView1.Columns[6].Name = "Premium";
            dataGridView1.Columns[7].Name = "Aktif Mi";
            dataGridView1.Columns[8].Name = "Oluşturulma Tarihi";

            foreach (var item in users)
            {
                dataGridView1.Rows.Add(item.Name, item.UserSurname, item.Email, item.Password, item.UserName, item.Rol.RollName, item.Premium == true ? "Evet" : "Hayır", item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Sil";
            btn.Text = "Kullanıcıyı Silin";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
        }

        //seçili kullanıcıyı silme
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    UserDAL userDAL = new UserDAL();
                    FavoriteDAL favoriteDAL = new FavoriteDAL();
                    BlackListDAL blacklistDAL = new BlackListDAL();
                    ProductDAL productDAL = new ProductDAL();
                    ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                    ProductStatusDAL productStatusDAL = new ProductStatusDAL();
                    SearchHistoryDAL searchHistoryDAL = new SearchHistoryDAL();
                    FavoriteDAL userFavoriteDAL = new FavoriteDAL();
                    List<Product> deletedProducts = new List<Product>();
                    var user = userDAL.GetUserByUserInfo(row.Cells[4].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[2].Value.ToString());
                    using (TransactionScope ts = new TransactionScope())
                    {
                        try
                        {
                            //delete user favorite
                            var userFavorites = favoriteDAL.GetUserFavoriteByUserID(user.UserID);
                            if (userFavorites.Count > 0)
                            {
                                foreach (var item in userFavorites)
                                {
                                    favoriteDAL.DeleteUserFavorite(item);
                                }

                            }

                            //delete blacklist
                            var blacklists = blacklistDAL.GetBlackListByUserID(user.UserID);
                            if (blacklists.Count > 0)
                            {
                                foreach (var item in blacklists)
                                {
                                    blacklistDAL.DeleteBlackList(item);
                                }

                            }

                            //product
                            var products = productDAL.GetAllProductByUserID(user.UserID);
                            if (products.Count > 0)
                            {
                                foreach (var item in products)
                                {
                                    var deletedProduct = productDAL.DeleteProduct(item.ProductID);
                                    deletedProducts.Add(deletedProduct);
                                }

                            }
                            //delete product ingredient
                            if (deletedProducts.Count > 0)
                            {
                                foreach (var product in deletedProducts)
                                {
                                    var productIngredient = productIngredientDAL.GetProductIngredientByProductID(product.ProductID);
                                    if (productIngredient.Count > 0)
                                    {
                                        foreach (var item in productIngredient)
                                        {
                                            productIngredientDAL.DeleteProductIngredient(item);
                                        }

                                    }
                                }
                            }

                            //delete product status
                            if (deletedProducts.Count > 0)
                            {
                                foreach (var product in deletedProducts)
                                {
                                    var productStatus = productStatusDAL.GetProductStatusByProductID(product.ProductID);
                                    if (productStatus != null)
                                    {
                                        productStatusDAL.DeleteProductStatus(productStatus);
                                    }
                                }
                            }

                            //delete user favorite
                            if (deletedProducts.Count > 0)
                            {
                                foreach (var product in deletedProducts)
                                {
                                    var userFavorite = userFavoriteDAL.GetUserFavoriteByProductID(product.ProductID);
                                    if (userFavorite.Count > 0)
                                    {
                                        foreach (var item in userFavorite)
                                        {
                                            userFavoriteDAL.DeleteUserFavorite(item);
                                        }

                                    }
                                }
                            }

                            // delete search history
                            if (deletedProducts.Count > 0)
                            {
                                foreach (var product in deletedProducts)
                                {
                                    var searchHistory = searchHistoryDAL.GetSearchHistoryByProductID(product.ProductID);
                                    if (searchHistory.Count > 0)
                                    {
                                        foreach (var item in searchHistory)
                                        {
                                            searchHistoryDAL.DeleteSearchHistory(item);
                                        }

                                    }
                                }
                            }

                            ts.Complete();
                        }
                        catch (Exception ex)
                        {
                            ts.Dispose();
                            throw;
                        }
                    }

                    userDAL.DeleteUser(user.UserID);
                    MessageBox.Show("Kullanıcı Silindi");
                    dataGridView1.Rows.Clear();
                    var users = userDAL.GetAllUsers();
                    foreach (var item in users)
                    {
                        dataGridView1.Rows.Add(item.Name, item.UserSurname, item.Email, item.Password, item.UserName, item.Rol.RollName, item.Premium == true ? "Evet" : "Hayır", item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
                    }
                }
            }
        }
    }
}
