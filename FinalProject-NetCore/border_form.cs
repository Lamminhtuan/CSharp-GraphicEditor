using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace FinalProject_NetCore
{
    public partial class border_form : Form
    {
        public border_form()
        {
            InitializeComponent();
        }

        private void border_form_Load(object sender, EventArgs e)
        {
            borders.KhoiTao();
            for (int i = 0; i < borders.borderurls.Count; i++)
            {
                imageList1.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + borders.borderurls[i]));
            }
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                listView1.Items.Add("", i);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = borders.borderurls[listView1.SelectedIndices[0]];
            borders.borderhientai = Directory.GetCurrentDirectory() + path;
            this.Close();
        }
    }
}
