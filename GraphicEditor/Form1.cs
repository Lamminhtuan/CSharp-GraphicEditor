using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicEditor
{
    public partial class Form1 : Form
    {
        private Bitmap OriginalImage;
        private Bitmap EditedImage;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help_form hf = new help_form();
            hf.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ptb_main.BackgroundImage = new Bitmap(openFileDialog.FileName);
                OriginalImage = (Bitmap)ptb_main.BackgroundImage;
                EditedImage = OriginalImage;
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
