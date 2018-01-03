using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2
{
	public class AddSite : Form
	{
		private Website w;

		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private TextBox txtUrl;

		private TextBox txtName;

		private TextBox txtFolder;

		private Button button1;

		private Button btnSave;

		private Button btnBrowse;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private Label label7;

		private Label label6;

		private Label label5;

		private Label lblPhotoCount;

		private CheckBox chkEnabled;

		private GroupBox groupBox1;

		private CheckBox chkEXIF;

		private CheckBox chkDownloadVideos;

		private CheckBox chkDownloadPhotosets;

		private CheckBox chkRename;

		private Button btnCancel;

		private TabPage tabPage3;

		private Label label4;

		private Button button2;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label9;

		private Label label10;

		private Label label11;

		private Label lblPosts;

		private Label lblPhotos;

		private Label lblVideos;

		private Label label12;

		public AddSite()
		{
			this.InitializeComponent();
			this.w = new Website()
			{
				FirstAdded = DateTime.Now
			};
			this.btnSave.Enabled = false;
			this.txtUrl.Focus();
			this.invalidTumblr();
		}

		public AddSite(Website w)
		{
			this.InitializeComponent();
			this.w = w;
			this.txtUrl.Text = w.Url;
			this.txtFolder.Text = w.Folder;
			this.txtName.Text = w.Name;
			this.chkEnabled.Checked = w.Viewchecked;
			this.chkDownloadPhotosets.Checked = w.DoPhotoSets;
			this.chkDownloadVideos.Checked = w.DoVideos;
			this.chkEXIF.Checked = w.DoEXIF;
			this.chkRename.Checked = w.RenameFiles;
			this.btnSave.Enabled = true;
			this.UpdateStatsTab();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			this.w.PhotoCountLoaded = 0;
			this.w.PhotoCount = 0;
			this.w.ViewnewItems = 0;
			this.w.Viewstatus = "";
			this.w.PostCount = 0;
			this.w.LastPost = "0";
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
			{
				ShowNewFolderButton = true
			};
			folderBrowserDialog.ShowDialog();
			this.txtFolder.Text = folderBrowserDialog.SelectedPath;
		}

		private void buttonCheck_Click(object sender, EventArgs e)
		{
			this.checkTumblr();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(this.txtFolder.Text))
			{
				Directory.CreateDirectory(this.txtFolder.Text);
			}
			this.w.Name = this.txtName.Text;
			this.w.Folder = this.txtFolder.Text;
			this.w.Url = this.txtUrl.Text;
			this.w.DoPhotoSets = this.chkDownloadPhotosets.Checked;
			this.w.DoEXIF = this.chkEXIF.Checked;
			this.w.DoVideos = this.chkDownloadVideos.Checked;
			this.w.RenameFiles = this.chkRename.Checked;
			this.w.Viewchecked = this.chkEnabled.Checked;
			Settings settings = Settings.GetSettings();
			settings.AddSite(this.w);
			settings.SaveSettings();
			base.Close();
		}

		private void checkTumblr()
		{
			string str = Ripper.Cleanurl(this.txtUrl.Text);
			this.txtUrl.Text = str;
			string title = Ripper.GetTitle(str);
			if (title == null)
			{
				MessageBox.Show("Invalid Tumblr");
				this.invalidTumblr();
			}
			else
			{
				this.txtName.Text = title;
				this.btnSave.Enabled = true;
				if (this.txtFolder.Text.Length == 0)
				{
					string str1 = Website.RemoveInvalidFilePathCharacters(title, "");
					this.txtFolder.Text = Path.Combine(Settings.GetSettings().DefaultPath, str1);
					this.validTumblr();
					return;
				}
			}
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AddSite));
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.txtUrl = new TextBox();
			this.txtName = new TextBox();
			this.txtFolder = new TextBox();
			this.button1 = new Button();
			this.btnSave = new Button();
			this.btnBrowse = new Button();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.btnCancel = new Button();
			this.groupBox1 = new GroupBox();
			this.chkEnabled = new CheckBox();
			this.chkRename = new CheckBox();
			this.chkEXIF = new CheckBox();
			this.chkDownloadVideos = new CheckBox();
			this.chkDownloadPhotosets = new CheckBox();
			this.tabPage3 = new TabPage();
			this.button2 = new Button();
			this.label4 = new Label();
			this.tabPage2 = new TabPage();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label9 = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.lblPhotoCount = new Label();
			this.lblPosts = new Label();
			this.lblPhotos = new Label();
			this.lblVideos = new Label();
			this.label12 = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(25, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(22, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Site URL";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(22, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Folder";
			this.txtUrl.Enabled = false;
			this.txtUrl.Location = new Point(25, 31);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(242, 20);
			this.txtUrl.TabIndex = 1;
			this.txtName.Location = new Point(25, 73);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(242, 20);
			this.txtName.TabIndex = 3;
			this.txtFolder.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.txtFolder.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
			this.txtFolder.Location = new Point(25, 119);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(242, 20);
			this.txtFolder.TabIndex = 4;
			this.button1.Location = new Point(276, 29);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Check";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.buttonCheck_Click);
			this.btnSave.Enabled = false;
			this.btnSave.Location = new Point(273, 283);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 20;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new EventHandler(this.buttonSave_Click);
			this.btnBrowse.Location = new Point(276, 117);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 5;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new EventHandler(this.buttonBrowse_Click);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(10);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(369, 356);
			this.tabControl1.TabIndex = 6;
			this.tabPage1.Controls.Add(this.btnCancel);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.btnBrowse);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.btnSave);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.txtUrl);
			this.tabPage1.Controls.Add(this.txtFolder);
			this.tabPage1.Controls.Add(this.txtName);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(361, 330);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Settings";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(273, 254);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 21;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.groupBox1.Controls.Add(this.chkEnabled);
			this.groupBox1.Controls.Add(this.chkRename);
			this.groupBox1.Controls.Add(this.chkEXIF);
			this.groupBox1.Controls.Add(this.chkDownloadVideos);
			this.groupBox1.Controls.Add(this.chkDownloadPhotosets);
			this.groupBox1.Location = new Point(25, 155);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
			this.groupBox1.Size = new System.Drawing.Size(242, 151);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			this.chkEnabled.AutoSize = true;
			this.chkEnabled.Checked = true;
			this.chkEnabled.CheckState = CheckState.Checked;
			this.chkEnabled.Location = new Point(13, 24);
			this.chkEnabled.Name = "chkEnabled";
			this.chkEnabled.Size = new System.Drawing.Size(65, 17);
			this.chkEnabled.TabIndex = 6;
			this.chkEnabled.Text = "Enabled";
			this.chkEnabled.UseVisualStyleBackColor = true;
			this.chkRename.AutoSize = true;
			this.chkRename.Checked = true;
			this.chkRename.CheckState = CheckState.Checked;
			this.chkRename.Location = new Point(13, 93);
			this.chkRename.Name = "chkRename";
			this.chkRename.Size = new System.Drawing.Size(170, 17);
			this.chkRename.TabIndex = 9;
			this.chkRename.Text = "Rename files using Post Name";
			this.chkRename.UseVisualStyleBackColor = true;
			this.chkEXIF.AutoSize = true;
			this.chkEXIF.Enabled = false;
			this.chkEXIF.Location = new Point(13, 116);
			this.chkEXIF.Name = "chkEXIF";
			this.chkEXIF.Size = new System.Drawing.Size(156, 17);
			this.chkEXIF.TabIndex = 10;
			this.chkEXIF.Text = "Add #Tags to EXIF (slower)";
			this.chkEXIF.UseVisualStyleBackColor = true;
			this.chkDownloadVideos.AutoSize = true;
			this.chkDownloadVideos.Location = new Point(13, 47);
			this.chkDownloadVideos.Name = "chkDownloadVideos";
			this.chkDownloadVideos.Size = new System.Drawing.Size(109, 17);
			this.chkDownloadVideos.TabIndex = 7;
			this.chkDownloadVideos.Text = "Download Videos";
			this.chkDownloadVideos.UseVisualStyleBackColor = true;
			this.chkDownloadPhotosets.AutoSize = true;
			this.chkDownloadPhotosets.Location = new Point(13, 70);
			this.chkDownloadPhotosets.Name = "chkDownloadPhotosets";
			this.chkDownloadPhotosets.Size = new System.Drawing.Size(163, 17);
			this.chkDownloadPhotosets.TabIndex = 8;
			this.chkDownloadPhotosets.Text = "Download Photosets (slower)";
			this.chkDownloadPhotosets.UseVisualStyleBackColor = true;
			this.tabPage3.Controls.Add(this.button2);
			this.tabPage3.Controls.Add(this.label4);
			this.tabPage3.Location = new Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(361, 330);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Advanced";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.button2.Location = new Point(8, 299);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Reset Blog";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click_1);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(70, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(208, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "No options yet.  Coming soon: Filters, etc...";
			this.tabPage2.Controls.Add(this.tableLayoutPanel1);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(5);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(361, 330);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Stats";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblPhotoCount, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.lblPosts, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblPhotos, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblVideos, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label12, 0, 3);
			this.tableLayoutPanel1.Location = new Point(8, 48);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(232, 100);
			this.tableLayoutPanel1.TabIndex = 1;
			this.label9.AutoSize = true;
			this.label9.Location = new Point(3, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(67, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "Total Photos";
			this.label10.AutoSize = true;
			this.label10.Location = new Point(3, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 13);
			this.label10.TabIndex = 2;
			this.label10.Text = "Total Posts";
			this.label11.AutoSize = true;
			this.label11.Location = new Point(3, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(66, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "Total Videos";
			this.lblPhotoCount.AutoSize = true;
			this.lblPhotoCount.Location = new Point(119, 60);
			this.lblPhotoCount.Name = "lblPhotoCount";
			this.lblPhotoCount.Size = new System.Drawing.Size(73, 13);
			this.lblPhotoCount.TabIndex = 0;
			this.lblPhotoCount.Text = "lblPhotoCount";
			this.lblPosts.AutoSize = true;
			this.lblPosts.Location = new Point(119, 0);
			this.lblPosts.Name = "lblPosts";
			this.lblPosts.Size = new System.Drawing.Size(43, 13);
			this.lblPosts.TabIndex = 4;
			this.lblPosts.Text = "lblPosts";
			this.lblPhotos.AutoSize = true;
			this.lblPhotos.Location = new Point(119, 20);
			this.lblPhotos.Name = "lblPhotos";
			this.lblPhotos.Size = new System.Drawing.Size(50, 13);
			this.lblPhotos.TabIndex = 5;
			this.lblPhotos.Text = "lblPhotos";
			this.lblVideos.AutoSize = true;
			this.lblVideos.Location = new Point(119, 40);
			this.lblVideos.Name = "lblVideos";
			this.lblVideos.Size = new System.Drawing.Size(49, 13);
			this.lblVideos.TabIndex = 6;
			this.lblVideos.Text = "lblVideos";
			this.label12.AutoSize = true;
			this.label12.Location = new Point(3, 60);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(79, 13);
			this.label12.TabIndex = 7;
			this.label12.Text = "Loaded Photos";
			this.label7.AutoSize = true;
			this.label7.Location = new Point(27, 151);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(0, 13);
			this.label7.TabIndex = 0;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(27, 114);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(0, 13);
			this.label6.TabIndex = 0;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(27, 76);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(0, 13);
			this.label5.TabIndex = 0;
			base.AcceptButton = this.btnSave;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(369, 356);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "AddSite";
			this.Text = "Edit Site";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void invalidTumblr()
		{
			this.txtName.Enabled = false;
			this.txtFolder.Enabled = false;
			this.btnSave.Enabled = false;
			this.btnBrowse.Enabled = false;
		}

		private void label8_Click(object sender, EventArgs e)
		{
			AddTumblrHelper addTumblrHelper = new AddTumblrHelper();
			if (addTumblrHelper.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string uRL = addTumblrHelper.URL;
				this.txtUrl.Text = addTumblrHelper.URL;
				this.checkTumblr();
			}
		}

		private void UpdateStatsTab()
		{
			try
			{
				Label str = this.lblPhotoCount;
				int photoCountLoaded = this.w.PhotoCountLoaded;
				str.Text = photoCountLoaded.ToString();
				Label label = this.lblPhotos;
				photoCountLoaded = this.w.PhotoCount;
				label.Text = photoCountLoaded.ToString();
				Label str1 = this.lblVideos;
				photoCountLoaded = this.w.VideoCount;
				str1.Text = photoCountLoaded.ToString();
				Label label1 = this.lblPosts;
				photoCountLoaded = this.w.PostCount;
				label1.Text = photoCountLoaded.ToString();
			}
			catch (Exception exception)
			{
			}
		}

		private void validTumblr()
		{
			this.txtName.Enabled = true;
			this.txtFolder.Enabled = true;
			this.btnSave.Enabled = true;
			this.btnBrowse.Enabled = true;
		}
	}
}