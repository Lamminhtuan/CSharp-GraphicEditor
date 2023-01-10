using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject_NetCore
{
    public partial class new_form : Form
    {
        public new_form()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            newcanvas.height = Convert.ToUInt16(txt_height.Text);
            newcanvas.width = Convert.ToUInt16(txt_width.Text);
            this.Close();
            
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            ColorDialog cld = new ColorDialog();
            if (cld.ShowDialog() == DialogResult.OK)
            {
                ptb_color.BackColor = cld.Color;
                newcanvas.bgcolor = cld.Color;
            }
        }

        private void btn_logo_Click(object sender, EventArgs e)
        {
            txt_height.Text = 500.ToString();
            txt_width.Text = 500.ToString();
        }

        private void btn_fhd_Click(object sender, EventArgs e)
        {
            txt_height.Text = 1080.ToString();
            txt_width.Text = 1920.ToString();
        }

        private void btn_hd_Click(object sender, EventArgs e)
        {
            txt_height.Text = 720.ToString();
            txt_width.Text = 1280.ToString();
        }

        private void btn_insta_Click(object sender, EventArgs e)
        {
            txt_height.Text = 1080.ToString();
            txt_width.Text = 1080.ToString();
        }

        private void btn_photo_Click(object sender, EventArgs e)
        {
            txt_height.Text = 1080.ToString();
            txt_width.Text = 1200.ToString();
        }

        private void btn_a4_Click(object sender, EventArgs e)
        {
            txt_height.Text = 627.ToString();
            txt_width.Text = 1200.ToString();
        }

        private void new_form_Load(object sender, EventArgs e)
        {
            newcanvas.bgcolor = Color.Gray;
            ptb_color.BackColor = Color.Gray;
        }
    }
}
