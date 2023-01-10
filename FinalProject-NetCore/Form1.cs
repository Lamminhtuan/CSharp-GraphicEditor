using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XPhoto;
using System.Drawing.Imaging;
using System.Drawing;
using Facebook;
using System.Drawing.Drawing2D;

namespace FinalProject_NetCore
{
    public partial class Form1 : Form
    {

        Image<Bgr, byte> ori;
        Image<Bgr, byte> ori_rotate;
        Image<Bgr, byte> ori_filter;
        
        
        Color paintcolor = Color.Black;
        bool choose = false;
        bool draw = false;
        bool drawtext = false;
        Item currentitem;
        int x, y, lx, ly;
        public Pen crpPen = new Pen(Color.White);
        Size currentsize;
        Size orisize;
        int ratio;
        //Point
        Point v1;
        Point v2;
        Point v3;
        bool fv1 = false;
        bool fv2 = false;
        bool fv3 = false;
        public Form1()
        {
            InitializeComponent();
        }
        public enum Item
        {
            FilledRect, FilledRect1, FilledRect2, FilledRect3, FilledRect4, Rectangle, 
            FilledEll, FilledEll1, FilledEll2, FilledEll3, FilledEll4, Ellipse, 
            Line, Text, Brush, Pencil, eraser, ColorPicker, test, CropImage, FilledTri, Tri, None, RedEyeRemover, Bucket
        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentitem = Item.None;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, byte> img = new Image<Bgr, byte>(ofd.FileName);

                if (img.Height > 1080)
                {
                    ptb_main.Width = (int)img.Width / 3;
                    ptb_main.Height = (int)img.Height / 3;

                }
                else if (img.Width > 940 || img.Height > 497)
                {
                    ptb_main.Width = (int)img.Width / 2;
                    ptb_main.Height = (int)img.Height / 2;

                }

                else
                {
                    ptb_main.Width = (int)img.Width / 1;
                    ptb_main.Height = (int)img.Height / 1;
                }

                ratio = img.Width / ptb_main.Width;

                ori = img;
                ori_rotate = ori;
                ori_filter = ori;
                orisize = ptb_main.Size;
                currentsize = orisize;
                ptb_main.BackColor = Color.Transparent;
              
                ptb_main.Image = img.ToBitmap();
       
                ptb_main.Invalidate();

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
            int oldwidth = ptb_main.Width;
            ptb_main.Width = ptb_main.Height;
            ptb_main.Height = oldwidth;
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            currentsize = ptb_main.Size;
            ptb_main.Image = bmp;
        }

        private void xoayPhảiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)ptb_main.Image;
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Bitmap rotated = bmp;
            int oldwidth = ptb_main.Width;
            ptb_main.Width = ptb_main.Height;
            ptb_main.Height = oldwidth;
            ori_rotate = rotated.ToImage<Bgr, byte>();
            ori_filter = ori_rotate;
            currentsize= ptb_main.Size;
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
            ptb_main.Size = orisize;
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
            if (draw && currentitem != Item.CropImage)
            {

                Graphics g = Graphics.FromImage(ptb_main.Image);

                switch (currentitem)
                {
                    case Item.Brush:
                        g.FillEllipse(new SolidBrush(paintcolor), e.X * ratio, e.Y * ratio, Convert.ToUInt16(ts_brushsize.Text), Convert.ToUInt16(ts_brushsize.Text));
                        break;

                    case Item.eraser:
                        g.FillEllipse(new SolidBrush(ptb_main.BackColor), e.X * ratio, e.Y * ratio,
                            Convert.ToInt32(ts_brushsize.Text), Convert.ToInt32(ts_brushsize.Text));
                        break;
                    case Item.Pencil:

                        g.FillEllipse(new SolidBrush(paintcolor), e.X * ratio, e.Y * ratio, 5, 5);

                        break;

                }
                Bitmap bmp = (Bitmap)ptb_main.Image;
                ptb_main.Invalidate();
                ori_filter = bmp.ToImage<Bgr, byte>();
                ori_rotate = ori_filter;
                g.Dispose();
                ptb_main.Refresh();
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
            if (drawtext == false)
            {
                drawtext = true;
                ts_btn_drawtext.BackColor = Color.Green;
            }
            else
            {
                drawtext = false;
                ts_btn_drawtext.BackColor = Color.Red;
            }

            
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void chiaSẻQuaFacebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var client = new FacebookClient();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
             if (WindowState == FormWindowState.Normal)
            {
                ptb_main.Size = orisize;
            }
            if (WindowState == FormWindowState.Maximized)
            {
                ptb_main.Size = orisize * 2;
            }
        }

