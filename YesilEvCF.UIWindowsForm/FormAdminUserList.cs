using System;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormAdminUserList : Form
    {
        public FormAdminUserList()
        {
            InitializeComponent();
        }

        private void FormAdminUserList_Load(object sender, EventArgs e)
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
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.HeaderText = "Sil";
            //btn.Text = "Ürünü Silin";
            //btn.Name = "btn";
            //btn.UseColumnTextForButtonValue = true;
        }
    }
}
