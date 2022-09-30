using System;
using System.Transactions;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminProductDelete : Form
    {
        public FormAdminProductDelete()
        {
            InitializeComponent();
        }

        private void FormAdminProductDelete_Load(object sender, EventArgs e)
        {
            var productDAL = new ProductDAL();
            var products = productDAL.GetAllProduct();

            dataGridView1.ColumnCount = 9;
            dataGridView1.Columns[0].Name = "Adı";
            dataGridView1.Columns[1].Name = "Barcode";
            dataGridView1.Columns[2].Name = "Firma";
            dataGridView1.Columns[3].Name = "Ürün foto ön yüz";
            dataGridView1.Columns[4].Name = "Ürün foto arka yüz";
            dataGridView1.Columns[5].Name = "Kategori";
            dataGridView1.Columns[6].Name = "Kullanıcı";
            dataGridView1.Columns[7].Name = "Aktif Mi";
            dataGridView1.Columns[8].Name = "Oluşturulma Tarihi";

            foreach (var item in products)
            {
                dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Sil";
            btn.Text = "Ürünü Silin";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
        }
        //seçili ürünü silme
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    ProductDAL productDAL = new ProductDAL();
                    ProductIngredientDAL productIngredientDAL = new ProductIngredientDAL();
                    ProductStatusDAL productStatusDAL = new ProductStatusDAL();
                    SearchHistoryDAL searchHistoryDAL = new SearchHistoryDAL();
                    FavoriteDAL userFavoriteDAL = new FavoriteDAL();
                    Guid guid = new Guid(row.Cells[1].Value.ToString());
                    var product = productDAL.GetProductByBarcode(guid);
                    using (TransactionScope ts = new TransactionScope())
                    {
                        try
                        {
                            //delete product ingredient
                            var productIngredient = productIngredientDAL.GetProductIngredientByProductID(product.ProductID);
                            if (productIngredient.Count > 0)
                            {
                                foreach (var item in productIngredient)
                                {
                                    productIngredientDAL.DeleteProductIngredient(item);
                                }

                            }

                            //delete product status
                            var productStatus = productStatusDAL.GetProductStatusByProductID(product.ProductID);
                            if (productStatus != null)
                            {
                                productStatusDAL.DeleteProductStatus(productStatus);
                            }

                            //delete user favorite
                            var userFavorite = userFavoriteDAL.GetUserFavoriteByProductID(product.ProductID);
                            if (userFavorite.Count > 0)
                            {
                                foreach (var item in userFavorite)
                                {
                                    userFavoriteDAL.DeleteUserFavorite(item);
                                }

                            }
                            // delete search history
                            var searchHistory = searchHistoryDAL.GetSearchHistoryByProductID(product.ProductID);
                            if (searchHistory.Count > 0)
                            {
                                foreach (var item in searchHistory)
                                {
                                    searchHistoryDAL.DeleteSearchHistory(item);
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
                    productDAL.DeleteProduct(product.ProductID);
                    MessageBox.Show("Ürün Silindi");
                    dataGridView1.Rows.Clear();
                    var products = productDAL.GetAllProduct(); ;
                    foreach (var item in products)
                    {
                        dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
                    }
                }
            }
        }
    }
}
