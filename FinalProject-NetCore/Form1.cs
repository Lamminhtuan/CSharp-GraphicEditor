using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XPhoto;
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
            bar_brightness.Value = 0;
            bar_contrast.Value = 100;
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
            Image<Bgr, byte> neg = img.SmoothGaussian(25);
            ptb_main.Image = neg.ToBitmap();
        }

        private void mờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.SmoothBilateral(25, 85, 85);
           
         
            ptb_main.Image = neg.ToBitmap();
        }

        private void vẽBútChìĐenTrắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

            Mat output_gray = new Mat();
            Mat output = new Mat();
            CvInvoke.PencilSketch(img, output_gray, output, 60, (float)0.07, (float)0.07);
            ptb_main.Image = output_gray.ToBitmap();
        }

        private void vẽBútChìMàuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

            Mat output_gray = new Mat();
            Mat output = new Mat();
            CvInvoke.PencilSketch(img, output_gray, output, 60, (float)0.07, (float)0.07);
            ptb_main.Image = output.ToBitmap();
            
        }

        private void hDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

           
            Mat output = new Mat();
            CvInvoke.DetailEnhance(img, output, (float)12.0, (float)0.15);
            ptb_main.Image = output.ToBitmap();
        }

        private void ptb_main_Click(object sender, EventArgs e)
        {

        }

        private void mùaHèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            float[,] array = new float[2, 4] { { 0, 64, 128, 256 }, { 0, 80, 160, 256 } };
            Matrix<double> kernel = new Matrix<double>(3,3);
            kernel[0, 0] = 0.272;
            kernel[0, 1] = 0.534;
            kernel[0, 2] = 0.131;
            kernel[1, 0] = 0.349;
            kernel[1, 1] = 0.686;
            kernel[1, 2] = 0.168;
            kernel[2, 0] = 0.393;
            kernel[2, 1] = 0.769;
            kernel[2, 2] = 0.189;
            Mat output = new Mat();
            CvInvoke.Transform(img, output, kernel);
            ptb_main.Image = output.ToBitmap();
        }

        private void xoayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void khửMắtĐỏToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

            CascadeClassifier eyes_cascade = new CascadeClassifier("haarcascade_eye.xml");
            Rectangle[] eyes;
            eyes = eyes_cascade.DetectMultiScale(img);
            for (int i = 0; i < eyes.Length; i++)
            {
                
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
          
        }

        private void pn_adjust_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cânBằngHistogramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void cânBằngHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

            Image<Gray, byte> gray = img.Convert<Gray, byte>();
            Mat output = new Mat();
            CvInvoke.EqualizeHist(gray, output);

          
            ptb_main.Image = output.ToBitmap();
          
        }

        private void cânBằngTrắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

          
            Mat output = new Mat();
 
            var wb = new LearningBasedWB();
            wb.BalanceWhite(img, output);
            ptb_main.Image = output.ToBitmap();
        }
    }
}