using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using TumblRipper2.classes;

namespace TumblRipper2
{
	public class HelpWindow : Form
	{
		private string _key;

		private IContainer components;

		private Button button1;

		private CheckBox checkBox1;

		private Label label1;

		private TableLayoutPanel tableLayoutPanel1;

		public HelpWindow()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				Settings.GetSettings().HiddenPopups.Add(this._key);
				Settings.GetSettings().SaveSettings();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(HelpWindow));
			this.button1 = new Button();
			this.checkBox1 = new CheckBox();
			this.label1 = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new Point(449, 112);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Close";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.checkBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new Point(3, 118);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(179, 17);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "Do not show this message again";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
			this.label1.Dock = DockStyle.Fill;
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(521, 105);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.button1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.checkBox1, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 76.81159f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 23.18841f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(527, 138);
			this.tableLayoutPanel1.TabIndex = 3;
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.button1;
			base.ClientSize = new System.Drawing.Size(527, 138);
			base.Controls.Add(this.tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "HelpWindow";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Help Window";
			base.TopMost = true;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}

		public void SetText(string t)
		{
			this._key = t;
			this.label1.Text = HelpMessages.Messages[t];
		}
	}
}