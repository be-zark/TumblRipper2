using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using TumblRipper2.classes;

namespace TumblRipper2
{
	public class SettingsView : Form
	{
		private IContainer components;

		private Button button3;

		private TextBox txtFolder;

		private Label label1;

		private Button btnSave;

		private ListBox lstAllDisplayData;

		private ListBox lstDisplayData;

		private Button button1;

		private Button button2;

		private Label label2;

		private Label label3;

		private NumericUpDown numericUpDown1;

		private Label label4;

		private CheckBox checkBox1;

		private ToolTip toolTip1;

		private CheckBox chkResetHelp;

		private NumericUpDown txtRowHeight;

		private Label label5;

		private Button button4;

		public SettingsView()
		{
			this.InitializeComponent();
			this.txtFolder.Text = Settings.GetSettings().DefaultPath;
			this.numericUpDown1.Value = Settings.GetSettings().MaxThreads;
			this.txtRowHeight.Value = Settings.GetSettings().RowHeight;
			if (Settings.GetSettings().Licence == "free")
			{
				this.numericUpDown1.Value = decimal.One;
				this.numericUpDown1.Enabled = false;
			}
			this.lstAllDisplayData.DataSource = Settings.AllDisplayFields;
			this.lstDisplayData.DataSource = Settings.GetSettings().DisplayFields;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (this.chkResetHelp.Checked)
			{
				HelpMessages.ResetHelp();
			}
			Settings.GetSettings().DefaultPath = this.txtFolder.Text;
			Settings.GetSettings().MaxThreads = (int)this.numericUpDown1.Value;
			Settings.GetSettings().RowHeight = (int)this.txtRowHeight.Value;
			Settings.GetSettings().SaveSettings();
			Downloader.GetInstance().MaxConcurrentDownloads = (int)this.numericUpDown1.Value;
			base.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.lstDisplayData.SelectedIndex > -1)
			{
				Settings.GetSettings().DisplayFields.Remove(this.lstDisplayData.SelectedItem as DisplayField);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.lstAllDisplayData.SelectedIndex > -1)
			{
				Settings.GetSettings().DisplayFields.Add(this.lstAllDisplayData.SelectedItem as DisplayField);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
			{
				ShowNewFolderButton = true
			};
			folderBrowserDialog.ShowDialog();
			this.txtFolder.Text = folderBrowserDialog.SelectedPath;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", Settings.DirectorySettings);
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
			this.components = new System.ComponentModel.Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsView));
			this.button3 = new Button();
			this.txtFolder = new TextBox();
			this.label1 = new Label();
			this.btnSave = new Button();
			this.lstAllDisplayData = new ListBox();
			this.lstDisplayData = new ListBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.label2 = new Label();
			this.label3 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.label4 = new Label();
			this.checkBox1 = new CheckBox();
			this.toolTip1 = new ToolTip(this.components);
			this.chkResetHelp = new CheckBox();
			this.txtRowHeight = new NumericUpDown();
			this.label5 = new Label();
			this.button4 = new Button();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			((ISupportInitialize)this.txtRowHeight).BeginInit();
			base.SuspendLayout();
			this.button3.Location = new Point(232, 47);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "Browse";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.txtFolder.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.txtFolder.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
			this.txtFolder.Location = new Point(12, 48);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(214, 20);
			this.txtFolder.TabIndex = 5;
			this.toolTip1.SetToolTip(this.txtFolder, "Automatic suggestion for new ripped sites");
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Default Base Directory";
			this.btnSave.Location = new Point(242, 336);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			this.lstAllDisplayData.FormattingEnabled = true;
			this.lstAllDisplayData.Location = new Point(12, 200);
			this.lstAllDisplayData.Name = "lstAllDisplayData";
			this.lstAllDisplayData.Size = new System.Drawing.Size(120, 95);
			this.lstAllDisplayData.TabIndex = 9;
			this.lstDisplayData.FormattingEnabled = true;
			this.lstDisplayData.Location = new Point(187, 200);
			this.lstDisplayData.Name = "lstDisplayData";
			this.lstDisplayData.Size = new System.Drawing.Size(120, 95);
			this.lstDisplayData.TabIndex = 9;
			this.button1.Location = new Point(138, 251);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(43, 23);
			this.button1.TabIndex = 10;
			this.button1.Text = "<<";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new Point(138, 222);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(43, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = ">>";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 181);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Available Data";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(184, 181);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Shown Data";
			this.numericUpDown1.Location = new Point(13, 88);
			this.numericUpDown1.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
			this.numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(50, 20);
			this.numericUpDown1.TabIndex = 12;
			this.numericUpDown1.Value = new decimal(new int[] { 2, 0, 0, 0 });
			this.label4.AutoSize = true;
			this.label4.Location = new Point(69, 91);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(97, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Download Threads";
			this.checkBox1.AutoSize = true;
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new Point(12, 128);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(176, 17);
			this.checkBox1.TabIndex = 14;
			this.checkBox1.Text = "Post anonymous usage for stats";
			this.toolTip1.SetToolTip(this.checkBox1, "No personal information will be sent. Just the blog url you indexed and the amount of posts");
			this.checkBox1.UseVisualStyleBackColor = true;
			this.chkResetHelp.AutoSize = true;
			this.chkResetHelp.Location = new Point(12, 152);
			this.chkResetHelp.Name = "chkResetHelp";
			this.chkResetHelp.Size = new System.Drawing.Size(127, 17);
			this.chkResetHelp.TabIndex = 15;
			this.chkResetHelp.Text = "Reset help messages";
			this.chkResetHelp.UseVisualStyleBackColor = true;
			this.txtRowHeight.Location = new Point(12, 308);
			this.txtRowHeight.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
			this.txtRowHeight.Name = "txtRowHeight";
			this.txtRowHeight.Size = new System.Drawing.Size(51, 20);
			this.txtRowHeight.TabIndex = 16;
			this.txtRowHeight.Value = new decimal(new int[] { 15, 0, 0, 0 });
			this.label5.AutoSize = true;
			this.label5.Location = new Point(69, 310);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(83, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Row Height (px)";
			this.button4.Location = new Point(12, 335);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(127, 23);
			this.button4.TabIndex = 18;
			this.button4.Text = "Open settings folder";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(329, 381);
			base.Controls.Add(this.button4);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.txtRowHeight);
			base.Controls.Add(this.chkResetHelp);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.numericUpDown1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.lstDisplayData);
			base.Controls.Add(this.lstAllDisplayData);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.txtFolder);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "SettingsView";
			this.Text = "Settings";
			base.Load += new EventHandler(this.SettingsView_Load);
			((ISupportInitialize)this.numericUpDown1).EndInit();
			((ISupportInitialize)this.txtRowHeight).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void SettingsView_Load(object sender, EventArgs e)
		{
		}
	}
}