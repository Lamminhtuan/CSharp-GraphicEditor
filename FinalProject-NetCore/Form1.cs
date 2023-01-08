using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XPhoto;
using System.Drawing.Imaging;
using System.Drawing;
namespace FinalProject_NetCore
{
    public partial class Form1 : Form
    {
  
        Image<Bgr, byte> ori;
        Image<Bgr, byte> ori_rotate;
        Image<Bgr, byte> ori_filter;
        Image<Bgr, byte> prev;
        bool flag = false;
        Color paintcolor = Color.Black;
        bool choose = false;
        bool draw = false;
        Item currentitem;
        int x, y, lx, ly = 0;

        // font style => ts_fontcb => tscombobox3
        // ptn_main => picturebox1
        //ts_txtToDraw => toolstriptextbox2
        // toolstriptxtbox3 => ts_fontstyle
        // toolstriptxtbox1 => ts_shadow
        public Form1()
        {
            InitializeComponent();
        }
        public enum Item
        {
            FilledRect, Rectangle, FilledEll, Ellipse, Line, Text, Brush, Pencil, eraser, ColorPicker
        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, byte> img = new Image<Bgr, byte>(ofd.FileName);
                ori = img;
                ori_rotate = ori;
                ori_filter = ori;
                ptb_main.Image = img.ToBitmap();
            }
        }
        private void AdjustBrightnessContrast()
        {
            try
            {

                Bitmap bmp = (Bitmap)ptb_main.Image;
                Image<Bgr, byte> inp = bmp.ToImage<Bgr, byte>();
                double currentcontrast = (double)bar_contrast.Value / 100;
                var imgoutput = ori_filter.Mul(currentcontrast) + bar_brightness.Value;
              
                ptb_main.Image = imgoutput.AsBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FontFamily[] ffm = FontFamily.Families;
            foreach (FontFamily f in ffm)
                ts_fontcb.Items.Add(f.GetName(1).ToString());
            for (int i = 8; i<100;i+=2)
                ts_brushsize.Items.Add(i.ToString());
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
            Bitmap rotated = bmp;
       
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            ptb_main.Image = bmp;
        }

        private void xoayPhảiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Bitmap rotated = bmp;
           
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            ptb_main.Image = bmp;
        }

