using AeroWizard;
//using Microsoft.Win32.DesktopWindowManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;
using Vanara.Interop.DesktopWindowManager;

namespace TumblRipper2
{
	public class AddWizard : Form
	{
		private Website _website;

		private IContainer components;

		private WizardControl wizardControl1;

		private WizardPage wizardPage1;

		private Button button2;

		private Button button1;

		private WizardPage wizardPageTumblr;

		private WizardPage wizardPage500;

		private GlassExtenderProvider glassExtenderProvider1;

		private WizardPage wizardPageValidate;

		private FlowLayoutPanel flowLayoutPanel1;

		private Button button4;

		private Label label1;

		private Label label2;

		private Label label3;

		private WizardPage wizardPageInstagram;

		private FlowLayoutPanel flowLayoutPanel2;

		private Label label4;

		private Button button5;

		private Button button6;

		private WizardPage wizardPageTumblrBlog;

		private WizardPage wizardPageTumblrTags;

		private FlowLayoutPanel flowLayoutPanel3;

		private Label label6;

		private TextBox txtTumblrBlogUrl;

		private Label label7;

		private FlowLayoutPanel flowLayoutPanel4;

		private Label label8;

		private TextBox txtTumblrTag;

		private Label label9;

		private FlowLayoutPanel flowLayoutPanel5;

		private Label label10;

		private TextBox txt500pxUsername;

		private Label label11;

		private Label lblError500px;

		private Label lblErrorTumblrBlog;

		private Label lblErrorInstagram;

		private FlowLayoutPanel flowLayoutPanel6;

		private Label label12;

		private TextBox txtInstagram;

		private Label label13;

		private Label lblErrorInstragram;

		private GroupBox grpTumblrBlog;

		private FlowLayoutPanel flowLayoutPanel7;

		private CheckBox chkTumblrPhotoSets;

		private FlowLayoutPanel flowLayoutPanel8;

		private Label label5;

		private TextBox txtName;

		private Label label14;

		private Label label15;

		private TextBox txtFolder;

		private Label label16;

		private GroupBox groupBox1;

		private FlowLayoutPanel flowLayoutPanel9;

		private CheckBox chkRenameFiles;

		private FlowLayoutPanel flowLayoutPanel10;

		private Button button3;

		private CheckBox chkTumblrVideos;

		private GroupBox groupBox2;

		private FlowLayoutPanel flowLayoutPanel11;

		private CheckBox checkBox4;

		private CheckBox checkBox5;

		private CheckBox checkBox6;

		private CheckBox checkBox7;

		private Button button7;

		private Label label17;

		private WizardPage wizardPageFlickr;

		private FlowLayoutPanel flowLayoutPanel12;

		private Label label18;

		private TextBox txtFlickr;

		private Label label19;

		public AddWizard()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.wizardControl1.NextPage(this.wizardPageTumblr, false);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.wizardControl1.NextPage(this.wizardPage500, false);
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
			this.wizardControl1.NextPage(this.wizardPageInstagram, false);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			this.wizardControl1.NextPage(this.wizardPageTumblrBlog, false);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			this.wizardControl1.NextPage(this.wizardPageTumblrTags, false);
		}

