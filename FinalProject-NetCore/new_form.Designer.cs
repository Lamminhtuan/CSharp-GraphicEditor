namespace FinalProject_NetCore
{
    partial class new_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(new_form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_height = new System.Windows.Forms.TextBox();
            this.txt_width = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_color = new System.Windows.Forms.Button();
            this.btn_fhd = new System.Windows.Forms.Button();
            this.btn_hd = new System.Windows.Forms.Button();
            this.btn_a4 = new System.Windows.Forms.Button();
            this.btn_logo = new System.Windows.Forms.Button();
            this.btn_insta = new System.Windows.Forms.Button();
            this.btn_photo = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ptb_color = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_color)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_height);
            this.groupBox1.Controls.Add(this.txt_width);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kích thước";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chiều cao:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chiều rộng:";
            // 
            // txt_height
            // 
            this.txt_height.Location = new System.Drawing.Point(103, 47);
            this.txt_height.Name = "txt_height";
            this.txt_height.Size = new System.Drawing.Size(100, 23);
            this.txt_height.TabIndex = 1;
            this.txt_height.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_width
            // 
            this.txt_width.Location = new System.Drawing.Point(103, 18);
            this.txt_width.Name = "txt_width";
            this.txt_width.Size = new System.Drawing.Size(100, 23);
            this.txt_width.TabIndex = 0;
            this.txt_width.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(174, 258);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 1;
            this.btn_new.Text = "Tạo mới";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_color
            // 
            this.btn_color.Location = new System.Drawing.Point(12, 258);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(75, 23);
            this.btn_color.TabIndex = 1;
            this.btn_color.Text = "Chọn màu";
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // btn_fhd
            // 
            this.btn_fhd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fhd.Image = ((System.Drawing.Image)(resources.GetObject("btn_fhd.Image")));
            this.btn_fhd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_fhd.Location = new System.Drawing.Point(12, 94);
            this.btn_fhd.Name = "btn_fhd";
            this.btn_fhd.Size = new System.Drawing.Size(75, 23);
            this.btn_fhd.TabIndex = 3;
            this.btn_fhd.Text = "Full HD";
            this.btn_fhd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_fhd.UseVisualStyleBackColor = true;
            this.btn_fhd.Click += new System.EventHandler(this.btn_fhd_Click);
            // 
            // btn_hd
            // 
            this.btn_hd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_hd.Image = ((System.Drawing.Image)(resources.GetObject("btn_hd.Image")));
            this.btn_hd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_hd.Location = new System.Drawing.Point(93, 94);
            this.btn_hd.Name = "btn_hd";
            this.btn_hd.Size = new System.Drawing.Size(75, 23);
            this.btn_hd.TabIndex = 3;
            this.btn_hd.Text = "HD";
            this.btn_hd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_hd.UseVisualStyleBackColor = true;
            this.btn_hd.Click += new System.EventHandler(this.btn_hd_Click);
            // 
            // btn_a4
            // 
            this.btn_a4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_a4.Image = ((System.Drawing.Image)(resources.GetObject("btn_a4.Image")));
            this.btn_a4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_a4.Location = new System.Drawing.Point(174, 94);
            this.btn_a4.Name = "btn_a4";
            this.btn_a4.Size = new System.Drawing.Size(75, 23);
            this.btn_a4.TabIndex = 3;
            this.btn_a4.Text = "LinkedIn";
            this.btn_a4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_a4.UseVisualStyleBackColor = true;
            this.btn_a4.Click += new System.EventHandler(this.btn_a4_Click);
            // 
            // btn_logo
            // 
            this.btn_logo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_logo.Image = ((System.Drawing.Image)(resources.GetObject("btn_logo.Image")));
            this.btn_logo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_logo.Location = new System.Drawing.Point(12, 123);
            this.btn_logo.Name = "btn_logo";
            this.btn_logo.Size = new System.Drawing.Size(75, 23);
            this.btn_logo.TabIndex = 3;
            this.btn_logo.Text = "Logo";
            this.btn_logo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_logo.UseVisualStyleBackColor = true;
            this.btn_logo.Click += new System.EventHandler(this.btn_logo_Click);
            // 
            // btn_insta
            // 
            this.btn_insta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_insta.Image = ((System.Drawing.Image)(resources.GetObject("btn_insta.Image")));
            this.btn_insta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_insta.Location = new System.Drawing.Point(93, 123);
            this.btn_insta.Name = "btn_insta";
            this.btn_insta.Size = new System.Drawing.Size(75, 23);
            this.btn_insta.TabIndex = 3;
            this.btn_insta.Text = "Insta";
            this.btn_insta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_insta.UseVisualStyleBackColor = true;
            this.btn_insta.Click += new System.EventHandler(this.btn_insta_Click);
            // 
            // btn_photo
            // 
            this.btn_photo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_photo.Image = ((System.Drawing.Image)(resources.GetObject("btn_photo.Image")));
            this.btn_photo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_photo.Location = new System.Drawing.Point(174, 123);
            this.btn_photo.Name = "btn_photo";
            this.btn_photo.Size = new System.Drawing.Size(75, 23);
            this.btn_photo.TabIndex = 3;
            this.btn_photo.Text = "Ảnh";
            this.btn_photo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_photo.UseVisualStyleBackColor = true;
            this.btn_photo.Click += new System.EventHandler(this.btn_photo_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ptb_color);
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Màu nền";
            // 
            // ptb_color
            // 
            this.ptb_color.Location = new System.Drawing.Point(6, 22);
            this.ptb_color.Name = "ptb_color";
            this.ptb_color.Size = new System.Drawing.Size(225, 72);
            this.ptb_color.TabIndex = 0;
            this.ptb_color.TabStop = false;
            // 
            // new_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(261, 289);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_a4);
            this.Controls.Add(this.btn_hd);
            this.Controls.Add(this.btn_photo);
            this.Controls.Add(this.btn_insta);
            this.Controls.Add(this.btn_logo);
            this.Controls.Add(this.btn_fhd);
            this.Controls.Add(this.btn_color);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "new_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo mới";
            this.Load += new System.EventHandler(this.new_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_color)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txt_width;
        private ColorDialog colorDialog1;
        private TextBox txt_height;
        private Button btn_new;
        private Button btn_color;
        private Label label2;
        private Label label1;
        private Button btn_fhd;
        private Button btn_hd;
        private Button btn_a4;
        private Button btn_logo;
        private Button btn_insta;
        private Button btn_photo;
        private GroupBox groupBox2;
        private PictureBox ptb_color;
    }
}