        private void xoay180ĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            Bitmap rotated = bmp;
           
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            ptb_main.Image = bmp;
        }

        private void lậtGươngNgangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Bitmap rotated = bmp; 
          
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            ptb_main.Image = bmp;
        }

        private void lậtGươngDọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Bitmap rotated = bmp; 
           
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            ptb_main.Image = bmp;
        }

        private void ảnhXámToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Gray, byte> gray = img.Convert<Gray, byte>();
            ori_filter = gray.Convert<Bgr, byte>();
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
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.Not();
            ori_filter = neg;
            ptb_main.Image = neg.ToBitmap();
        }

        private void làmMờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.SmoothGaussian(25);
            ori_filter = neg;
            ptb_main.Image = neg.ToBitmap();
        }

        private void mờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.SmoothBilateral(25, 85, 85);

            ori_filter = neg;
            ptb_main.Image = neg.ToBitmap();
        }

        private void vẽBútChìĐenTrắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

            Mat output_gray = new Mat();
            Mat output = new Mat();
            CvInvoke.PencilSketch(img, output_gray, output, 60, (float)0.07, (float)0.07);
            ori_filter = output_gray.ToImage<Bgr, byte>();
            ptb_main.Image = output_gray.ToBitmap();
        }

        private void vẽBútChìMàuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

            Mat output_gray = new Mat();
            Mat output = new Mat();
            CvInvoke.PencilSketch(img, output_gray, output, 60, (float)0.07, (float)0.07);
            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output.ToBitmap();
            
        }

        private void hDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

           
            Mat output = new Mat();
            CvInvoke.DetailEnhance(img, output, (float)12.0, (float)0.15);
            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output.ToBitmap();
        }

        private void ptb_main_Click(object sender, EventArgs e)
        {
           
        }

        private void mùaHèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
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
            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output.ToBitmap();
        }

        private void xoayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void khửMắtĐỏToolStripMenuItem_Click(object sender, EventArgs e)
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

            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output.ToBitmap();
          
        }

        private void cânBằngTrắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

          
            Mat output = new Mat();
 
            var wb = new LearningBasedWB();
            wb.BalanceWhite(img, output);
            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output.ToBitmap();
    
        }
       
        private void tranhSơnDầuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
            Image<Bgr, byte> neg = img.SmoothBilateral(25, 85, 85);
            Mat output = new Mat();
            XPhotoInvoke.OilPainting(img, output, 7, 1);
            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output.ToBitmap();
        }

        private void xóaNềnXanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap inp = (Bitmap)ptb_main.Image;
            Bitmap output = new Bitmap(inp.Width, inp.Height);
            for (int y = 0; y < inp.Height; y++)
            {
                for (int x = 0; x < inp.Width; x++)
                {
                    Color camcolor = inp.GetPixel(x, y);
                    byte max = Math.Max(Math.Max(camcolor.R, camcolor.G), camcolor.B);
                    byte min = Math.Min(Math.Min(camcolor.R, camcolor.G), camcolor.B);
                    bool replace = camcolor.G != min //Xanh không phải màu có giá trị nhỏ nhất 
                        && (camcolor.G == max || max - camcolor.G < 12)
                        && (max - min > 96);
                    if (replace)
                        camcolor = Color.Transparent;
                    output.SetPixel(x, y, camcolor);
                }
            }
            ori_filter = output.ToImage<Bgr, byte>();
            ptb_main.Image = output;
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info_form f = new info_form();
            f.ShowDialog();
        }

        private void ảnhGốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ptb_main.Image = ori.ToBitmap();
            bar_brightness.Value = 0;
            bar_contrast.Value = 100;
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            choose = true;
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            choose = false;
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (choose)
                {
                    Bitmap bmp = (Bitmap)ptb_palette.Image.Clone();
                    paintcolor = bmp.GetPixel(e.X, e.Y);
                    ptb_ccolor.BackColor = paintcolor;
                    bar_red.Value = paintcolor.R;
                    bar_blue.Value = paintcolor.B;
                    bar_green.Value = paintcolor.G;
                    lb_bar_r.Text = paintcolor.R.ToString();
                    lb_bar_g.Text = paintcolor.G.ToString();
                    lb_bar_b.Text = paintcolor.B.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Màu chọn không phù hợp!");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void ptb_adjust_Click(object sender, EventArgs e)
        {
            pn_adjust.Visible = true;
            pn_color.Visible = false;
        }

        private void ptb_color_Click(object sender, EventArgs e)
        {
            pn_adjust.Visible = false;
            pn_color.Visible = true;
        }

        private void ptb_main_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                Graphics g = ptb_main.CreateGraphics();
                switch (currentitem) {
                    case Item.Brush:
                        g.FillEllipse(new SolidBrush(paintcolor), e.X, e.Y, Convert.ToUInt16(ts_brushsize.Text), Convert.ToUInt16(ts_brushsize.Text));
                        break;

                    case Item.eraser:
                        g.FillEllipse(new SolidBrush(ptb_main.BackColor), e.X - x + x, e.Y - y + y,
                            Convert.ToInt32(ts_brushsize.Text), Convert.ToInt32(ts_brushsize.Text));
                        break;
                    case Item.Pencil:

                        g.FillEllipse(new SolidBrush(paintcolor), e.X, e.Y, 5, 5);
                           
                        break;

                }

                g.Dispose();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            currentitem = Item.Line;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            currentitem = Item.Rectangle;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            currentitem = Item.Ellipse;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            currentitem = Item.Brush;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            currentitem = Item.Pencil;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            currentitem = Item.ColorPicker;
        }

        private void ptb_main_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentitem == Item.ColorPicker)
            {
                Bitmap bmp = new Bitmap(ptb_main.Width, ptb_main.Height);
                Graphics g = Graphics.FromImage(bmp);
                Rectangle rect = ptb_main.RectangleToScreen(ptb_main.ClientRectangle);
                g.CopyFromScreen(rect.Location, Point.Empty, ptb_main.Size);
                g.Dispose();
                paintcolor = bmp.GetPixel(e.X, e.Y);
                ptb_ccolor.BackColor = paintcolor;
                bar_red.Value = paintcolor.R;
                bar_green.Value = paintcolor.G;
                bar_blue.Value = paintcolor.B;
                lb_bar_r.Text = paintcolor.R.ToString();
                lb_bar_g.Text = paintcolor.G.ToString();
                lb_bar_b.Text = paintcolor.B.ToString();
                bmp.Dispose();

            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            currentitem = Item.eraser;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            currentitem = Item.Text;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Graphics g = ptb_main.CreateGraphics();
            if (currentitem == Item.Text)
            {
                // font
                if (ts_FontStyle.Text == "Regular")
                {
                    g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text,  Convert.ToInt32(ts_FontSize.Text), FontStyle.Regular)
               , new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (ts_FontStyle.Text == "Bold")
                {
                    g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                        FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (ts_FontStyle.Text == "Underline")
                {
                    g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                        FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (ts_FontStyle.Text == "Strikeout")
                {
                    g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                        FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (ts_FontStyle.Text == "Italic")
                {
                    g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                        FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                }

                // shadow 
                if (ts_TxtShadow.Text == "SE")
                {
                    if (ts_FontStyle.Text == "Regular")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Bold")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Underline")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Strikeout")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Italic")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (ts_TxtShadow.Text == "SW")
                {
                    if (ts_FontStyle.Text == "Regular")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Bold")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Underline")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Strikeout")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Italic")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (ts_TxtShadow.Text == "NE")
                {
                    if (ts_FontStyle.Text == "Regular")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Bold")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Underline")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Strikeout")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Italic")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (ts_TxtShadow.Text == "NW")
                {
                    if (ts_FontStyle.Text == "Regular")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Bold")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Underline")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Strikeout")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (ts_FontStyle.Text == "Italic")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                g.Dispose();

            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ptb_main_MouseDown(object sender, MouseEventArgs e)
        {

            draw = true;
            x = e.X;
            y = e.Y;
        }

        private void ptb_main_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = e.X;
            ly = e.Y;
            Graphics g = ptb_main.CreateGraphics();
            switch (currentitem)
            {
                case Item.FilledRect:
                    g.FillRectangle(new SolidBrush(paintcolor), x, y, e.X - x, e.Y - y);
                    break;

                case Item.Rectangle:
                    g.DrawRectangle(new Pen(paintcolor, 2), x, y, e.X - x, e.Y - y);
                    break;

                case Item.Line:
                    g.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, ly));
                    break;

                case Item.FilledEll:
                    g.FillEllipse(new SolidBrush(paintcolor), x, y, e.X - x, e.Y - y);
                    break;

                case Item.Ellipse:
                    g.DrawEllipse(new Pen(paintcolor, 2), x, y, e.X - x, e.Y - y);
                    break;

              


                
            }



            g.Dispose();
            
            
        }
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Bitmap Image(.bmp)| *.bmp | Gif Image(.gif) | *.gif | JPEG Image(.jpeg) | *.jpeg | Png Image(.png) | *.png";
            if (save.ShowDialog() == DialogResult.OK)
            {
                if (save.FilterIndex == 1)
                {
                    ptb_main.Image.Save(save.FileName, ImageFormat.Bmp);
                }
                if (save.FilterIndex == 2)
                {
                    ptb_main.Image.Save(save.FileName, ImageFormat.Gif);
                }
                if (save.FilterIndex == 3)
                {
                    ptb_main.Image.Save(save.FileName, ImageFormat.Jpeg);
                }
                if (save.FilterIndex == 4)
                {
                    ptb_main.Image.Save(save.FileName, ImageFormat.Png);
                }
            }
        }

        
    }
}