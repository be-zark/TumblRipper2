using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TumblRipper2
{
	public class PleaseWait : Form
	{
		private IContainer components;

		private Label label1;

		public PleaseWait()
		{
			this.InitializeComponent();
		}

		public PleaseWait(string message)
		{
			this.InitializeComponent();
			this.setLabel(message);
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
			this.label1 = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(202, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(464, 151);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "PleaseWait";
			this.Text = "PleaseWait";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void setLabel(string l)
		{
			this.label1.Text = l;
		}
	}
}