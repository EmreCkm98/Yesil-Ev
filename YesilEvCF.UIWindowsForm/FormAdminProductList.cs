using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YesilEvCF.Core.Entities;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminProductList : Form
    {
        public FormAdminProductList()
        {
            InitializeComponent();
        }

        private void FormAdminProductList_Load(object sender, EventArgs e)
        {
            var productStatusDAL = new ProductStatusDAL();
            var products = productStatusDAL.GetPendingProducts();

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
            btn.HeaderText = "Onay";
            btn.Text = "Onaylayın";
            btn.Name = "btnPendingPage1";
            btn.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn1);
            btn1.HeaderText = "Reddet";
            btn1.Text = "Reddedin";
            btn1.Name = "btnPendingRed1";
            btn1.UseColumnTextForButtonValue = true;
        }

        //onay bekleyen ürünler listesi
        private void button1_Click(object sender, EventArgs e)
        {
            var productStatusDAL = new ProductStatusDAL();
            var products = productStatusDAL.GetPendingProducts();

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
            dataGridView1.Rows.Clear();
            label1.Text = "Onay bekleyen Ürünler";
            foreach (var item in products)
            {
                dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
            }
            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn2);
            btn2.HeaderText = "Onay";
            btn2.Text = "Onaylayın";
            btn2.Name = "btnPendingPage2";
            btn2.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn btn3 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn3);
            btn3.HeaderText = "Reddet";
            btn3.Text = "Reddedin";
            btn3.Name = "btnPendingRed2";
            btn3.UseColumnTextForButtonValue = true;
        }
        //onaylı ürünler listesi
        private void button2_Click(object sender, EventArgs e)
        {
            var productStatusDAL = new ProductStatusDAL();
            var products = productStatusDAL.GetApprovedProducts();

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
            dataGridView1.Rows.Clear();

            label1.Text = "Onaylı Ürünler";
            foreach (var item in products)
            {
                dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
            }
            DataGridViewButtonColumn btn4 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn4);
            btn4.HeaderText = "Onay Beklemede";
            btn4.Text = "Beklemede";
            btn4.Name = "btnPendingOnayBeklemede1";
            btn4.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn btn5 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn5);
            btn5.HeaderText = "Reddet";
            btn5.Text = "Reddedin";
            btn5.Name = "btnPendingRed3";
            btn5.UseColumnTextForButtonValue = true;
        }
        //reddedilmiş ürünler listesi
        private void button3_Click(object sender, EventArgs e)
        {
            var productStatusDAL = new ProductStatusDAL();
            var products = productStatusDAL.GetRejectedProducts();

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
            dataGridView1.Rows.Clear();

            label1.Text = "Reddedilmiş Ürünler";
            foreach (var item in products)
            {
                dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
            }
            DataGridViewButtonColumn btn6 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn6);
            btn6.HeaderText = "Onay Beklemede";
            btn6.Text = "Beklemede";
            btn6.Name = "btnPendingOnayBeklemede2";
            btn6.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn btn7 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn7);
            btn7.HeaderText = "Onay";
            btn7.Text = "Onaylayın";
            btn7.Name = "btnOnay10";
            btn7.UseColumnTextForButtonValue = true;
        }
        //seçili ürün onay durumu değiştirme
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedButton = sender as DataGridView;

            if (e.ColumnIndex == 9)
            {
                var productStatus = selectedButton.Columns[9].HeaderText;
                var productStatusButonName = selectedButton.Columns[9].Name;
                if (productStatus == "Onay")
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        ProductDAL productDAL = new ProductDAL();
                        var productStatusDAL = new ProductStatusDAL();
                        string productName = row.Cells[0].Value.ToString();
                        Guid guid = new Guid(row.Cells[1].Value.ToString());
                        var productStatusDb = productStatusDAL.GetProductStatusByProductInfo(productName, guid);
                        productStatusDb.ApprovementStatusID = 2;
                        productStatusDb.UserID = FormLogin.userLogin.UserID;
                        productStatusDAL.UpdateProductStatus(productStatusDb);
                        MessageBox.Show("Ürün Onaylandı");
                        dataGridView1.Rows.Clear();
                        List<Product> products = null;
                        if (productStatusButonName == "btnPendingPage1" || productStatusButonName == "btnPendingPage2")
                        {
                            products = productStatusDAL.GetPendingProducts();
                        }
                        else
                        {
                            products = productStatusDAL.GetRejectedProducts();
                        }

                        foreach (var item in products)
                        {
                            dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
                        }
                    }
                }
                if (productStatus == "Onay Beklemede")
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        ProductDAL productDAL = new ProductDAL();
                        var productStatusDAL = new ProductStatusDAL();
                        string productName = row.Cells[0].Value.ToString();
                        Guid guid = new Guid(row.Cells[1].Value.ToString());

                        var productStatusDb = productStatusDAL.GetProductStatusByProductInfo(productName, guid);
                        productStatusDb.ApprovementStatusID = 1;
                        productStatusDb.UserID = FormLogin.userLogin.UserID;
                        productStatusDAL.UpdateProductStatus(productStatusDb);
                        MessageBox.Show("Ürün Bekleme Durumuna alındı");
                        dataGridView1.Rows.Clear();
                        List<Product> products = null;
                        if (productStatusButonName == "btnPendingOnayBeklemede1")
                        {
                            products = productStatusDAL.GetApprovedProducts();
                        }
                        else
                        {
                            products = productStatusDAL.GetRejectedProducts();
                        }
                        foreach (var item in products)
                        {
                            dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
                        }
                    }
                }
            }
            if (e.ColumnIndex == 10)
            {
                var productStatus = selectedButton.Columns[10].HeaderText;
                var productStatusButonName = selectedButton.Columns[10].Name;
                if (productStatus == "Reddet")
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        ProductDAL productDAL = new ProductDAL();
                        var productStatusDAL = new ProductStatusDAL();
                        string productName = row.Cells[0].Value.ToString();
                        Guid guid = new Guid(row.Cells[1].Value.ToString());

                        var productStatusDb = productStatusDAL.GetProductStatusByProductInfo(productName, guid);
                        productStatusDb.ApprovementStatusID = 3;
                        productStatusDb.UserID = FormLogin.userLogin.UserID;
                        productStatusDAL.UpdateProductStatus(productStatusDb);
                        MessageBox.Show("Ürün Onayı Reddedildi");
                        dataGridView1.Rows.Clear();
                        List<Product> products = null;
                        if (productStatusButonName == "btnPendingRed1" || productStatusButonName == "btnPendingRed2")
                        {
                            products = productStatusDAL.GetPendingProducts();
                        }
                        else
                        {
                            products = productStatusDAL.GetApprovedProducts();
                        }
                        foreach (var item in products)
                        {
                            dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
                        }
                    }
                }
                if (productStatus == "Onay")
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        ProductDAL productDAL = new ProductDAL();
                        var productStatusDAL = new ProductStatusDAL();
                        string productName = row.Cells[0].Value.ToString();
                        Guid guid = new Guid(row.Cells[1].Value.ToString());

                        var productStatusDb = productStatusDAL.GetProductStatusByProductInfo(productName, guid);
                        productStatusDb.ApprovementStatusID = 2;
                        productStatusDb.UserID = FormLogin.userLogin.UserID;
                        productStatusDAL.UpdateProductStatus(productStatusDb);
                        MessageBox.Show("Ürün Onaylandı");
                        dataGridView1.Rows.Clear();
                        List<Product> products = null;
                        if (productStatusButonName == "btnOnay10")
                        {
                            products = productStatusDAL.GetRejectedProducts();
                        }
                        foreach (var item in products)
                        {
                            dataGridView1.Rows.Add(item.ProductName, item.Barcode, item.Manufacturer.ManufacturerName, item.ProductFrontImage, item.ProductBackImage, item.Category.CategoryName, item.User.UserName, item.IsActive == true ? "Evet" : "Hayır", item.CreatedDate.ToString());
                        }
                    }
                }
            }
        }
    }
}
