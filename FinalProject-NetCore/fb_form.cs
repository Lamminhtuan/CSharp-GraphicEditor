using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
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
       

        private void button1_Click(object sender, EventArgs e)
        {
            
            var driverservice = EdgeDriverService.CreateDefaultService();
            driverservice.HideCommandPromptWindow = true;
            EdgeOptions options = new EdgeOptions();
            options.AddArguments("--disable-notifications");
            options.AddArguments("--disable-application-cache");
            var driver = new EdgeDriver(driverservice, options);
            driver.Navigate().GoToUrl("https://www.facebook.com/");
            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div/div/div/div[2]/div/div[1]/form/div[1]/div[1]/input")).SendKeys(txt_username.Text);
            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div/div/div/div[2]/div/div[1]/form/div[1]/div[2]/div/input")).SendKeys(txt_pass.Text);
            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[1]/div/div/div/div[2]/div/div[1]/form/div[2]/button")).Click();
            Thread.Sleep(2000);
            //driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[3]/div/div/div/div[1]/div[1]/div/div[2]/div/div/div/div[3]/div/div[2]/div/div/div/div[2]/div[2]")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[3]/div/div/div/div[1]/div[1]/div/div[2]/div/div/div/div[3]/div/div[2]/div/div/div/div[1]/div")).SendKeys(txt_caption.Text);
            var ele = driver.SwitchTo().ActiveElement();
            
            



        }
    }
}