		private void button7_Click(object sender, EventArgs e)
		{
			this.wizardControl1.NextPage(this.wizardPageFlickr, false);
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
			this.wizardControl1 = new WizardControl();
			this.wizardPage1 = new WizardPage();
			this.flowLayoutPanel1 = new FlowLayoutPanel();
			this.button1 = new Button();
			this.label1 = new Label();
			this.button2 = new Button();
			this.label2 = new Label();
			this.button4 = new Button();
			this.label3 = new Label();
			this.button7 = new Button();
			this.label17 = new Label();
			this.wizardPageTumblr = new WizardPage();
			this.flowLayoutPanel2 = new FlowLayoutPanel();
			this.label4 = new Label();
			this.button5 = new Button();
			this.button6 = new Button();
			this.wizardPageTumblrBlog = new WizardPage();
			this.lblErrorTumblrBlog = new Label();
			this.flowLayoutPanel3 = new FlowLayoutPanel();
			this.label6 = new Label();
			this.txtTumblrBlogUrl = new TextBox();
			this.label7 = new Label();
			this.grpTumblrBlog = new GroupBox();
			this.flowLayoutPanel7 = new FlowLayoutPanel();
			this.chkTumblrPhotoSets = new CheckBox();
			this.chkTumblrVideos = new CheckBox();
			this.wizardPageValidate = new WizardPage();
			this.flowLayoutPanel8 = new FlowLayoutPanel();
			this.label5 = new Label();
			this.txtName = new TextBox();
			this.label14 = new Label();
			this.label15 = new Label();
			this.flowLayoutPanel10 = new FlowLayoutPanel();
			this.txtFolder = new TextBox();
			this.button3 = new Button();
			this.label16 = new Label();
			this.groupBox1 = new GroupBox();
			this.flowLayoutPanel9 = new FlowLayoutPanel();
			this.chkRenameFiles = new CheckBox();
			this.wizardPageTumblrTags = new WizardPage();
			this.flowLayoutPanel4 = new FlowLayoutPanel();
			this.label8 = new Label();
			this.txtTumblrTag = new TextBox();
			this.label9 = new Label();
			this.wizardPage500 = new WizardPage();
			this.flowLayoutPanel5 = new FlowLayoutPanel();
			this.label10 = new Label();
			this.txt500pxUsername = new TextBox();
			this.label11 = new Label();
			this.groupBox2 = new GroupBox();
			this.flowLayoutPanel11 = new FlowLayoutPanel();
			this.checkBox4 = new CheckBox();
			this.checkBox5 = new CheckBox();
			this.checkBox6 = new CheckBox();
			this.checkBox7 = new CheckBox();
			this.lblError500px = new Label();
			this.wizardPageInstagram = new WizardPage();
			this.lblErrorInstragram = new Label();
			this.lblErrorInstagram = new Label();
			this.flowLayoutPanel6 = new FlowLayoutPanel();
			this.label12 = new Label();
			this.txtInstagram = new TextBox();
			this.label13 = new Label();
			this.wizardPageFlickr = new WizardPage();
			this.flowLayoutPanel12 = new FlowLayoutPanel();
			this.label18 = new Label();
			this.txtFlickr = new TextBox();
			this.label19 = new Label();
			this.glassExtenderProvider1 = new GlassExtenderProvider();
			((ISupportInitialize)this.wizardControl1).BeginInit();
			this.wizardPage1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.wizardPageTumblr.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.wizardPageTumblrBlog.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.grpTumblrBlog.SuspendLayout();
			this.flowLayoutPanel7.SuspendLayout();
			this.wizardPageValidate.SuspendLayout();
			this.flowLayoutPanel8.SuspendLayout();
			this.flowLayoutPanel10.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.flowLayoutPanel9.SuspendLayout();
			this.wizardPageTumblrTags.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.wizardPage500.SuspendLayout();
			this.flowLayoutPanel5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.flowLayoutPanel11.SuspendLayout();
			this.wizardPageInstagram.SuspendLayout();
			this.flowLayoutPanel6.SuspendLayout();
			this.wizardPageFlickr.SuspendLayout();
			this.flowLayoutPanel12.SuspendLayout();
			base.SuspendLayout();
			this.wizardControl1.Location = new Point(0, 0);
			this.wizardControl1.Name = "wizardControl1";
			this.wizardControl1.Pages.Add(this.wizardPage1);
			this.wizardControl1.Pages.Add(this.wizardPageTumblr);
			this.wizardControl1.Pages.Add(this.wizardPageTumblrBlog);
			this.wizardControl1.Pages.Add(this.wizardPageTumblrTags);
			this.wizardControl1.Pages.Add(this.wizardPage500);
			this.wizardControl1.Pages.Add(this.wizardPageInstagram);
			this.wizardControl1.Pages.Add(this.wizardPageFlickr);
			this.wizardControl1.Pages.Add(this.wizardPageValidate);
			this.wizardControl1.Size = new System.Drawing.Size(518, 409);
			this.wizardControl1.TabIndex = 0;
			this.wizardControl1.Title = "Add New Site";
			this.wizardPage1.AllowNext = false;
			this.wizardPage1.Controls.Add(this.flowLayoutPanel1);
			this.wizardPage1.Name = "wizardPage1";
			this.wizardPage1.ShowNext = false;
			this.wizardPage1.Size = new System.Drawing.Size(471, 254);
			this.wizardPage1.TabIndex = 0;
			this.wizardPage1.Text = "Blog Type";
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.button2);
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Controls.Add(this.button4);
			this.flowLayoutPanel1.Controls.Add(this.label3);
			this.flowLayoutPanel1.Controls.Add(this.button7);
			this.flowLayoutPanel1.Controls.Add(this.label17);
			this.flowLayoutPanel1.Dock = DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(471, 254);
			this.flowLayoutPanel1.TabIndex = 3;
			this.button1.Location = new Point(3, 15);
			this.button1.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(136, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Tumblr";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(246, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Backup your Tumblr Blog, or explore site tags";
			this.button2.Location = new Point(3, 71);
			this.button2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(136, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "500px";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 97);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(323, 15);
			this.label2.TabIndex = 4;
			this.label2.Text = "Backup your uploads,likes,favorites and commented photos";
			this.button4.Location = new Point(3, 127);
			this.button4.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(136, 23);
			this.button4.TabIndex = 2;
			this.button4.Text = "Instagram";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 153);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(174, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Backup your instagram uploads";
			this.button7.Location = new Point(3, 183);
			this.button7.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(136, 23);
			this.button7.TabIndex = 6;
			this.button7.Text = "Flickr";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new EventHandler(this.button7_Click);
			this.label17.AutoSize = true;
			this.label17.Location = new Point(3, 209);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(149, 15);
			this.label17.TabIndex = 7;
			this.label17.Text = "Backup your Flickr uploads";
			this.wizardPageTumblr.AllowNext = false;
			this.wizardPageTumblr.Controls.Add(this.flowLayoutPanel2);
			this.wizardPageTumblr.Name = "wizardPageTumblr";
			this.wizardPageTumblr.ShowNext = false;
			this.wizardPageTumblr.Size = new System.Drawing.Size(471, 254);
			this.wizardPageTumblr.TabIndex = 1;
			this.wizardPageTumblr.Text = "Tumblr";
			this.flowLayoutPanel2.Controls.Add(this.label4);
			this.flowLayoutPanel2.Controls.Add(this.button5);
			this.flowLayoutPanel2.Controls.Add(this.button6);
			this.flowLayoutPanel2.Dock = DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new Point(0, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(471, 254);
			this.flowLayoutPanel2.TabIndex = 0;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(248, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "What kind of Tumblr do you wish to Backup ?";
			this.button5.Location = new Point(3, 30);
			this.button5.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(139, 23);
			this.button5.TabIndex = 1;
			this.button5.Text = "Blog";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new EventHandler(this.button5_Click);
			this.button6.Location = new Point(3, 71);
			this.button6.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(139, 23);
			this.button6.TabIndex = 2;
			this.button6.Text = "Tags";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new EventHandler(this.button6_Click);
			this.wizardPageTumblrBlog.Controls.Add(this.lblErrorTumblrBlog);
			this.wizardPageTumblrBlog.Controls.Add(this.flowLayoutPanel3);
			this.wizardPageTumblrBlog.Name = "wizardPageTumblrBlog";
			this.wizardPageTumblrBlog.NextPage = this.wizardPageValidate;
			this.wizardPageTumblrBlog.Size = new System.Drawing.Size(471, 254);
			this.wizardPageTumblrBlog.TabIndex = 6;
			this.wizardPageTumblrBlog.Text = "Tumblr Blog";
			this.wizardPageTumblrBlog.Commit += new EventHandler<WizardPageConfirmEventArgs>(this.wizardPageTumblrBlog_Commit);
			this.lblErrorTumblrBlog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.lblErrorTumblrBlog.AutoSize = true;
			this.lblErrorTumblrBlog.Location = new Point(0, 239);
			this.lblErrorTumblrBlog.Name = "lblErrorTumblrBlog";
			this.lblErrorTumblrBlog.Size = new System.Drawing.Size(10, 15);
			this.lblErrorTumblrBlog.TabIndex = 1;
			this.lblErrorTumblrBlog.Text = " ";
			this.flowLayoutPanel3.Controls.Add(this.label6);
			this.flowLayoutPanel3.Controls.Add(this.txtTumblrBlogUrl);
			this.flowLayoutPanel3.Controls.Add(this.label7);
			this.flowLayoutPanel3.Controls.Add(this.grpTumblrBlog);
			this.flowLayoutPanel3.Dock = DockStyle.Top;
			this.flowLayoutPanel3.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new Point(0, 0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(471, 199);
			this.flowLayoutPanel3.TabIndex = 0;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(177, 15);
			this.label6.TabIndex = 0;
			this.label6.Text = "Please type in the url of the blog";
			this.txtTumblrBlogUrl.Location = new Point(3, 30);
			this.txtTumblrBlogUrl.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.txtTumblrBlogUrl.Name = "txtTumblrBlogUrl";
			this.txtTumblrBlogUrl.Size = new System.Drawing.Size(305, 23);
			this.txtTumblrBlogUrl.TabIndex = 1;
			this.txtTumblrBlogUrl.Text = "http://";
			this.label7.AutoSize = true;
			this.label7.Location = new Point(3, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(204, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "ex: http://artisnotamedia.tumblr.com";
			this.grpTumblrBlog.Controls.Add(this.flowLayoutPanel7);
			this.grpTumblrBlog.Location = new Point(3, 86);
			this.grpTumblrBlog.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.grpTumblrBlog.Name = "grpTumblrBlog";
			this.grpTumblrBlog.Size = new System.Drawing.Size(305, 100);
			this.grpTumblrBlog.TabIndex = 3;
			this.grpTumblrBlog.TabStop = false;
			this.grpTumblrBlog.Text = "Options";
			this.flowLayoutPanel7.Controls.Add(this.chkTumblrPhotoSets);
			this.flowLayoutPanel7.Controls.Add(this.chkTumblrVideos);
			this.flowLayoutPanel7.Dock = DockStyle.Fill;
			this.flowLayoutPanel7.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel7.Location = new Point(3, 19);
			this.flowLayoutPanel7.Name = "flowLayoutPanel7";
			this.flowLayoutPanel7.Size = new System.Drawing.Size(299, 78);
			this.flowLayoutPanel7.TabIndex = 1;
			this.chkTumblrPhotoSets.AutoSize = true;
			this.chkTumblrPhotoSets.Location = new Point(3, 3);
			this.chkTumblrPhotoSets.Name = "chkTumblrPhotoSets";
			this.chkTumblrPhotoSets.Size = new System.Drawing.Size(194, 19);
			this.chkTumblrPhotoSets.TabIndex = 0;
			this.chkTumblrPhotoSets.Text = "Download Photosets (2x slower)";
			this.chkTumblrPhotoSets.UseVisualStyleBackColor = true;
			this.chkTumblrVideos.AutoSize = true;
			this.chkTumblrVideos.Location = new Point(3, 28);
			this.chkTumblrVideos.Name = "chkTumblrVideos";
			this.chkTumblrVideos.Size = new System.Drawing.Size(118, 19);
			this.chkTumblrVideos.TabIndex = 1;
			this.chkTumblrVideos.Text = "Download Videos";
			this.chkTumblrVideos.UseVisualStyleBackColor = true;
			this.wizardPageValidate.Controls.Add(this.flowLayoutPanel8);
			this.wizardPageValidate.Name = "wizardPageValidate";
			this.wizardPageValidate.Size = new System.Drawing.Size(471, 254);
			this.wizardPageValidate.TabIndex = 3;
			this.wizardPageValidate.Text = "Validation";
			this.wizardPageValidate.Commit += new EventHandler<WizardPageConfirmEventArgs>(this.wizardPageValidate_Commit);
			this.wizardPageValidate.Initialize += new EventHandler<WizardPageInitEventArgs>(this.wizardPageValidate_Initialize);
			this.flowLayoutPanel8.Controls.Add(this.label5);
			this.flowLayoutPanel8.Controls.Add(this.txtName);
			this.flowLayoutPanel8.Controls.Add(this.label14);
			this.flowLayoutPanel8.Controls.Add(this.label15);
			this.flowLayoutPanel8.Controls.Add(this.flowLayoutPanel10);
			this.flowLayoutPanel8.Controls.Add(this.label16);
			this.flowLayoutPanel8.Controls.Add(this.groupBox1);
			this.flowLayoutPanel8.Dock = DockStyle.Fill;
			this.flowLayoutPanel8.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel8.Location = new Point(0, 0);
			this.flowLayoutPanel8.Name = "flowLayoutPanel8";
			this.flowLayoutPanel8.Size = new System.Drawing.Size(471, 254);
			this.flowLayoutPanel8.TabIndex = 0;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Name";
			this.txtName.Location = new Point(3, 18);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(173, 23);
			this.txtName.TabIndex = 1;
			this.label14.AutoSize = true;
			this.label14.Location = new Point(3, 44);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(10, 15);
			this.label14.TabIndex = 2;
			this.label14.Text = " ";
			this.label15.AutoSize = true;
			this.label15.Location = new Point(3, 59);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(104, 15);
			this.label15.TabIndex = 3;
			this.label15.Text = "Folder to save files";
			this.flowLayoutPanel10.Controls.Add(this.txtFolder);
			this.flowLayoutPanel10.Controls.Add(this.button3);
			this.flowLayoutPanel10.Location = new Point(3, 77);
			this.flowLayoutPanel10.Name = "flowLayoutPanel10";
			this.flowLayoutPanel10.Size = new System.Drawing.Size(274, 29);
			this.flowLayoutPanel10.TabIndex = 7;
			this.txtFolder.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.txtFolder.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
			this.txtFolder.Location = new Point(3, 3);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(173, 23);
			this.txtFolder.TabIndex = 4;
			this.button3.Location = new Point(182, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "&Browse";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.label16.AutoSize = true;
			this.label16.Location = new Point(3, 109);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(10, 15);
			this.label16.TabIndex = 6;
			this.label16.Text = " ";
			this.groupBox1.Controls.Add(this.flowLayoutPanel9);
			this.groupBox1.Location = new Point(3, 127);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 100);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			this.flowLayoutPanel9.Controls.Add(this.chkRenameFiles);
			this.flowLayoutPanel9.Dock = DockStyle.Fill;
			this.flowLayoutPanel9.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel9.Location = new Point(3, 19);
			this.flowLayoutPanel9.Name = "flowLayoutPanel9";
			this.flowLayoutPanel9.Size = new System.Drawing.Size(194, 78);
			this.flowLayoutPanel9.TabIndex = 0;
			this.chkRenameFiles.AutoSize = true;
			this.chkRenameFiles.Checked = true;
			this.chkRenameFiles.CheckState = CheckState.Checked;
			this.chkRenameFiles.Location = new Point(3, 3);
			this.chkRenameFiles.Name = "chkRenameFiles";
			this.chkRenameFiles.Size = new System.Drawing.Size(184, 19);
			this.chkRenameFiles.TabIndex = 0;
			this.chkRenameFiles.Text = "Rename files using post name";
			this.chkRenameFiles.UseVisualStyleBackColor = true;
			this.wizardPageTumblrTags.Controls.Add(this.flowLayoutPanel4);
			this.wizardPageTumblrTags.Name = "wizardPageTumblrTags";
			this.wizardPageTumblrTags.NextPage = this.wizardPageValidate;
			this.wizardPageTumblrTags.Size = new System.Drawing.Size(471, 254);
			this.wizardPageTumblrTags.TabIndex = 7;
			this.wizardPageTumblrTags.Text = "Tumblr Tags";
			this.wizardPageTumblrTags.Commit += new EventHandler<WizardPageConfirmEventArgs>(this.wizardPageTumblrTags_Commit);
			this.flowLayoutPanel4.Controls.Add(this.label8);
			this.flowLayoutPanel4.Controls.Add(this.txtTumblrTag);
			this.flowLayoutPanel4.Controls.Add(this.label9);
			this.flowLayoutPanel4.Dock = DockStyle.Fill;
			this.flowLayoutPanel4.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel4.Location = new Point(0, 0);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(471, 254);
			this.flowLayoutPanel4.TabIndex = 0;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(3, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(133, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "Please select a tag to rip";
			this.txtTumblrTag.Location = new Point(3, 30);
			this.txtTumblrTag.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.txtTumblrTag.Name = "txtTumblrTag";
			this.txtTumblrTag.Size = new System.Drawing.Size(133, 23);
			this.txtTumblrTag.TabIndex = 1;
			this.label9.AutoSize = true;
			this.label9.Location = new Point(3, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 15);
			this.label9.TabIndex = 2;
			this.label9.Text = "ex: wallpaper";
			this.wizardPage500.Controls.Add(this.flowLayoutPanel5);
			this.wizardPage500.Controls.Add(this.lblError500px);
			this.wizardPage500.Name = "wizardPage500";
			this.wizardPage500.NextPage = this.wizardPageValidate;
			this.wizardPage500.Size = new System.Drawing.Size(471, 254);
			this.wizardPage500.TabIndex = 2;
			this.wizardPage500.Text = "500px";
			this.wizardPage500.Commit += new EventHandler<WizardPageConfirmEventArgs>(this.wizardPage500_Commit);
			this.flowLayoutPanel5.Controls.Add(this.label10);
			this.flowLayoutPanel5.Controls.Add(this.txt500pxUsername);
			this.flowLayoutPanel5.Controls.Add(this.label11);
			this.flowLayoutPanel5.Controls.Add(this.groupBox2);
			this.flowLayoutPanel5.Dock = DockStyle.Top;
			this.flowLayoutPanel5.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel5.Location = new Point(0, 0);
			this.flowLayoutPanel5.Name = "flowLayoutPanel5";
			this.flowLayoutPanel5.Size = new System.Drawing.Size(471, 226);
			this.flowLayoutPanel5.TabIndex = 0;
			this.label10.AutoSize = true;
			this.label10.Location = new Point(3, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(231, 15);
			this.label10.TabIndex = 0;
			this.label10.Text = "Please type in the username on 500px.com";
			this.txt500pxUsername.Location = new Point(3, 30);
			this.txt500pxUsername.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.txt500pxUsername.Name = "txt500pxUsername";
			this.txt500pxUsername.Size = new System.Drawing.Size(132, 23);
			this.txt500pxUsername.TabIndex = 1;
			this.label11.AutoSize = true;
			this.label11.Location = new Point(3, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(55, 15);
			this.label11.TabIndex = 2;
			this.label11.Text = "ex: bollzy";
			this.groupBox2.Controls.Add(this.flowLayoutPanel11);
			this.groupBox2.Location = new Point(3, 86);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 122);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Options";
			this.flowLayoutPanel11.Controls.Add(this.checkBox4);
			this.flowLayoutPanel11.Controls.Add(this.checkBox5);
			this.flowLayoutPanel11.Controls.Add(this.checkBox6);
			this.flowLayoutPanel11.Controls.Add(this.checkBox7);
			this.flowLayoutPanel11.Dock = DockStyle.Fill;
			this.flowLayoutPanel11.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel11.Location = new Point(3, 19);
			this.flowLayoutPanel11.Name = "flowLayoutPanel11";
			this.flowLayoutPanel11.Size = new System.Drawing.Size(194, 100);
			this.flowLayoutPanel11.TabIndex = 0;
			this.checkBox4.AutoSize = true;
			this.checkBox4.Checked = true;
			this.checkBox4.CheckState = CheckState.Checked;
			this.checkBox4.Enabled = false;
			this.checkBox4.Location = new Point(3, 3);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(159, 19);
			this.checkBox4.TabIndex = 0;
			this.checkBox4.Text = "Download posted photos";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox5.AutoSize = true;
			this.checkBox5.Checked = true;
			this.checkBox5.CheckState = CheckState.Checked;
			this.checkBox5.Enabled = false;
			this.checkBox5.Location = new Point(3, 28);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(151, 19);
			this.checkBox5.TabIndex = 1;
			this.checkBox5.Text = "Download Liked photos";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox6.AutoSize = true;
			this.checkBox6.Checked = true;
			this.checkBox6.CheckState = CheckState.Checked;
			this.checkBox6.Enabled = false;
			this.checkBox6.Location = new Point(3, 53);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(151, 19);
			this.checkBox6.TabIndex = 2;
			this.checkBox6.Text = "Download Fav'd photos";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.checkBox7.AutoSize = true;
			this.checkBox7.Checked = true;
			this.checkBox7.CheckState = CheckState.Checked;
			this.checkBox7.Enabled = false;
			this.checkBox7.Location = new Point(3, 78);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(190, 19);
			this.checkBox7.TabIndex = 3;
			this.checkBox7.Text = "Download Commented photos";
			this.checkBox7.UseVisualStyleBackColor = true;
			this.lblError500px.AutoSize = true;
			this.lblError500px.Location = new Point(3, 211);
			this.lblError500px.Name = "lblError500px";
			this.lblError500px.Size = new System.Drawing.Size(0, 15);
			this.lblError500px.TabIndex = 3;
			this.wizardPageInstagram.Controls.Add(this.lblErrorInstragram);
			this.wizardPageInstagram.Controls.Add(this.lblErrorInstagram);
			this.wizardPageInstagram.Controls.Add(this.flowLayoutPanel6);
			this.wizardPageInstagram.Name = "wizardPageInstagram";
			this.wizardPageInstagram.NextPage = this.wizardPageValidate;
			this.wizardPageInstagram.Size = new System.Drawing.Size(471, 254);
			this.wizardPageInstagram.TabIndex = 4;
			this.wizardPageInstagram.Text = "Instagram";
			this.wizardPageInstagram.Commit += new EventHandler<WizardPageConfirmEventArgs>(this.wizardPageInstagram_Commit);
			this.lblErrorInstragram.AutoSize = true;
			this.lblErrorInstragram.Location = new Point(0, 210);
			this.lblErrorInstragram.Name = "lblErrorInstragram";
			this.lblErrorInstragram.Size = new System.Drawing.Size(0, 15);
			this.lblErrorInstragram.TabIndex = 2;
			this.lblErrorInstagram.AutoSize = true;
			this.lblErrorInstagram.Location = new Point(0, 210);
			this.lblErrorInstagram.Name = "lblErrorInstagram";
			this.lblErrorInstagram.Size = new System.Drawing.Size(0, 15);
			this.lblErrorInstagram.TabIndex = 1;
			this.flowLayoutPanel6.Controls.Add(this.label12);
			this.flowLayoutPanel6.Controls.Add(this.txtInstagram);
			this.flowLayoutPanel6.Controls.Add(this.label13);
			this.flowLayoutPanel6.Dock = DockStyle.Top;
			this.flowLayoutPanel6.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel6.Location = new Point(0, 0);
			this.flowLayoutPanel6.Name = "flowLayoutPanel6";
			this.flowLayoutPanel6.Size = new System.Drawing.Size(471, 203);
			this.flowLayoutPanel6.TabIndex = 0;
			this.label12.AutoSize = true;
			this.label12.Location = new Point(3, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(227, 15);
			this.label12.TabIndex = 0;
			this.label12.Text = "Please type in the username on Instagram";
			this.txtInstagram.Location = new Point(3, 30);
			this.txtInstagram.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.txtInstagram.Name = "txtInstagram";
			this.txtInstagram.Size = new System.Drawing.Size(139, 23);
			this.txtInstagram.TabIndex = 1;
			this.label13.AutoSize = true;
			this.label13.Location = new Point(3, 56);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(94, 15);
			this.label13.TabIndex = 2;
			this.label13.Text = "ex: britneyspears";
			this.wizardPageFlickr.Controls.Add(this.flowLayoutPanel12);
			this.wizardPageFlickr.Name = "wizardPageFlickr";
			this.wizardPageFlickr.Size = new System.Drawing.Size(471, 254);
			this.wizardPageFlickr.TabIndex = 8;
			this.wizardPageFlickr.Text = "Flickr";
			this.wizardPageFlickr.Commit += new EventHandler<WizardPageConfirmEventArgs>(this.wizardPageFlickr_Commit);
			this.flowLayoutPanel12.Controls.Add(this.label18);
			this.flowLayoutPanel12.Controls.Add(this.txtFlickr);
			this.flowLayoutPanel12.Controls.Add(this.label19);
			this.flowLayoutPanel12.Dock = DockStyle.Fill;
			this.flowLayoutPanel12.FlowDirection = FlowDirection.TopDown;
			this.flowLayoutPanel12.Location = new Point(0, 0);
			this.flowLayoutPanel12.Name = "flowLayoutPanel12";
			this.flowLayoutPanel12.Size = new System.Drawing.Size(471, 254);
			this.flowLayoutPanel12.TabIndex = 0;
			this.label18.AutoSize = true;
			this.label18.Location = new Point(3, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(205, 15);
			this.label18.TabIndex = 1;
			this.label18.Text = "Please type in the username on FlickR";
			this.txtFlickr.Location = new Point(3, 30);
			this.txtFlickr.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
			this.txtFlickr.Name = "txtFlickr";
			this.txtFlickr.Size = new System.Drawing.Size(145, 23);
			this.txtFlickr.TabIndex = 0;
			this.label19.AutoSize = true;
			this.label19.Location = new Point(3, 56);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(87, 15);
			this.label19.TabIndex = 2;
			this.label19.Text = "ex: odins_raven";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(518, 409);
			base.Controls.Add(this.wizardControl1);
			this.glassExtenderProvider1.SetGlassMargins(this, new System.Windows.Forms.Padding(0));
			base.Name = "AddWizard";
			this.Text = "Add Site Wizard";
			((ISupportInitialize)this.wizardControl1).EndInit();
			this.wizardPage1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.wizardPageTumblr.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.wizardPageTumblrBlog.ResumeLayout(false);
			this.wizardPageTumblrBlog.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.grpTumblrBlog.ResumeLayout(false);
			this.flowLayoutPanel7.ResumeLayout(false);
			this.flowLayoutPanel7.PerformLayout();
			this.wizardPageValidate.ResumeLayout(false);
			this.flowLayoutPanel8.ResumeLayout(false);
			this.flowLayoutPanel8.PerformLayout();
			this.flowLayoutPanel10.ResumeLayout(false);
			this.flowLayoutPanel10.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.flowLayoutPanel9.ResumeLayout(false);
			this.flowLayoutPanel9.PerformLayout();
			this.wizardPageTumblrTags.ResumeLayout(false);
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			this.wizardPage500.ResumeLayout(false);
			this.wizardPage500.PerformLayout();
			this.flowLayoutPanel5.ResumeLayout(false);
			this.flowLayoutPanel5.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.flowLayoutPanel11.ResumeLayout(false);
			this.flowLayoutPanel11.PerformLayout();
			this.wizardPageInstagram.ResumeLayout(false);
			this.wizardPageInstagram.PerformLayout();
			this.flowLayoutPanel6.ResumeLayout(false);
			this.flowLayoutPanel6.PerformLayout();
			this.wizardPageFlickr.ResumeLayout(false);
			this.flowLayoutPanel12.ResumeLayout(false);
			this.flowLayoutPanel12.PerformLayout();
			base.ResumeLayout(false);
		}

		private string RemoveInvalidFilePathCharacters(string filename, string replaceChar)
		{
			string str = string.Concat(new string(Path.GetInvalidFileNameChars()), new string(Path.GetInvalidPathChars()));
			return (new Regex(string.Format("[{0}]", Regex.Escape(str)))).Replace(filename, replaceChar);
		}

		private void wizardPage500_Commit(object sender, WizardPageConfirmEventArgs e)
		{
			string str = string.Concat("http://500px.com/", this.txt500pxUsername.Text);
			str = Ripper.Cleanurl(str);
			string title = Ripper.GetTitle(str);
			this.lblError500px.Text = "";
			if (title == null)
			{
				e.Cancel = true;
				this.lblError500px.Text = "Error trying to retrieve 500px page";
				return;
			}
			this._website = new Website()
			{
				Name = title,
				Url = str
			};
		}

		private void wizardPageFlickr_Commit(object sender, WizardPageConfirmEventArgs e)
		{
			string str = string.Concat("http://www.flickr.com/photos/", this.txtFlickr.Text);
			str = Ripper.Cleanurl(str);
			string title = Ripper.GetTitle(str);
			if (title == null)
			{
				e.Cancel = true;
				return;
			}
			this._website = new Website()
			{
				Name = title,
				Url = str
			};
		}

		private void wizardPageInstagram_Commit(object sender, WizardPageConfirmEventArgs e)
		{
			string str = string.Concat("http://instagram.com/", this.txtInstagram.Text);
			str = Ripper.Cleanurl(str);
			string title = Ripper.GetTitle(str);
			this.lblErrorInstagram.Text = "";
			if (title == null)
			{
				e.Cancel = true;
				this.lblErrorInstagram.Text = "Error trying to retrieve Instagram page";
				return;
			}
			this._website = new Website()
			{
				Name = title,
				Url = str
			};
		}

		private void wizardPageTumblrBlog_Commit(object sender, WizardPageConfirmEventArgs e)
		{
			this.txtTumblrBlogUrl.Text = Ripper.Cleanurl(this.txtTumblrBlogUrl.Text);
			string title = Ripper.GetTitle(this.txtTumblrBlogUrl.Text);
			this.lblErrorTumblrBlog.Text = "";
			if (title == null)
			{
				e.Cancel = true;
				this.lblErrorTumblrBlog.Text = "Error trying to retrieve Tumblr Blog";
				return;
			}
			this._website = new Website()
			{
				Name = title,
				Url = this.txtTumblrBlogUrl.Text,
				DoPhotoSets = this.chkTumblrPhotoSets.Checked,
				DoVideos = this.chkTumblrVideos.Checked
			};
		}

		private void wizardPageTumblrTags_Commit(object sender, WizardPageConfirmEventArgs e)
		{
			if (this.txtTumblrTag.Text.Length == 0)
			{
				e.Cancel = true;
				return;
			}
			string str = this.txtTumblrTag.Text.Trim();
			string str1 = string.Concat("www.tumblr.com/tagged/", str);
			this._website = new Website()
			{
				Name = string.Concat("Tumblr: ", str),
				Url = str1
			};
		}

		private void wizardPageValidate_Commit(object sender, WizardPageConfirmEventArgs e)
		{
			this._website.Name = this.txtName.Text;
			this._website.Folder = this.txtFolder.Text;
			this._website.Viewchecked = true;
			this._website.RenameFiles = this.chkRenameFiles.Checked;
			if (Settings.GetSettings().Sites.Contains(this._website))
			{
				MessageBox.Show("Website is already in TumblRipper", "Website Exists");
				e.Cancel = true;
			}
			if (!Directory.Exists(this.txtFolder.Text))
			{
				if (MessageBox.Show("Directory does not exist. Create it ?", "Directory Not Found", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
				{
					e.Cancel = true;
					return;
				}
				try
				{
					Directory.CreateDirectory(this.txtFolder.Text);
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
					return;
				}
			}
			else if (Directory.GetFileSystemEntries(this.txtFolder.Text).Length != 0 && MessageBox.Show("Directory already exists and isn't empty", "Warning", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}
			Settings.GetSettings().Sites.Add(this._website);
			Settings.GetSettings().SaveSettings();
		}

		private void wizardPageValidate_Initialize(object sender, WizardPageInitEventArgs e)
		{
			this.txtName.Text = this._website.Name;
			string str = this.RemoveInvalidFilePathCharacters(this._website.Name, "");
			this.txtFolder.Text = Path.Combine(Settings.GetSettings().DefaultPath, str);
		}
	}
}