using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using TumblRipper2.classes.Generic;

namespace TumblRipper2
{
	public class AddTumblrHelper : Form
	{
		private int mode;

		private IContainer components;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private RadioButton radioButton3;

		private TextBox txtInput;

		private GroupBox groupBox1;

		private FlowLayoutPanel flowLayoutPanel1;

		private Label lblInput;

		private Label lblStatus;

		private Button btnCancel;

		private Button btnOK;

		private Button button1;

		private FlowLayoutPanel flowLayoutPanel2;

		private RadioButton radioButton4;

		public string URL
		{
			get;
			set;
		}

		public AddTumblrHelper()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			base.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.URL = "";
			if (this.mode == 0)
			{
				this.URL = string.Concat("http://", this.txtInput.Text, ".tumblr.com/");
			}
			if (this.mode == 2)
			{
				this.URL = string.Concat("http://www.tumblr.com/tagged/", this.txtInput.Text);
			}
			if (this.mode == 3)
			{
				this.URL = string.Concat("http://500px.com/", this.txtInput.Text);
			}
			this.URL = Ripper.Cleanurl(this.URL);
			string title = Ripper.GetTitle(this.URL);
			if (title != null)
			{
				this.lblStatus.Text = title;
				return;
			}
			this.lblStatus.Text = "error";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AddTumblrHelper));
			this.radioButton1 = new RadioButton();
			this.radioButton2 = new RadioButton();
			this.radioButton3 = new RadioButton();
			this.txtInput = new TextBox();
			this.groupBox1 = new GroupBox();
			this.flowLayoutPanel2 = new FlowLayoutPanel();
			this.radioButton4 = new RadioButton();
			this.flowLayoutPanel1 = new FlowLayoutPanel();
			this.lblInput = new Label();
			this.button1 = new Button();
			this.lblStatus = new Label();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.groupBox1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new Point(3, 3);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(81, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Tumblr Blog";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Enabled = false;
			this.radioButton2.Location = new Point(3, 49);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(117, 17);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Tumblr User's Likes";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new Point(3, 26);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(84, 17);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Tumblr Tags";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
			this.txtInput.Location = new Point(74, 3);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(100, 20);
			this.txtInput.TabIndex = 4;
			this.groupBox1.Controls.Add(this.flowLayoutPanel2);
			this.groupBox1.Location = new Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 142);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Choose what you want to rip";
			this.flowLayoutPanel2.Controls.Add(this.radioButton1);
			this.flowLayoutPanel2.Controls.Add(this.radioButton3);
			this.flowLayoutPanel2.Controls.Add(this.radioButton2);
			this.flowLayoutPanel2.Controls.Add(this.radioButton4);
			this.flowLayoutPanel2.Location = new Point(6, 18);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(146, 118);
			this.flowLayoutPanel2.TabIndex = 3;
			this.radioButton4.AutoSize = true;
			this.radioButton4.Location = new Point(3, 72);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(117, 17);
			this.radioButton4.TabIndex = 3;
			this.radioButton4.TabStop = true;
			this.radioButton4.Text = "500px Profile (Beta)";
			this.radioButton4.UseVisualStyleBackColor = true;
			this.radioButton4.CheckedChanged += new EventHandler(this.radioButton4_CheckedChanged);
			this.flowLayoutPanel1.Controls.Add(this.lblInput);
			this.flowLayoutPanel1.Controls.Add(this.txtInput);
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Location = new Point(12, 160);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(266, 32);
			this.flowLayoutPanel1.TabIndex = 6;
			this.lblInput.AutoSize = true;
			this.lblInput.Location = new Point(3, 6);
			this.lblInput.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.lblInput.Name = "lblInput";
			this.lblInput.Size = new System.Drawing.Size(65, 13);
			this.lblInput.TabIndex = 5;
			this.lblInput.Text = "Blog Name :";
			this.button1.Location = new Point(180, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(52, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Check";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new Point(15, 207);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(28, 13);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = " ......";
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(203, 242);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.Location = new Point(13, 242);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.button2_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(290, 283);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.lblStatus);
			base.Controls.Add(this.flowLayoutPanel1);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "AddTumblrHelper";
			this.Text = "Add Tumblr Helper";
			this.groupBox1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.lblInput.Text = "Blog name : ";
			this.mode = 0;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.lblInput.Text = "Username : ";
			this.mode = 1;
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			this.lblInput.Text = "Keyword : ";
			this.mode = 2;
		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{
			this.lblInput.Text = "Username : ";
			this.mode = 3;
			MessageBox.Show("Still in beta, it will download the user's uploads,commented,favorited and liked");
		}
	}
}