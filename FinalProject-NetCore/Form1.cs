using Emgu.CV;
using Emgu.CV.Structure;
namespace FinalProject_NetCore
{
    public partial class Form1 : Form
    {

        Image<Bgr, byte> ori;
        public Form1()
        {
            InitializeComponent();
        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, byte> img = new Image<Bgr, byte>(ofd.FileName);
                ori = img;
                ptb_main.Image = img.ToBitmap();
            }
        }
        private void AdjustBrightnessContrast()
        {
            try
            {
                
                
                double currentcontrast = (double)bar_contrast.Value / 100;
                var imgoutput = ori.Mul(currentcontrast) + bar_brightness.Value;
              
                ptb_main.Image = imgoutput.AsBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bar_brightness_Scroll(object sender, EventArgs e)
        {
            AdjustBrightnessContrast();
        }

        private void bar_contrast_Scroll(object sender, EventArgs e)
        {
            AdjustBrightnessContrast();
        }

        private void bộLọcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xoayTráiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            
            ptb_main.Image = bmp;
        }

        private void xoayPhảiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
           
            ptb_main.Image = bmp;
        }

        private void xoay180ĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            
            ptb_main.Image = bmp;
        }

        private void lậtGươngNgangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            
            ptb_main.Image = bmp;
        }

        private void lậtGươngDọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
         
            ptb_main.Image = bmp;
        }

        private void ảnhXámToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Gray, byte> gray = img.Convert<Gray, byte>();
            ptb_main.Image = gray.ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ptb_main.Image = ori.ToBitmap();
        }

        private void âmBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.Not();
            ptb_main.Image = neg.ToBitmap();
        }

        private void làmMờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.SmoothGaussian(5);
            ptb_main.Image = neg.ToBitmap();
        }
    }
}