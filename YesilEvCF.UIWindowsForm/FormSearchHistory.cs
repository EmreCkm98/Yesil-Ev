using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;
using YesilEvCF.DTOs;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormSearchHistory : Form
    {
        int butonClickCount = 0;
        public FormSearchHistory()
        {
            InitializeComponent();
        }

        private void FormSearchHistory_Load(object sender, EventArgs e)
        {
            //aranan ürünleri listviewde gosteriyorum
            SearchHistoryDAL searchHistoryDAL = new SearchHistoryDAL();
            ProductDAL productDAL = new ProductDAL();

            var productList = searchHistoryDAL.GetAllSearchHistory();
            foreach (var item in productList)
            {
                var product = productDAL.GetProductByID(item.ProductID);
                string[] row = { product.ProductName, item.SearchDate.ToString() };
                ListViewItem listItem = new ListViewItem(row);
                listItem.Tag = item;
                listView1.Items.Add(listItem);
            }
        }

        List<SearchHistoryDTO> selectedItems = new List<SearchHistoryDTO>();
        //arama geçmişinden kayıt silme
        private void button1_Click(object sender, EventArgs e)
        {
            SearchHistoryDAL searchHistoryDAL = new SearchHistoryDAL();
            ProductDAL productDAL = new ProductDAL();
            foreach (var item in selectedItems)
            {
                var searchDate = searchHistoryDAL.GetSearchHistoryBySearchDate(item.SearchDate);
                foreach (var deletedSearch in searchDate)
                {
                    searchHistoryDAL.DeleteSearchHistory(deletedSearch);
                    MessageBox.Show("Aranan ürün silindi");
                    listView1.Items.Clear();
                }
            }
            var productList = searchHistoryDAL.GetAllSearchHistory();
            foreach (var item in productList)
            {
                var product = productDAL.GetProductByID(item.ProductID);
                string[] row = { product.ProductName, item.SearchDate.ToString() };
                ListViewItem listItem = new ListViewItem(row);
                listItem.Tag = item;
                listView1.Items.Add(listItem);
            }

        }

        //listviewden secili a.geçmişi kaydı alma
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                SearchHistoryDTO searcHistory = (SearchHistoryDTO)item.Tag;
                selectedItems.Add(searcHistory);
            }
        }

        //favorileri listeleme
        private void button6_Click(object sender, EventArgs e)
        {
            butonClickCount++;
            if (butonClickCount == 1)
            {
                ProductDAL productDAL = new ProductDAL();
                var productList = productDAL.GetAllUserFavoriteProduct(FormLogin.userLogin.UserID);
                foreach (var item in productList)
                {
                    string[] row = { item.FavoriteName, item.ProductName, item.CreatedDate };
                    ListViewItem listItem = new ListViewItem(row);
                    listItem.Tag = row;
                    listView2.Items.Add(listItem);//listviewe ekledim

                }
            }
        }

        //ana ekrana gitme butonu
        private void pictureBox9_Click(object sender, EventArgs e)
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
            this.Close();
        }
    }
}
