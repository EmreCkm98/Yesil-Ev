namespace YesilEvCF.UIWindowsForm
{
    partial class FormSearchHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchHistory));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(515, 191);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(599, 355);
            this.listView2.TabIndex = 42;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Favori Grubu";
            this.columnHeader4.Width = 130;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Ürün Adı";
            this.columnHeader5.Width = 155;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Oluşturulma Tarihi";
            this.columnHeader6.Width = 195;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(406, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 36);
            this.button1.TabIndex = 41;
            this.button1.Text = "Sil";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(50, 191);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(439, 355);
            this.listView1.TabIndex = 40;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Aranan Ürün Adı";
            this.columnHeader1.Width = 143;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Arama Tarihi";
            this.columnHeader2.Width = 203;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(590, 119);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(195, 34);
            this.button6.TabIndex = 39;
            this.button6.Text = "Favoriler";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 38;
            this.label1.Text = "Arama Geçmişi";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button7.BackgroundImage")));
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(12, 21);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(84, 61);
            this.button7.TabIndex = 86;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(1015, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 69);
            this.button5.TabIndex = 87;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(12, 580);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(80, 63);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 95;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox9_Click);
            // 
            // FormSearchHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1160, 655);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "FormSearchHistory";
            this.Text = "FormSearchHistory";
            this.Load += new System.EventHandler(this.FormSearchHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox pictureBox9;
    }
}