using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TumblRipper2.classes;

namespace TumblRipper2
{
	public class About : Form
	{
		private IContainer components;

		private PictureBox pictureBox1;

		private LinkLabel linkLabel1;

		private Label label1;

		private Button button1;

		private Label lblVersion;

		public About()
		{
			this.InitializeComponent();
			this.lblVersion.Text = string.Concat("Version : ", LicenceValidation.Version.ToString());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            ComponentResourceManager resources = new ComponentResourceManager(typeof(About));
			this.pictureBox1 = new PictureBox();
			this.linkLabel1 = new LinkLabel();
			this.label1 = new Label();
			this.button1 = new Button();
			this.lblVersion = new Label();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(12, 208);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(42, 41);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new Point(168, 72);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(69, 13);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "www.zark.be";
			this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			this.label1.AutoSize = true;
			this.label1.BackColor = Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(57, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 37);
			this.label1.TabIndex = 6;
			this.label1.Text = "TumblRipper";
			this.button1.Location = new Point(12, 143);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(260, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "check for update";
			this.button1.UseVisualStyleBackColor = true;
			this.lblVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new Point(61, 72);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(35, 13);
			this.lblVersion.TabIndex = 9;
			this.lblVersion.Text = "label2";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(284, 261);
			base.Controls.Add(this.lblVersion);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "About";
			this.Text = "About";
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("http://www.zark.be");
			}
			catch (Win32Exception win32Exception1)
			{
				Win32Exception win32Exception = win32Exception1;
				if (win32Exception.ErrorCode == -2147467259)
				{
					MessageBox.Show(win32Exception.Message);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.facebook.com/tumblRipper");
			}
			catch (Win32Exception win32Exception1)
			{
				Win32Exception win32Exception = win32Exception1;
				if (win32Exception.ErrorCode == -2147467259)
				{
					MessageBox.Show(win32Exception.Message);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}