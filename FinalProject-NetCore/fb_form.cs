using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
namespace FinalProject_NetCore
{
    public partial class fb_form : Form
    {
        public fb_form()
        {
            InitializeComponent();
        }

        private void fb_form_Load(object sender, EventArgs e)
        {
           
        }
        public static bool Post(string accesstoken, string status, string link = "")
        {
            try
            {
                FacebookClient fb = new FacebookClient(accesstoken);
                Dictionary<string, object> PostArgs = new Dictionary<string, object>();
                PostArgs["message"] = status;
                if (link != "")
                {
                    PostArgs["link"] = link;
                }
                fb.Post("/me/feed", PostArgs);
                return true;
            }
            catch   
            {
                return false;
            }
        }
        public static bool PostImage(string accesstoken, string status, string imagepath)
        {
            try
            {
                FacebookClient fb = new FacebookClient(accesstoken);
                var imgstream = File.OpenRead(imagepath);
                dynamic res = fb.Post("/me/photos", new
                {
                    message = status,
                    file = new FacebookMediaStream
                    {
                        ContentType = "image/jpg",
                        FileName = Path.GetFileName(imagepath)
                    }.SetValue(imgstream)
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
