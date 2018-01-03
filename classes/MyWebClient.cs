using System;
using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using TumblRipper2;

namespace TumblRipper2.classes
{
    internal class MyWebClient : WebClient
    {
        public bool KeepSession = true;

        private ToolStripProgressBar progressBar;

        public System.Net.CookieContainer CookieContainer
        {
            get;
            set;
        }

        public MyWebClient()
        {
            ServicePointManager.ServerCertificateValidationCallback = (object argument0, X509Certificate argument1, X509Chain argument2, SslPolicyErrors argument3) => true;
            base.Encoding = System.Text.Encoding.UTF8;
            MainWindow.Instance.Invoke(new MethodInvoker(() => {
                this.progressBar = new ToolStripProgressBar()
                {
                    AutoSize = false,
                    Width = 50,
                    AutoToolTip = true
                };
                MainWindow.GetToolStrip().Items.Add(this.progressBar);
            }));
            this.CookieContainer = new System.Net.CookieContainer();
            base.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.MyWebClient_DownloadProgressChanged);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            try
            {
                MainWindow.Instance.Invoke(new MethodInvoker(() => {
                    if (!MainWindow.GetToolStrip().IsDisposed)
                    {
                        MainWindow.GetToolStrip().Items.Remove(this.progressBar);
                    }
                }));
            }
            catch (Exception exception)
            {
            }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest webRequest = (HttpWebRequest)base.GetWebRequest(address);
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (this.KeepSession)
            {
                webRequest.CookieContainer = this.CookieContainer;
            }
            return webRequest;
        }

        private void MyWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            MainWindow.Instance.Invoke(new MethodInvoker(() => {
                this.progressBar.Visible = true;
                if (e.ProgressPercentage == 100)
                {
                    this.progressBar.Visible = false;
                }
                this.progressBar.ToolTipText = this.BaseAddress;
                this.progressBar.Value = e.ProgressPercentage;
            }));
        }
    }
}