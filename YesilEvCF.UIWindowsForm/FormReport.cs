using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using YesilEvCF.Core.Context;

namespace YesilEvCF.UIWindowsForm
{
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }

        //hangi ürünün kaç içeriği var
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var urunCount = yesilEvEntities.ProductIngredients.Join(yesilEvEntities.Products, x => x.ProductID, y => y.ProductID, (pi, p) =>
                new
                {
                    productid = pi.ProductID,
                    ingredientid = pi.IngredientID,
                    productName = p.ProductName
                }).Join(yesilEvEntities.Ingredients, x => x.ingredientid, y => y.IngredientID, (pi, i) => new
                {
                    productid = pi.productid,
                    ingredientid = pi.ingredientid,
                    productName = pi.productName,
                    ingredientname = i.IngredientName
                }).GroupBy(x => x.productName).Select(x => new { ProductName = x.Key, Count = x.Count() }).ToList();
                foreach (var item in urunCount)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //girilen madde hangi ürünlerde var?
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            YesilEvDbContext yesilEv = new YesilEvDbContext();
            if (textBox1.Text != "")
            {
                if (yesilEv.Ingredients.Any(x => x.IngredientName == textBox1.Text))
                {

                    using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
                    {
                        var urunCount = yesilEvEntities.ProductIngredients.Join(yesilEvEntities.Products, x => x.ProductID, y => y.ProductID, (pi, p) =>
                        new
                        {
                            productid = pi.ProductID,
                            ingredientid = pi.IngredientID,
                            productName = p.ProductName
                        }).Join(yesilEvEntities.Ingredients, x => x.ingredientid, y => y.IngredientID, (pi, i) => new
                        {
                            productid = pi.productid,
                            ingredientid = pi.ingredientid,
                            productName = pi.productName,
                            ingredientname = i.IngredientName
                        }).Where(x => x.ingredientname == textBox1.Text).Select(x => new { ProductName = x.productName }).ToList();
                        if (urunCount != null)
                        {
                            foreach (var item in urunCount)
                            {
                                listBox1.Items.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("girdiğiniz madde bulunamadı");
                }
            }
            else
            {
                MessageBox.Show("Aranacak maddeyi giriniz");
            }
        }

        //hangi kullanıcının kaç ürün girişi var?
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var userProduct = yesilEvEntities.Users.GroupJoin(yesilEvEntities.Products, x => x.UserID, y => y.UserID, (u, p) => new
                {
                    product = p,
                    user = u
                }).SelectMany(x => x.product.DefaultIfEmpty(), (u, p) => new
                {
                    user = u.user,
                    product = p
                }).GroupBy(x => x.user.Name).Select(x => new { Name = x.Key, Adet = x.Count(y => y.product.ProductID != null) }).OrderByDescending(x => x.Adet).ToList();
                foreach (var item in userProduct)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //girilen madde kaç kişinin favorisinde?
        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var userProduct = yesilEvEntities.UserFavorites.Join(yesilEvEntities.Favorites, x => x.FavoriteID, y => y.FavoriteID, (uf, f) => new
                {
                    userfav = uf,
                    fav = f
                }).Join(yesilEvEntities.Users, x => x.userfav.ProductID, y => y.UserID, (uf, u) => new
                {
                    userfav = uf.userfav,
                    fav = uf.fav,
                    user = u
                }).Join(yesilEvEntities.Products, x => x.user.UserID, y => y.UserID, (u, p) => new
                {
                    userfav = u.userfav,
                    fav = u.fav,
                    user = u.user,
                    product = p
                }).Join(yesilEvEntities.ProductIngredients, x => x.product.ProductID, y => y.ProductID, (p, pi) => new
                {
                    userfav = p.userfav,
                    fav = p.fav,
                    user = p.user,
                    product = p.product,
                    productingredients = pi
                }).Join(yesilEvEntities.Ingredients, x => x.productingredients.IngredientID, y => y.IngredientID, (pi, i) => new
                {
                    userfav = pi.userfav,
                    fav = pi.fav,
                    user = pi.user,
                    product = pi.product,
                    productingredients = pi.productingredients,
                    ingredients = i
                }).Where(x => x.ingredients.IngredientName == textBox2.Text).Select(x => new
                {
                    user = x.user.UserName
                }).Distinct().ToList();
                foreach (var item in userProduct)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //girilen madde kaç kişinin karalistesinde?
        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var karaliste = yesilEvEntities.BlackLists.Join(yesilEvEntities.Users, x => x.UserID, y => y.UserID, (b, u) => new
                {
                    blist = b,
                    user = u
                }).Join(yesilEvEntities.Ingredients, x => x.blist.IngredientID, y => y.IngredientID, (b, i) => new
                {
                    blist = b.blist,
                    user = b.user,
                    ingredient = i
                }).Where(x => x.ingredient.IngredientName == textBox3.Text).Select(x => new
                {
                    user = x.user.UserName
                }).ToList();
                foreach (var item in karaliste)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en riskli ürünler neler?
        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var productrisk = yesilEvEntities.ProductIngredients.Join(yesilEvEntities.Products, x => x.ProductID, y => y.ProductID, (pi, p) => new
                {
                    producting = pi,
                    product = p
                }).Join(yesilEvEntities.Ingredients, x => x.producting.IngredientID, y => y.IngredientID, (pi, i) => new
                {
                    producting = pi.producting,
                    product = pi.product,
                    ingredient = i
                }).Join(yesilEvEntities.Contents, x => x.producting.ContentID, y => y.ContentID, (i, c) => new
                {
                    producting = i.producting,
                    product = i.product,
                    ingredient = i.ingredient,
                    content = c
                }).Where(x => x.content.ContentID == 3).Select(x => new
                {
                    productName = x.product.ProductName
                }).Distinct().ToList();
                foreach (var item in productrisk)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en çok alerjen yapan ürünler neler?
        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var encokalerjen = yesilEvEntities.ProductIngredients.Join(yesilEvEntities.Ingredients, x => x.IngredientID, y => y.IngredientID, (pi, i) => new
                {
                    producting = pi,
                    ingredient = i
                }).Join(yesilEvEntities.Products, x => x.producting.ProductID, y => y.ProductID, (pi, p) => new
                {
                    producting = pi.producting,
                    ingredient = pi.ingredient,
                    product = p
                }).Join(yesilEvEntities.Contents, x => x.producting.ContentID, y => y.ContentID, (pi, c) => new
                {
                    producting = pi.producting,
                    ingredient = pi.ingredient,
                    product = pi.product,
                    content = c
                }).Where(x => x.producting.ContentID == 3).GroupBy(x => x.product.ProductName).Select(x => new
                {
                    productname = x.Key,
                    alerjenmaddesayisi = x.Count()
                }).OrderByDescending(x => x.alerjenmaddesayisi).ToList();
                foreach (var item in encokalerjen)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en favori ürünler?
        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var favProduct = yesilEvEntities.UserFavorites.Join(yesilEvEntities.Products, x => x.ProductID, y => y.UserID, (uf, p) => new
                {
                    userfav = uf,
                    product = p
                }).Select(x => new
                {
                    product = x.product
                }).GroupBy(x => new { x.product.ProductID, x.product.ProductName }).Select(x => new
                {
                    ürün = x.Key.ProductName,
                    adet = x.Count()
                }).OrderByDescending(x => x.adet).Take(1).ToList();
                foreach (var item in favProduct)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en fazla riskli ürün tutan ilk 3 kullanıcı?
        private void button10_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var risklikullanici = yesilEvEntities.Users.Join(yesilEvEntities.Products, x => x.UserID, y => y.UserID, (u, p) => new
                {
                    user = u,
                    product = p
                }).Join(yesilEvEntities.ProductIngredients, x => x.product.ProductID, y => y.ProductID, (p, pi) => new
                {
                    user = p.user,
                    product = p.product,
                    producting = pi
                }).Join(yesilEvEntities.Ingredients, x => x.producting.IngredientID, y => y.IngredientID, (pi, i) => new
                {
                    user = pi.user,
                    product = pi.product,
                    producting = pi.producting,
                    ingredient = i
                }).Join(yesilEvEntities.Contents, x => x.producting.ContentID, y => y.ContentID, (i, c) => new
                {
                    user = i.user,
                    product = i.product,
                    producting = i.producting,
                    ingredient = i.ingredient,
                    content = c
                }).Where(x => x.content.ContentID == 3).GroupBy(x => x.user.UserName).Select(x => new
                {
                    kullanici = x.Key,
                    adet = x.Count()
                }).OrderByDescending(x => x.adet).Take(3).ToList();
                foreach (var item in risklikullanici)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en cok ürün giren kullanıcıların ilk 5i username,maili
        private void button11_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var userProduct = yesilEvEntities.Users.Join(yesilEvEntities.Products, x => x.UserID, y => y.UserID, (u, p) => new
                {
                    user = u,
                    product = p
                }).GroupBy(x => new { x.user.UserName, x.user.Email }).Select(x => new
                {
                    username = x.Key.UserName,
                    useremail = x.Key.Email,
                    adet = x.Count()
                }).OrderByDescending(x => x.adet).Take(3).ToList();
                foreach (var item in userProduct)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en çok maddesi olan ürün ilk 10 ürün içerik sayılarıyla
        private void button12_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var productingencok = yesilEvEntities.ProductIngredients.Join(yesilEvEntities.Products, x => x.ProductID, y => y.ProductID, (pi, p) => new
                {
                    producting = pi,
                    product = p
                }).Join(yesilEvEntities.Ingredients, x => x.producting.IngredientID, y => y.IngredientID, (pi, i) => new
                {
                    producting = pi.producting,
                    product = pi.product,
                    ingredient = i
                }).GroupBy(x => new { x.product.ProductID, x.product.ProductName }).Select(x => new
                {
                    productid = x.Key.ProductID,
                    productname = x.Key.ProductName,
                    adet = x.Count()
                }).OrderByDescending(x => x.adet).Take(10).ToList();
                foreach (var item in productingencok)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //girilen ayda favorilere alınan ürünler
        private void button13_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (textBox4.Text != "")
            {
                using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
                {
                    var favay = yesilEvEntities.UserFavorites.Join(yesilEvEntities.Users, x => x.UserID, y => y.UserID, (uf, u) => new
                    {
                        userfav = uf,
                        user = u
                    }).Join(yesilEvEntities.Favorites, x => x.userfav.FavoriteID, y => y.FavoriteID, (uf, f) => new
                    {
                        userfav = uf.userfav,
                        user = uf.user,
                        favori = f
                    }).Join(yesilEvEntities.Products, x => x.userfav.ProductID, y => y.ProductID, (uf, p) => new
                    {
                        userfav = uf.userfav,
                        user = uf.user,
                        favori = uf.favori,
                        product = p
                    }).Where(x => x.userfav.CreatedDate.Month.ToString() == textBox4.Text).Select(x => new
                    {
                        username = x.user.UserName,
                        urunid = x.product.ProductID,
                        urunadi = x.product.ProductName,
                        tarih = x.userfav.CreatedDate
                    }).ToList();
                    foreach (var item in favay)
                    {
                        listBox1.Items.Add(item);
                    }
                }
            }
            else
            {
                MessageBox.Show("aranacak ay giriniz");
            }
        }

        //girilen ayda karalisteye alınan ürünler
        private void button14_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (textBox5.Text != "")
            {
                using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
                {
                    var userblacklist = yesilEvEntities.BlackLists.Join(yesilEvEntities.Users, x => x.UserID, y => y.UserID, (b, u) => new
                    {
                        blacklist = b,
                        user = u
                    }).Join(yesilEvEntities.Ingredients, x => x.blacklist.IngredientID, y => y.IngredientID, (bl, i) => new
                    {
                        blacklist = bl.blacklist,
                        user = bl.user,
                        ing = i
                    }).Join(yesilEvEntities.ProductIngredients, x => x.ing.IngredientID, y => y.IngredientID, (i, pi) => new
                    {
                        blacklist = i.blacklist,
                        user = i.user,
                        ing = i.ing,
                        producting = pi
                    }).Join(yesilEvEntities.Products, x => x.producting.ProductID, y => y.ProductID, (pi, p) => new
                    {
                        blacklist = pi.blacklist,
                        user = pi.user,
                        ing = pi.ing,
                        producting = pi.producting,
                        product = p
                    }).Where(x => x.blacklist.CreatedDate.Month.ToString() == textBox5.Text).Select(x => new
                    {
                        username = x.user.UserName,
                        ingredientname = x.ing.IngredientName,
                        productname = x.product.ProductName,
                        tarih = x.blacklist.CreatedDate
                    }).Distinct().ToList();
                    foreach (var item in userblacklist)
                    {
                        listBox1.Items.Add(item);
                    }
                }
            }
            else
            {
                MessageBox.Show("aranacak ay giriniz");
            }
        }

        //Kullanıcıların karalistedeki ürün sayısı?
        private void button15_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var usertotalblacklist = yesilEvEntities.BlackLists.Join(yesilEvEntities.Users, x => x.UserID, y => y.UserID, (b, u) => new
                {
                    blacklist = b,
                    user = u
                }).Join(yesilEvEntities.Ingredients, x => x.blacklist.IngredientID, y => y.IngredientID, (bl, i) => new
                {
                    blacklist = bl.blacklist,
                    user = bl.user,
                    ing = i
                }).Join(yesilEvEntities.ProductIngredients, x => x.ing.IngredientID, y => y.IngredientID, (i, pi) => new
                {
                    blacklist = i.blacklist,
                    user = i.user,
                    ing = i.ing,
                    producting = pi
                }).Join(yesilEvEntities.Products, x => x.producting.ProductID, y => y.ProductID, (pi, p) => new
                {
                    blacklist = pi.blacklist,
                    user = pi.user,
                    ing = pi.ing,
                    producting = pi.producting,
                    product = p
                }).GroupBy(x => x.user.UserName).Select(x => new
                {
                    username = x.Key,
                    adet = x.Count()
                }).ToList();
                foreach (var item in usertotalblacklist)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //Kullanıcıların favorideki ürün sayısı?
        private void button16_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var usertotalfavoritelist = yesilEvEntities.UserFavorites.Join(yesilEvEntities.Favorites, x => x.FavoriteID, y => y.FavoriteID, (uf, f) => new
                {
                    favoritelist = uf,
                    favorite = f
                }).Join(yesilEvEntities.Users, x => x.favoritelist.UserID, y => y.UserID, (uf, u) => new
                {
                    favoritelist = uf.favoritelist,
                    favorite = uf.favorite,
                    user = u
                }).Join(yesilEvEntities.Products, x => x.favoritelist.ProductID, y => y.ProductID, (uf, p) => new
                {
                    favoritelist = uf.favoritelist,
                    favorite = uf.favorite,
                    user = uf.user,
                    product = p
                }).GroupBy(x => x.user.UserName).Select(x => new
                {
                    username = x.Key,
                    adet = x.Count()
                }).ToList();
                foreach (var item in usertotalfavoritelist)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //kullanıcıların kaçı admin kaçı kullanıcı rolünde?
        private void button17_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var useradmincount = yesilEvEntities.Users.Join(yesilEvEntities.Rols, x => x.RollID, y => y.RolID, (u, r) => new
                {
                    user = u,
                    rol = r
                }).GroupBy(x => new { x.rol.RolID, x.rol.RollName }).Select(x => new
                {
                    rolname = x.Key.RollName,
                    adet = x.Count()
                }).ToList();
                foreach (var item in useradmincount)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //en az riski olan ürünler neler? bunların kaçı favori listelerinde var?
        private void button18_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var enazriskliFavori = yesilEvEntities.ProductIngredients.Join(yesilEvEntities.Ingredients, x => x.IngredientID, y => y.IngredientID, (pi, i) => new
                {
                    producting = pi,
                    ingredient = i
                }).Join(yesilEvEntities.Products, x => x.producting.ProductID, y => y.ProductID, (pi, p) => new
                {
                    producting = pi.producting,
                    ingredient = pi.ingredient,
                    product = p
                }).Join(yesilEvEntities.Contents, x => x.producting.ContentID, y => y.ContentID, (pi, c) => new
                {
                    producting = pi.producting,
                    ingredient = pi.ingredient,
                    product = pi.product,
                    content = c
                }).GroupJoin(yesilEvEntities.UserFavorites, x => x.product.ProductID, y => y.ProductID, (p, uf) => new
                {
                    producting = p.producting,
                    ingredient = p.ingredient,
                    product = p.product,
                    content = p.content,
                    userfav = uf
                }).SelectMany(x => x.userfav.DefaultIfEmpty(), (p, uf) => new
                {
                    producting = p.producting,
                    ingredient = p.ingredient,
                    product = p.product,
                    content = p.content,
                    userfav = p.userfav,
                    favori = uf
                }).Where(x => x.producting.ContentID == 5).GroupBy(x => x.product.ProductName).Select(x => new
                {
                    azriskliürün = x.Key,
                    favorilistesindekisayisi = x.Count(y => y.favori.ProductID != null)
                }).Distinct().ToList();

                foreach (var item in enazriskliFavori)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        //adminin onayı girilmemiş kaç ürünü var aylık ?
        private void button19_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            using (YesilEvDbContext yesilEvEntities = new YesilEvDbContext())
            {
                var adminonay = yesilEvEntities.Products.Where(x => x.CreatedDate.Month == 8).Select(x => new
                {
                    productname = x.ProductName,
                    olusturulmatarihi = x.CreatedDate
                }).ToList();
                foreach (var item in adminonay)
                {
                    listBox1.Items.Add(item);
                }
            }
        }
        //ana ekrana giden buton
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            FormMain anaEkran = new FormMain();
            anaEkran.Show();
            this.Close();
        }
    }
}
