using Chromium;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            ChromiumWebBrowser.Initialize();
            InitializeComponent();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CfxRuntime.Shutdown();
        }
        public void CfxHelloWorld_Execute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            MessageBox.Show("调用C#方法成功");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(wb.ExecuteJavascript(textBox1.Text));
        }
        ChromiumWebBrowser wb = new ChromiumWebBrowser();
        private void Form1_Load(object sender, EventArgs e)
        {
            ChromiumWebBrowser cb = new ChromiumWebBrowser();
            wb = cb;
            wb.Location = new Point(0,50);
            wb.Width = Width;
            wb.Height = Height - 120;
            wb.GlobalObject.AddFunction("s_domain").Execute += new CfrV8HandlerExecuteEventHandler(CfxHelloWorld_Execute);
            Controls.Add(wb);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CfxWindowInfo windowInfo = new CfxWindowInfo();
                windowInfo.Style = WindowStyle.WS_OVERLAPPEDWINDOW | WindowStyle.WS_CLIPCHILDREN | WindowStyle.WS_CLIPSIBLINGS | WindowStyle.WS_VISIBLE;
                windowInfo.ParentWindow = IntPtr.Zero;
                windowInfo.WindowName = "开发人员工具";
                windowInfo.X = 200;
                windowInfo.Y = 200;
                windowInfo.Width = 800;
                windowInfo.Height = 600;
                wb.Browser.Host.ShowDevTools(windowInfo, new CfxClient(), new CfxBrowserSettings(), null);
            }
            catch
            {
                Console.WriteLine("请等待页面加载完成之后再打开调试器");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wb.LoadUrl(textBox2.Text);
        }
    }
}
