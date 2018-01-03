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
	public class Licence : Form
	{
		private IContainer components;

		private Label label1;

		private Label label2;

		private TextBox textBox1;

		private TextBox textBox2;

		private Button button1;

		private Button button2;

		private Panel panel1;

		private Panel panel2;

		private Label label4;

		private Label label5;

		private Button button3;

		public Licence()
		{
			this.InitializeComponent();
			if (Settings.GetSettings().Licence == "free")
			{
                LicenceValidation.ValidLicence(this.textBox1.Text, this.textBox2.Text);
            }
            this.panel2.Show();
			this.panel1.Hide();
			this.label5.Text = Settings.GetSettings().Serial;
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Licence));
			this.label1 = new Label();
			this.label2 = new Label();
			this.textBox1 = new TextBox();
			this.textBox2 = new TextBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.label5 = new Label();
			this.label4 = new Label();
			this.button3 = new Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Email";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(13, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Serial";
			this.textBox1.Location = new Point(62, 14);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 2;
			this.textBox2.Location = new Point(62, 40);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(308, 20);
			this.textBox2.TabIndex = 3;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new Point(376, 37);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Validate";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new Point(13, 124);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "Close";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Location = new Point(13, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(486, 83);
			this.panel1.TabIndex = 7;
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(486, 83);
			this.panel2.TabIndex = 5;
			this.panel2.Visible = false;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(171, 43);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(28, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "###";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(168, 14);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(146, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Thank you for your donation !";
			this.button3.Location = new Point(429, 127);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "Lost Serial";
			this.button3.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button2;
			base.ClientSize = new System.Drawing.Size(516, 162);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.button2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "Licence";
			this.Text = "Licence";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}