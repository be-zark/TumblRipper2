using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TumblRipper2
{
	public class ImportExport : Form
	{
		private IContainer components;

		private Button button1;

		private Button button2;

		public ImportExport()
		{
			this.InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
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
			this.button2 = new Button();
			base.SuspendLayout();
			this.button1.Location = new Point(41, 32);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Import";
			this.button1.UseVisualStyleBackColor = true;
			this.button2.Location = new Point(41, 79);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "Export";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(159, 156);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Name = "ImportExport";
			this.Text = "ImportExport";
			base.ResumeLayout(false);
		}
	}
}