        //test 
        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect2;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect3;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect1;

        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect2;
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect3;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect4;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll1;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll2;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll3;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll4;
        }

        private void toolStripButton11_Click_2(object sender, EventArgs e)
        {
            currentitem = Item.CropImage;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledTri;
        }

        private void ts_btn_aim_Click(object sender, EventArgs e)
        {
            ptb_main.Image = null;
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            currentitem = Item.Tri;
        }

        private void ptb_review_Click(object sender, EventArgs e)
        {

                    }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            currentitem = Item.Bucket;
        }

        private void khửMắtĐỏToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            currentitem = Item.RedEyeRemover;
        }

        private void ptb_main_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                
                Graphics g = Graphics.FromImage(ptb_main.Image);
                if (currentitem == Item.Bucket)
                {
                    Bitmap bmp1 = (Bitmap)ptb_main.Image;
                    
                    pictureBox1.Image = bmp1;
                }
                if (currentitem == Item.FilledTri || currentitem == Item.Tri)
                {
                    if (!fv1) {
                        int x = e.X * ratio;
                        int y = e.Y * ratio;
                        Point temp = new Point(x, y);
                        v1 = temp;
                        fv1 = true;
                    }
                    else if (fv1 && !fv2)
                    {
                        int x = e.X * ratio;
                        int y = e.Y * ratio;
                        Point temp = new Point(x, y);
                        v2 = temp;
                       
                        fv2 = true;
                    }
                    else if (fv1 && fv2 && !fv3)
                    {
                        int x = e.X * ratio;
                        int y = e.Y * ratio;
                        Point temp = new Point(x, y);

                        fv3 = true;
                        v3 = temp;
                    }
                }
                if (currentitem == Item.Text && drawtext)
                {
                    // font
                    if (ts_FontStyle.Text == "Regular")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text), FontStyle.Regular)
                   , new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                    }
                    else if (ts_FontStyle.Text == "Bold")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Bold), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                    }
                    else if (ts_FontStyle.Text == "Underline")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Underline), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                    }
                    else if (ts_FontStyle.Text == "Strikeout")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                    }
                    else if (ts_FontStyle.Text == "Italic")
                    {
                        g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                            FontStyle.Italic), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                    }

                    // shadow 
                    if (ts_TxtShadow.Text == "SE")
                    {
                        if (ts_FontStyle.Text == "Regular")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Bold")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Underline")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Strikeout")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Italic")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                    }
                    else if (ts_TxtShadow.Text == "SW")
                    {
                        if (ts_FontStyle.Text == "Regular")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Bold")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Underline")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Strikeout")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Italic")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio + 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                    }
                    else if (ts_TxtShadow.Text == "NE")
                    {
                        if (ts_FontStyle.Text == "Regular")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(paintcolor), new PointF(e.X, e.Y));
                        }
                        else if (ts_FontStyle.Text == "Bold")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Underline")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Strikeout")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Italic")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(e.X * ratio + 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                    }
                    else if (ts_TxtShadow.Text == "NW")
                    {
                        if (ts_FontStyle.Text == "Regular")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Regular), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Bold")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Bold), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Underline")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Underline), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Strikeout")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                        else if (ts_FontStyle.Text == "Italic")
                        {
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(e.X * ratio - 5, e.Y * ratio - 5));
                            g.DrawString(ts_txtToDraw.Text, new Font(ts_fontcb.Text, Convert.ToInt32(ts_FontSize.Text),
                                FontStyle.Italic), new SolidBrush(paintcolor), new PointF(e.X * ratio, e.Y * ratio));
                        }
                    }
                }

                Bitmap bmp = (Bitmap)ptb_main.Image;
                ptb_main.Invalidate();
                ori_filter = bmp.ToImage<Bgr, byte>();
                ori_rotate = ori_filter;
                g.Dispose();
                ptb_main.Refresh();
                draw = true;
                x = (e.X * ratio);
                y = (e.Y * ratio);
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void ptb_main_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = (e.X * ratio);
            ly = (e.Y * ratio);
            Graphics g = Graphics.FromImage(ptb_main.Image);

            Image texture1 = Image.FromFile("./images/texture1.png");
            Image texture2 = Image.FromFile("./images/texture2.png");
            Image texture3 = Image.FromFile("./images/texture3.png");
            Image texture4 = Image.FromFile("./images/texture4.png");
            TextureBrush trb1 = new TextureBrush(texture1);
            TextureBrush trb2 = new TextureBrush(texture2);
            TextureBrush trb3 = new TextureBrush(texture3);
            TextureBrush trb4 = new TextureBrush(texture4);
            Cursor.Current = Cursors.Cross;

            switch (currentitem)
            {


                case Item.FilledRect1:
                    g.FillRectangle(trb1, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;
                case Item.FilledRect:
                    g.FillRectangle(new SolidBrush(paintcolor), x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;
                case Item.FilledRect2:
                    g.FillRectangle(trb2, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledRect3:
                    g.FillRectangle(trb3, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledRect4:
                    g.FillRectangle(trb4, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.Rectangle:
                    g.DrawRectangle(new Pen(paintcolor, Convert.ToUInt16(ts_brushsize.Text)), x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.Line:
                    g.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, ly));
                    break;

                // fill ellipse
                case Item.FilledEll:
                    g.FillEllipse(new SolidBrush(paintcolor), x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledEll1:
                    g.FillEllipse(trb1, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledEll2:
                    g.FillEllipse(trb2, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledEll3:
                    g.FillEllipse(trb3, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledEll4:
                    g.FillEllipse(trb4, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.Ellipse:
                    g.DrawEllipse(new Pen(paintcolor, Convert.ToUInt16(ts_brushsize.Text)), x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;
                case Item.RedEyeRemover:
                    Bitmap tempp = (Bitmap)ptb_main.Image;
                    for (int i = x; i < lx; i++)
                    {
                        for (int j = y; j < ly ; j++)
                        {
                            Color pixel = tempp.GetPixel(i, j);
                            float redIntensity = ((float)pixel.R / ((pixel.G + pixel.B) / 2));
                            if (redIntensity > 1.5f)  // 1.5 because it gives the best results
                            {
                                // reduce red to the average of blue and green
                                tempp.SetPixel(i, j, Color.FromArgb((pixel.G + pixel.B) / 2, pixel.G, pixel.B));
                            }
                        }
                    }
                    ptb_main.Image = tempp;
                    break;
                case Item.CropImage:
                    
                    int w = lx - x;
                    int h = ly - y;


                    Bitmap bmp2 = (Bitmap)ptb_main.Image;
                    Bitmap crpImg = new Bitmap(w, h);
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            Color pxlclr = bmp2.GetPixel(x + i, y + j);
                            crpImg.SetPixel(i, j, pxlclr);
                        }
                    }
                    if (crpImg.Height > 1080)
                    {
                        ptb_main.Width = (int)crpImg.Width / 3;
                        ptb_main.Height = (int)crpImg.Height / 3;

                    }
                    else if (crpImg.Width > 940 || crpImg.Height > 497)
                    {
                        ptb_main.Width = (int)crpImg.Width / 2;
                        ptb_main.Height = (int)crpImg.Height / 2;

                    }

                    else
                    {
                        ptb_main.Width = (int)crpImg.Width / 1;
                        ptb_main.Height = (int)crpImg.Height / 1;
                    }
                    this.Invalidate();
                    ratio = crpImg.Width / ptb_main.Width;
                
                    ptb_main.Image = (Image)crpImg;
                    ptb_main.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case Item.FilledTri:
                    /*g.DrawRectangle(new Pen(paintcolor, 2), x, y, e.X * ratio - x, e.Y * ratio - y);
                    MessageBox.Show(x.ToString() + " " + y.ToString() + " " + e.X.ToString() + " " + e.Y.ToString() );*/
                    if (fv3)
                    {
                        Point point1 = v1;
                        Point point2 = v2;
                        Point point3 = v3;
                        Point[] points = { point1, point2, point3 };

                        g.FillPolygon(new SolidBrush(paintcolor), points);
                        /*g.DrawLine(new Pen(new SolidBrush(paintcolor)), point1, point2);
                        g.DrawLine(new Pen(new SolidBrush(paintcolor)), point1, point3);
                        g.DrawLine(new Pen(new SolidBrush(paintcolor)), point2, point3);*/
                        fv1 = fv2 = fv3 = false;
                    }
                    break;
                case Item.Tri:
                    if (fv3)
                    {
                        Point point1 = v1;
                        Point point2 = v2;
                        Point point3 = v3;
                        Point[] points = { point1, point2, point3 };

                        g.DrawPolygon(new Pen(paintcolor, Convert.ToUInt16(ts_brushsize.Text)), points);
                        /*g.DrawLine(new Pen(new SolidBrush(paintcolor)), point1, point2);
                        g.DrawLine(new Pen(new SolidBrush(paintcolor)), point1, point3);
                        g.DrawLine(new Pen(new SolidBrush(paintcolor)), point2, point3);*/
                        fv1 = fv2 = fv3 = false;
                    }
                    break;
            }
            Bitmap bmp = (Bitmap)ptb_main.Image;
            ptb_main.Refresh();
            ptb_main.Invalidate();
            ori_filter = bmp.ToImage<Bgr, byte>();
            ori_rotate = ori_filter;

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