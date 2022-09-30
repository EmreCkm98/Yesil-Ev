using System;
using System.Drawing;
using System.Windows.Forms;
using YesilEvCF.DAL.Concrete;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormProductIngredient : Form
    {
        public Guid _guid;//barkod nosu girilen ürüne ulaşıyorum.
        public Label _selectedIngredient;//ürün sayfasından seçilen ürüne ulaşıyorum
        public FormProductIngredient()
        {
            InitializeComponent();
        }
        public FormProductIngredient(Guid guid) : this()
        {
            _guid = guid;
        }
        public FormProductIngredient(Label selectedIngredient) : this()
        {
            _selectedIngredient = selectedIngredient;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //ürünün içerik bilgilerini listeliyorum
        private void FormProductIngredient_Load(object sender, EventArgs e)
        {
            ProductDAL productDAL = new ProductDAL();
            var userp = productDAL.GetProductByBarcode(_guid);
            var userProduct = productDAL.GetAllProductByCategoryByIngredient(FormLogin.userLogin.UserID, _selectedIngredient.Text);
            foreach (var item in userProduct)
            {
                label1.Text = item.ManufacturerName + "-" + item.ProductName;
                if (item.ContentName == "Kara Liste İçerik")
                {
                    pictureBox1.BackgroundImage = Image.FromFile(@"C:\Users\emre\source\repos\YesilEvCF\YesilEvCF.UIWindowsForm\Images\blacklist_icon.png");
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    label2.Text = item.IngredientName;
                    pictureBox2.Visible = true;
                    label3.Visible = true;
                    label5.Text = item.IngredientContent;
                }
                else if (item.ContentName == "Riskli İçerik")
                {
                    pictureBox1.BackgroundImage = Image.FromFile(@"C:\Users\emre\source\repos\YesilEvCF\YesilEvCF.UIWindowsForm\Images\red_leaf_icon.png");
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    label2.Text = item.IngredientName;
                    pictureBox2.Visible = false;
                    label3.Visible = false;
                    label5.Text = item.IngredientContent;
                }
                else if (item.ContentName == "Orta Riskli İçerik")
                {
                    pictureBox1.BackgroundImage = Image.FromFile(@"C:\Users\emre\source\repos\YesilEvCF\YesilEvCF.UIWindowsForm\Images\orange_leaf_icon.png");
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    label2.Text = item.IngredientName;
                    pictureBox2.Visible = false;
                    label3.Visible = false;
                    label5.Text = item.IngredientContent;
                }
                else if (item.ContentName == "Az Riskli İçerik")
                {
                    pictureBox1.BackgroundImage = Image.FromFile(@"C:\Users\emre\source\repos\YesilEvCF\YesilEvCF.UIWindowsForm\Images\yellow_leaf_icon.png");
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    label2.Text = item.IngredientName;
                    pictureBox2.Visible = false;
                    label3.Visible = false;
                    label5.Text = item.IngredientContent;
                }
                else if (item.ContentName == "Temiz İçerik")
                {
                    pictureBox1.BackgroundImage = Image.FromFile(@"C:\Users\emre\source\repos\YesilEvCF\YesilEvCF.UIWindowsForm\Images\green_leaf_icon.png");
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                    label2.Text = item.IngredientName;
                    pictureBox2.Visible = false;
                    label3.Visible = false;
                    label5.Text = item.IngredientContent;
                }
            }
            //ürün içeriğinin hangi ürünlerde bulundugunun sayısını ekrana yazdırıyorum.            
            var ingredientCount = productDAL.GetProductIngredientCount(_selectedIngredient.Text);
            foreach (var item in ingredientCount)
            {
                label4.Text = "Bu içerik " + item.Count + " ürünün bileşenleri arasında yer almaktadır.";
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
            FormUserProfile user = new FormUserProfile();
            user.Show();
            this.Close();
        }
    }
}
