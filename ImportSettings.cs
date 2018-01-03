using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TumblRipper2.classes;

namespace TumblRipper2
{
	public class ImportSettings : Form
	{
		private IContainer components;

		private Button button1;

		private TextBox textBox1;

		public ImportSettings()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.MySettings = Settings.LoadSettingsString(this.textBox1.Text);
			}
			catch (Exception exception)
			{
			}
			foreach (Website site in Settings.GetSettings().Sites)
			{
				try
				{
					site.Folder = Path.Combine("c:\\Tumblr\\", Website.RemoveInvalidFilePathCharacters(site.Name, ""));
				}
				catch (Exception exception1)
				{
				}
			}
			base.Close();
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
			this.button1 = new Button();
			this.textBox1 = new TextBox();
			base.SuspendLayout();
			this.button1.Location = new Point(382, 206);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.textBox1.Location = new Point(65, 53);
			this.textBox1.MaxLength = 0;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(392, 147);
			this.textBox1.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(520, 294);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.button1);
			base.Name = "ImportSettings";
			this.Text = "ImportSettings";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}