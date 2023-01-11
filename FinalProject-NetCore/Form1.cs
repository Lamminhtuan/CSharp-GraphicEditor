using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XPhoto;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace FinalProject_NetCore
{
    public partial class Form1 : Form
    {
        Image imgToPaste;
        Color hatchbg = Color.Black;
        Color hatchfg = Color.White;
        Color grad1 = Color.Black;
        Color grad2 = Color.White;
        LinearGradientMode mode;
        LinearGradientBrush lgb;
        HatchStyle style = new HatchStyle();
        bool usehatch = false;
        bool usegrad = false;
        List<string> imageurls = new List<string>();
        Image<Bgr, byte> ori;
        Image<Bgr, byte> ori_rotate;
        Image<Bgr, byte> ori_filter;
        int scalefactor = 20;
        Image<Bgr, byte> prev;
        Size prevsize;
        Color paintcolor = Color.Black;
        bool choose = false;
        bool draw = false;
        bool drawtext = false;
        Item currentitem;
        int x, y, lx, ly;
        public Pen crpPen = new Pen(Color.White);
        Size currentsize;
        Size orisize;
        float ratio;
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
            FilledRect, FilledRect1, FilledRect2, FilledRect3, FilledRect4, FilledRect5, FilledRect6, Rectangle,
            FilledEll, FilledEll1, FilledEll2, FilledEll3, FilledEll4, FilledEll5, FilledEll6, Ellipse,
            Line, Text, Brush, Pencil, eraser, ColorPicker, test, CropImage, FilledTri, Tri, None, RedEyeRemover, Bucket,
            FilledArrow, Arrow, FilledPenta, Penta, ZoomIn, ZoomOut, ImageInImage
        }
        private void AddToRecent(string url)
        {
            for (int i = 0; i < imageurls.Count; i++) {
                if (imageurls[i] == url)
                    return;

            }
            imageurls.Add(url);
        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentitem = Item.None;
            listView1.Visible = false;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AddToRecent(ofd.FileName);
                Image<Bgr, byte> img = new Image<Bgr, byte>(ofd.FileName);
                prev = img;

                if (img.Height >= 1080)
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



                ori = img;
                ori_rotate = ori;
                ori_filter = ori;
                orisize = ptb_main.Size;
                if (WindowState == FormWindowState.Maximized)
                    ptb_main.Size = orisize * 2;
                ratio = (float)img.Width / ptb_main.Width;
                currentsize = orisize;
                prevsize = currentsize;
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
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ptb_fg.BackColor = hatchfg;
            ptb_bg.BackColor = hatchbg;
           
            cb_hatch.Items.Add(HatchStyle.Cross.ToString());
            cb_hatch.Items.Add(HatchStyle.OutlinedDiamond.ToString());
            cb_hatch.Items.Add(HatchStyle.SmallCheckerBoard.ToString());
            cb_hatch.Items.Add(HatchStyle.SmallConfetti.ToString());
            cb_hatch.Items.Add(HatchStyle.Wave.ToString());
            cb_hatch.Items.Add(HatchStyle.ZigZag.ToString());
            cb_hatch.Items.Add(HatchStyle.Divot.ToString());
            cb_hatch.Items.Add(HatchStyle.Percent10.ToString());
            cb_hatch.Items.Add(HatchStyle.Sphere.ToString());
            cb_hatch.Items.Add(HatchStyle.HorizontalBrick.ToString());
            FontFamily[] ffm = FontFamily.Families;
            foreach (FontFamily f in ffm)
                ts_fontcb.Items.Add(f.GetName(1).ToString());
            for (int i = 8; i < 100; i += 2)
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
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                Bitmap rotated = bmp;
                int oldwidth = ptb_main.Width;
                ptb_main.Width = ptb_main.Height;
                ptb_main.Height = oldwidth;
                ori_rotate = rotated.ToImage<Bgr, byte>();
                ori_filter = ori_rotate;
                prevsize = currentsize;

                currentsize = ptb_main.Size;
                ptb_main.Image = bmp;
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void xoayPhảiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Bitmap rotated = bmp;
                int oldwidth = ptb_main.Width;
                ptb_main.Width = ptb_main.Height;
                ptb_main.Height = oldwidth;
                ori_rotate = rotated.ToImage<Bgr, byte>();
                ori_filter = ori_rotate;
                prevsize = currentsize;
                currentsize = ptb_main.Size;
                ptb_main.Image = bmp;
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void xoay180ĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                Bitmap rotated = bmp;

                ori_rotate = rotated.ToImage<Bgr, byte>();
                ori_filter = ori_rotate;
                ptb_main.Image = bmp;
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void lậtGươngNgangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Bitmap rotated = bmp;

                ori_rotate = rotated.ToImage<Bgr, byte>();
                ori_filter = ori_rotate;
                ptb_main.Image = bmp;
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void lậtGươngDọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Bitmap rotated = bmp;

                ori_rotate = rotated.ToImage<Bgr, byte>();
                ori_filter = ori_rotate;
                ptb_main.Image = bmp;
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void ảnhXámToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
                Image<Gray, byte> gray = img.Convert<Gray, byte>();
                ori_filter = gray.Convert<Bgr, byte>();
                ptb_main.Image = gray.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

   
        private void âmBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> neg = img.Not();
                ori_filter = neg;
                ptb_main.Image = neg.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void làmMờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> neg = img.SmoothGaussian(25);
                ori_filter = neg;
                ptb_main.Image = neg.ToBitmap();
            }
            catch { MessageBox.Show("Chưa có ảnh!"); }
        }

        private void mờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> neg = img.SmoothBilateral(25, 85, 85);

                ori_filter = neg;
                ptb_main.Image = neg.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void vẽBútChìĐenTrắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

                Mat output_gray = new Mat();
                Mat output = new Mat();
                CvInvoke.PencilSketch(img, output_gray, output, 60, (float)0.07, (float)0.07);
                ori_filter = output_gray.ToImage<Bgr, byte>();
                ptb_main.Image = output_gray.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void vẽBútChìMàuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

                Mat output_gray = new Mat();
                Mat output = new Mat();
                CvInvoke.PencilSketch(img, output_gray, output, 60, (float)0.07, (float)0.07);
                ori_filter = output.ToImage<Bgr, byte>();
                ptb_main.Image = output.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void hDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();


                Mat output = new Mat();
                CvInvoke.DetailEnhance(img, output, (float)12.0, (float)0.15);
                ori_filter = output.ToImage<Bgr, byte>();
                ptb_main.Image = output.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void ptb_main_Click(object sender, EventArgs e)
        {

        }

        private void mùaHèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ori_rotate.ToBitmap();
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
                float[,] array = new float[2, 4] { { 0, 64, 128, 256 }, { 0, 80, 160, 256 } };
                Matrix<double> kernel = new Matrix<double>(3, 3);
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
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }



        private void cânBằngHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();

                Image<Gray, byte> gray = img.Convert<Gray, byte>();
                Mat output = new Mat();
                CvInvoke.EqualizeHist(gray, output);

                ori_filter = output.ToImage<Bgr, byte>();
                ptb_main.Image = output.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void cânBằngTrắngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();


                Mat output = new Mat();

                var wb = new LearningBasedWB();
                wb.BalanceWhite(img, output);
                ori_filter = output.ToImage<Bgr, byte>();
                ptb_main.Image = output.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void tranhSơnDầuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = (Bitmap)ptb_main.Image;
                prev = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> img = bmp.ToImage<Bgr, byte>();
                Image<Bgr, byte> neg = img.SmoothBilateral(25, 85, 85);
                Mat output = new Mat();
                XPhotoInvoke.OilPainting(img, output, 7, 1);
                ori_filter = output.ToImage<Bgr, byte>();
                ptb_main.Image = output.ToBitmap();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void xóaNềnXanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap inp = (Bitmap)ptb_main.Image;
                prev = inp.ToImage<Bgr, byte>();
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
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info_form f = new info_form();
            f.ShowDialog();
        }

        private void ảnhGốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap oribitmap = ori.ToBitmap();
                ptb_main.Image = ori.ToBitmap();
                bar_brightness.Value = 0;
                bar_contrast.Value = 100;
                if (WindowState == FormWindowState.Normal)
                {
                    ptb_main.Size = orisize;
                }
                if (WindowState == FormWindowState.Maximized)
                {
                    ptb_main.Size = orisize * 2;


                }
                ratio = (int)oribitmap.Width / orisize.Width;
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
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

        private void ptb_adjust_Click(object sender, EventArgs e)
        {
            pn_hatch.Visible = false;
            pn_adjust.Visible = true;
            pn_color.Visible = false;
        }

        private void ptb_color_Click(object sender, EventArgs e)
        {
            pn_adjust.Visible = false;
            pn_hatch.Visible = false;
            pn_color.Visible = true;
        }

        private void ptb_main_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw && currentitem != Item.CropImage)
            {

                Graphics g = Graphics.FromImage(ptb_main.Image);
                Bitmap cur = (Bitmap)ptb_main.Image;
                prev = cur.ToImage<Bgr, byte>();
                switch (currentitem)
                {
                    case Item.Brush:
                        g.FillEllipse(new SolidBrush(paintcolor), (int)Math.Round(e.X * ratio), (int)Math.Round(e.Y * ratio), Convert.ToUInt16(ts_brushsize.Text), Convert.ToUInt16(ts_brushsize.Text));
                        break;

                    case Item.eraser:
                        g.FillEllipse(new SolidBrush(ptb_main.BackColor), (int)Math.Round(e.X * ratio), (int)Math.Round(e.Y * ratio),
                            Convert.ToUInt16(ts_brushsize.Text), Convert.ToUInt16(ts_brushsize.Text));
                        break;
                    case Item.Pencil:

                        g.FillEllipse(new SolidBrush(paintcolor), (int)Math.Round(e.X * ratio), (int)Math.Round(e.Y * ratio), 5, 5);

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
            lb_tool.Text = "Đường thẳng";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect;
            lb_tool.Text = "Hình chữ nhật đặc";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            currentitem = Item.Rectangle;
            lb_tool.Text = "Hình chữ nhật rỗng";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            currentitem = Item.Ellipse;
            lb_tool.Text = "Hình ellipse rỗng";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll;
            lb_tool.Text = "Hình ellipse đặc";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            currentitem = Item.Brush;
            lb_tool.Text = "Cọ vẽ";
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            currentitem = Item.Pencil;
            lb_tool.Text = "Bút chì";
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            currentitem = Item.ColorPicker;
            lb_tool.Text = "Công cụ chọn màu";
        }
        private void validate(Bitmap bm, Stack<Point> sp, int px, int py, Color old_Color, Color new_color)
        {
            Color cx = bm.GetPixel(px, py);
            if (cx == old_Color)
            {
                sp.Push(new Point(px, py));
                bm.SetPixel(px, py, new_color);
            }
        }
        public void Fill(Bitmap bm, int px, int py, Color new_color)
        {
            Color old_color = bm.GetPixel(px, py);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(px, py));
            bm.SetPixel(px, py, new_color);
            if (old_color == new_color) return;

            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if ((pt.X > 0) && (pt.Y > 0) && (pt.X < bm.Width - 1) && (pt.Y < bm.Height - 1))
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_color);
                    validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_color);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_color);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_color);
                }
            }
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
            else if (currentitem == Item.Bucket)
            {
                Bitmap old = (Bitmap)ptb_main.Image;
                Bitmap b = (Bitmap)ptb_main.Image;
                prev = old.ToImage<Bgr, byte>();
                Color color = b.GetPixel(e.X, e.Y);
                Fill(b, (int)Math.Round(e.X * ratio), (int)Math.Round(e.Y * ratio), paintcolor);
                ptb_main.Image = b;
                Bitmap bmp = (Bitmap)ptb_main.Image;
                ori_filter = bmp.ToImage<Bgr, byte>();

                ori_rotate = ori_filter;
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            currentitem = Item.eraser;
            lb_tool.Text = "Cục tẩy";
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            currentitem = Item.Text;
            lb_tool.Text = "Viết chữ";
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


        private void chiaSẻQuaFacebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fb_form f = new fb_form();
            f.ShowDialog();

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
            if (ptb_main.Image != null)
            {
                Bitmap bmp = ori.ToBitmap();
                ratio = (float)ori.Width / ptb_main.Width;
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
            lb_tool.Text = "Cắt hình";
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledTri;
            lb_tool.Text = "Tam giác đặc";
        }

        private void ts_btn_aim_Click(object sender, EventArgs e)
        {
            currentitem = Item.ImageInImage;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgToPaste = Image.FromFile(ofd.FileName);
                Bitmap tmp = new Bitmap(imgToPaste, new Size(imgToPaste.Width / 2, imgToPaste.Height / 2));
                imgToPaste = (Image)tmp;
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            currentitem = Item.Tri;
            lb_tool.Text = "Tam giác rỗng";
        }

        private void ptb_review_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            currentitem = Item.Bucket;
            lb_tool.Text = "Đổ màu";
        }

        private void khửMắtĐỏToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            currentitem = Item.RedEyeRemover;
            lb_tool.Text = "Khử mắt đỏ";
        }

        private void lb_tool_Click(object sender, EventArgs e)
        {

        }

        private void tạoMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new_form nf = new new_form();
            nf.ShowDialog();
            try
            {
                if (newcanvas.height >= 1080)
                {
                    ptb_main.Width = (int)newcanvas.width / 3;
                    ptb_main.Height = (int)newcanvas.height / 3;


                }
                else if (newcanvas.width >= 940 || newcanvas.height >= 497)
                {
                    ptb_main.Width = (int)newcanvas.width / 2;
                    ptb_main.Height = (int)newcanvas.height / 2;


                }

                else
                {
                    ptb_main.Width = (int)newcanvas.width / 1;
                    ptb_main.Height = (int)newcanvas.height / 1;

                }

                orisize = ptb_main.Size;

                prevsize = orisize;
                Bitmap new1 = new Bitmap(newcanvas.width, newcanvas.height);
                Graphics g = Graphics.FromImage(new1);
                g.FillRectangle(new SolidBrush(newcanvas.bgcolor), new Rectangle(0, 0, newcanvas.width, newcanvas.height));

                ptb_main.Image = new1;
                ptb_main.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap bmp = (Bitmap)ptb_main.Image;
                ptb_main.Invalidate();
                prev = bmp.ToImage<Bgr, byte>();
                ori_filter = bmp.ToImage<Bgr, byte>();
                ori_rotate = ori_filter;
                ori = ori_filter;
                if (WindowState == FormWindowState.Maximized && new1.Width < 1900 && new1.Height < 1000)
                    ptb_main.Size = new1.Size;
                else
                    ptb_main.Size = orisize * 2;
                ratio = (float)new1.Width / ptb_main.Width;
                g.Dispose();
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = prev.ToBitmap();
                ptb_main.Image = prev.ToBitmap();
                currentsize = prevsize;
                ptb_main.Size = currentsize;
                Bitmap bmp1 = prev.ToBitmap();
                ori_filter = prev;
                ori_rotate = ori_filter;
                ptb_main.Invalidate();
                ratio = (float)prev.Width / ptb_main.Width;
            }
            catch { MessageBox.Show("Chưa có ảnh!"); }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledPenta;
            lb_tool.Text = "Ngũ giác đặc";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            currentitem = Item.Arrow;
            lb_tool.Text = "Mũi tên trái rỗng";
        }
        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            currentitem = Item.Penta;
            lb_tool.Text = "Ngũ giác rỗng";
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledArrow;
            lb_tool.Text = "Mũi tên phải đặc";
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            currentitem = Item.ZoomIn;
            float constantWH = (ptb_main.Image.Width / ptb_main.Image.Height);
            ptb_main.Height += Convert.ToInt32(scalefactor / constantWH);
            ptb_main.Width += scalefactor;
            Bitmap bori = ori.ToBitmap();
            ratio = (float)bori.Width / ptb_main.Width;
            lb_tool.Text = "Phóng to";
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            currentitem = Item.ZoomOut;
            ptb_main.Height -= scalefactor;
            ptb_main.Width -= scalefactor;
            Bitmap bori = ori.ToBitmap();
            ratio = (float)bori.Width / ptb_main.Width;
            lb_tool.Text = "Thu nhỏ";
        }

        private void gầnĐâyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Visible = true;
            listView1.Items.Clear();
            imageList1.Images.Clear();
            for (int i = 0; i < imageurls.Count; i++) {
                imageList1.Images.Add(Image.FromFile(imageurls[i]));
            }
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < imageurls.Count; i++)
            {
                listView1.Items.Add("", i);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = imageurls[listView1.SelectedIndices[0]];
            Bitmap open = new Bitmap(path);
            Image<Bgr, byte> img = open.ToImage<Bgr, byte>();
            prev = open.ToImage<Bgr, byte>();

            if (img.Height >= 1080)
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


            ori = img;
            ori_rotate = ori;
            ori_filter = ori;
            orisize = ptb_main.Size;
            if (WindowState == FormWindowState.Maximized)
                ptb_main.Size = orisize * 2;
            currentsize = orisize;
            prevsize = currentsize;
            ratio = (float)img.Width / ptb_main.Width;

            ptb_main.BackColor = Color.Transparent;

            ptb_main.Image = img.ToBitmap();

            ptb_main.Invalidate();
            listView1.Visible = false;
        }

        private void ptb_hatch_Click(object sender, EventArgs e)
        {
            pn_adjust.Visible = false;
            pn_color.Visible = false;
            pn_hatch.Visible = true;
        }

        private void pn_hatch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_bg_Click(object sender, EventArgs e)
        {
            ColorDialog cld = new ColorDialog();
            if (cld.ShowDialog() == DialogResult.OK)
            {
                hatchbg = cld.Color;
                ptb_bg.BackColor = hatchbg;
            }
        }

        private void btn_fg_Click(object sender, EventArgs e)
        {
            ColorDialog cld = new ColorDialog();
            if (cld.ShowDialog() == DialogResult.OK)
            {
                hatchfg = cld.Color;
                ptb_fg.BackColor = hatchfg;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //cb_hatch.Items.Add(HatchStyle.Cross.ToString());
            //cb_hatch.Items.Add(HatchStyle.OutlinedDiamond.ToString());
            //cb_hatch.Items.Add(HatchStyle.SmallCheckerBoard.ToString());
            //cb_hatch.Items.Add(HatchStyle.SmallConfetti.ToString());
            //cb_hatch.Items.Add(HatchStyle.Wave.ToString());
            //cb_hatch.Items.Add(HatchStyle.ZigZag.ToString());
            //cb_hatch.Items.Add(HatchStyle.Divot.ToString());
            //cb_hatch.Items.Add(HatchStyle.Percent10.ToString());
            //cb_hatch.Items.Add(HatchStyle.Sphere.ToString());
            //cb_hatch.Items.Add(HatchStyle.HorizontalBrick.ToString());
            usehatch = true;
            usegrad = false;
            string str = cb_hatch.Text;
            switch (str) {
                case "LargeGrid":
                    style = HatchStyle.LargeGrid;
                    break;
                case "OutlinedDiamond":
                    style = HatchStyle.OutlinedDiamond;
                    break;
                case "SmallCheckerBoard":
                    style = HatchStyle.SmallCheckerBoard;
                    break;
                case "SmallConfetti":
                    style = HatchStyle.SmallConfetti;
                    break;
                case "Wave":
                    style = HatchStyle.Wave;
                    break;
                case "ZigZag":
                    style = HatchStyle.ZigZag;
                    break;
                case "Divot":
                    style = HatchStyle.Divot;
                    break;
                case "Percent10":
                    style = HatchStyle.Percent10;
                    break;
                case "Sphere":
                    style = HatchStyle.Sphere;
                    break;
                case "HorizontalBrick":
                    style = HatchStyle.HorizontalBrick;
                    break;
            }
            Graphics g = ptb_hatchtest.CreateGraphics();
            HatchBrush b = new HatchBrush(style, hatchfg, hatchbg);
            g.FillRectangle(b, 0, 0, ptb_hatchtest.Width, ptb_hatchtest.Height);
            g.Dispose();
        }

        private void ptb_hatchtest_Click(object sender, EventArgs e)
        {

        }

        private void bar_red_Scroll(object sender, EventArgs e)
        {
            ptb_ccolor.BackColor = Color.FromArgb(bar_red.Value, bar_green.Value, bar_blue.Value);
            lb_bar_r.Text = bar_red.Value.ToString();
            paintcolor = Color.FromArgb(bar_red.Value, bar_green.Value, bar_blue.Value);
        }

        private void bar_green_Scroll(object sender, EventArgs e)
        {
            ptb_ccolor.BackColor = Color.FromArgb(bar_red.Value, bar_green.Value, bar_blue.Value);
            lb_bar_g.Text = bar_red.Value.ToString();
            paintcolor = Color.FromArgb(bar_red.Value, bar_green.Value, bar_blue.Value);
        }

        private void bar_blue_Scroll(object sender, EventArgs e)
        {
            ptb_ccolor.BackColor = Color.FromArgb(bar_red.Value, bar_green.Value, bar_blue.Value);
            lb_bar_b.Text = bar_red.Value.ToString();
            paintcolor = Color.FromArgb(bar_red.Value, bar_green.Value, bar_blue.Value);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            border_form bf = new border_form();
            bf.ShowDialog();
            
            try
            {

                
                
                Bitmap cur = (Bitmap)ptb_main.Image;
                Bitmap border = new Bitmap(Image.FromFile(borders.borderhientai), cur.Size);
               
                prev = cur.ToImage<Bgr, byte>();
                Graphics g = Graphics.FromImage(cur);
                g.DrawImage(border, 0, 0, cur.Width, cur.Height);
                g.Dispose();
            }
            catch
            {

            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect5;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledRect6;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll5;
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            currentitem = Item.FilledEll6;
        }

        private void ptb_main_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Bitmap cur = (Bitmap)ptb_main.Image;
                prev = cur.ToImage<Bgr, byte>();
                Graphics g = Graphics.FromImage(ptb_main.Image);
                if (currentitem == Item.Bucket)
                {
                    Bitmap bmp1 = (Bitmap)ptb_main.Image;
                    
                    pictureBox1.Image = bmp1;
                }
                if (currentitem == Item.FilledTri || currentitem == Item.Tri)
                {
                    if (!fv1) {
                        int x = (int)Math.Round(e.X * ratio);
                        int y = (int)Math.Round(e.Y * ratio);
                        Point temp = new Point(x, y);
                        v1 = temp;
                        fv1 = true;
                    }
                    else if (fv1 && !fv2)
                    {
                        int x = (int)Math.Round(e.X * ratio);
                        int y = (int)Math.Round(e.Y * ratio);
                        Point temp = new Point(x, y);
                        v2 = temp;
                       
                        fv2 = true;
                    }
                    else if (fv1 && fv2 && !fv3)
                    {
                        int x = (int)Math.Round(e.X * ratio);
                        int y = (int)Math.Round(e.Y * ratio);
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
                if(currentitem == Item.ImageInImage)
                     g.DrawImage(imgToPaste, new PointF(e.X * ratio, e.Y * ratio));
       

                Bitmap bmp = (Bitmap)ptb_main.Image;
                ptb_main.Invalidate();
                ori_filter = bmp.ToImage<Bgr, byte>();
                ori_rotate = ori_filter;
                g.Dispose();
                ptb_main.Refresh();
                draw = true;
                x = (int)Math.Round(e.X * ratio);
                y = (int)Math.Round(e.Y * ratio);
            }
            catch
            {
                MessageBox.Show("Chưa có ảnh!");
            }
        }

        private void ptb_main_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = (int)Math.Round(e.X * ratio);
            ly = (int)Math.Round(e.Y * ratio);
            Bitmap cur = (Bitmap)ptb_main.Image;

            Graphics g = Graphics.FromImage(ptb_main.Image);

            Image texture1 = Image.FromFile("./images/texture1.png");
            Image texture2 = Image.FromFile("./images/texture2.png");
            Image texture3 = Image.FromFile("./images/texture3.png");
            Image texture4 = Image.FromFile("./images/texture4.png");
            TextureBrush trb1 = new TextureBrush(texture1);
            TextureBrush trb2 = new TextureBrush(texture2);
            TextureBrush trb3 = new TextureBrush(texture3);
            TextureBrush trb4 = new TextureBrush(texture4);
            HatchBrush hb = new HatchBrush(HatchStyle.SolidDiamond, Color.Orchid);

            Rectangle rect = new Rectangle(x, y, lx, ly);
            LinearGradientBrush lgb = new LinearGradientBrush(rect, Color.PaleVioletRed, Color.Blue, LinearGradientMode.Horizontal);
            Cursor.Current = Cursors.Cross;

            switch (currentitem)
            {

                case Item.FilledRect1:
                    g.FillRectangle(trb1, x, y, (int)Math.Round(e.X * ratio - x), (int)Math.Round(e.Y * ratio - y));
                    break;
                case Item.FilledRect:
                    if (usehatch)
                        g.FillRectangle(new HatchBrush(style, hatchfg, hatchbg), x, y, e.X * ratio - x, e.Y * ratio - y);
                    else
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
                case Item.FilledEll5:
                    g.FillEllipse(hb, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.FilledEll6:
                    g.FillEllipse(lgb, x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;
                case Item.Rectangle:
                    g.DrawRectangle(new Pen(paintcolor, Convert.ToUInt16(ts_brushsize.Text)), x, y, e.X * ratio - x, e.Y * ratio - y);
                    break;

                case Item.Line:
                    g.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, ly));
                    break;

                // fill ellipse
                case Item.FilledEll:
                    if (usehatch)

                        g.FillEllipse(new HatchBrush(style, hatchfg, hatchbg), x, y, e.X * ratio - x, e.Y * ratio - y);
                  
                    else
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
                case Item.FilledPenta:

                    int w_s = lx - x;
                    int h_s = ly - y;
                    Point p1 = new Point(x + w_s / 2, y);
                    Point p2 = new Point(lx + w_s * 1 / 5, y + h_s * 7 / 16);
                    Point p3 = new Point(lx, ly);
                    Point p4 = new Point(lx - w_s, ly);
                    Point p5 = new Point(x - w_s * 1 / 5, y + h_s * 7 / 16);
                    Point[] ps = { p1, p2, p3, p4, p5 };
                    if (usehatch)
                        g.FillPolygon(new HatchBrush(style, hatchfg, hatchbg), ps);
                    else
                        g.FillPolygon(new SolidBrush(paintcolor), ps);
                    break;
                
                case Item.Penta:
                    int w_s2 = lx - x;
                    int h_s2 = ly - y;
                    Point p12 = new Point(x + w_s2 / 2, y);
                    Point p22 = new Point(lx + w_s2 * 1 / 5, y + h_s2 * 7 / 16);
                    Point p32 = new Point(lx, ly);
                    Point p42 = new Point(lx - w_s2, ly);
                    Point p52 = new Point(x - w_s2 * 1 / 5, y + h_s2 * 7 / 16);
                    Point[] ps2 = { p12, p22, p32, p42, p52 };
                    g.DrawPolygon(new Pen(paintcolor, Convert.ToUInt16(ts_brushsize.Text)), ps2);
                    
                    break;
                case Item.Arrow:
                    int w_ar = lx - x;
                    int h_ar = ly - y;
                    Point ar1 = new Point(x, y + h_ar * 1/3);
                    Point ar2 = new Point(x + w_ar * 3 / 4, y + h_ar * 1 / 3);
                    Point ar3 = new Point(x + w_ar * 3 / 4, y);
                    Point ar4 = new Point(lx, ly - h_ar * 1 / 2);
                    Point ar5 = new Point(lx - w_ar * 1 / 4, ly);
                    Point ar6 = new Point(lx - w_ar * 1 / 4, ly - h_ar * 1 / 3);
                    Point ar7 = new Point(x, y + h_ar * 2/3);
                    Point[] par = { ar1, ar2, ar3, ar4, ar5, ar6, ar7 };
                    
                    g.DrawPolygon(new Pen(paintcolor, Convert.ToUInt16(ts_brushsize.Text)), par);
                    break;
                case Item.FilledArrow:
                    int w_ar1 = lx - x;
                    int h_ar1 = ly - y;
                    Point ar11 = new Point(x, y + h_ar1 * 1 / 3);
                    Point ar21 = new Point(x + w_ar1 * 3 / 4, y + h_ar1 * 1 / 3);
                    Point ar31 = new Point(x + w_ar1 * 3 / 4, y);
                    Point ar41 = new Point(lx, ly - h_ar1 * 1 / 2);
                    Point ar51 = new Point(lx - w_ar1 * 1 / 4, ly);
                    Point ar61 = new Point(lx - w_ar1 * 1 / 4, ly - h_ar1 * 1 / 3);
                    Point ar71 = new Point(x, y + h_ar1 * 2 / 3);
                    Point[] par1 = { ar11, ar21, ar31, ar41, ar51, ar61, ar71 };
                    if (usehatch)
                        g.FillPolygon(new HatchBrush(style, hatchfg, hatchbg), par1);
                    else
                        g.FillPolygon(new SolidBrush(paintcolor), par1);
                    break;
                //case Item.ImageInImage:
                //    g.DrawImage(imgToPaste, new Point(lx, ly));
                //    break;
                case Item.CropImage:
                    
                    int w = lx - x;
                    int h = ly - y;

                    prevsize = currentsize;
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
                    currentsize = ptb_main.Size;
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
                        if (usehatch)
                            g.FillPolygon(new HatchBrush(style, hatchfg, hatchbg), points);
                        else
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