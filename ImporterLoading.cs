using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TumblRipper2
{
	public class ImporterLoading : Form
	{
		private int i;

		private IContainer components;

		private ProgressBar progressBar1;

		private Label label1;

		public ImporterLoading()
		{
			this.InitializeComponent();
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
			this.progressBar1 = new ProgressBar();
			this.label1 = new Label();
			base.SuspendLayout();
			this.progressBar1.Location = new Point(43, 104);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(470, 23);
			this.progressBar1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(39, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(555, 170);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "ImporterLoading";
			this.Text = "ImporterLoading";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void setLoading(string s)
		{
			this.label1.Text = string.Concat("Loading ", s);
			int num = this.i + 1;
			this.i = num;
			this.progressBar1.Value = num;
		}

		public void setTotal(int val)
		{
			this.progressBar1.Maximum = val;
		}
	}
}