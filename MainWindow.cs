using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TumblRipper2.classes;
using TumblRipper2.Properties;

namespace TumblRipper2
{
	public class MainWindow : Form
	{
		public static MainWindow Instance;

		private IContainer components;

		private TableLayoutPanel tableLayoutPanel1;

		private ToolStrip toolStrip1;

		private MenuStrip menuStrip1;

		private ToolStripButton toolStripButton1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripMenuItem helpToolStripMenuItem;

		private ToolStripMenuItem newToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private DataGridView dataGridView1;

		private ToolStripButton btnStart;

		private StatusStrip statusStrip1;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem editToolStripMenuItem1;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ToolStripButton toolStripSplitButton1;

		private ToolStripMenuItem settingsToolStripMenuItem;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripMenuItem toolStripMenuItem3;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton btnStop;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem runCompleteScanToolStripMenuItem;

		private ToolStripMenuItem openFolderToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem aboutToolStripMenuItem;

		private ToolStripNumberControl toolStripNumberControl1;

		private ToolStripLabel toolStripLabel1;

		private ToolStripMenuItem sendDebugInfoToolStripMenuItem;

		private ToolStripMenuItem unlockTumblRipperToolStripMenuItem;

		private ToolStripMenuItem importSettingsToolStripMenuItem;

		private ToolStripMenuItem openSiteToolStripMenuItem;

		public MainWindow()
		{
			this.InitializeComponent();
			this.importSettingsToolStripMenuItem.Visible = false;
			MainWindow.Instance = this;
			this.DatagridCells();
			this.btnStop.Visible = false;
			this.unlockTumblRipperToolStripMenuItem.Visible = false;
			ToolStripNumberControl str = this.toolStripNumberControl1;
			int maxConcurrentDownloads = Downloader.GetInstance().MaxConcurrentDownloads;
			str.Text = maxConcurrentDownloads.ToString(CultureInfo.InvariantCulture);
			if (TumblRipper2.classes.Settings.GetSettings().Licence == "free")
			{
				this.toolStripNumberControl1.Text = "1";
				this.toolStripNumberControl1.Visible = false;
				this.toolStripLabel1.Visible = false;
				this.Text = string.Concat(this.Text, " freeware");
				this.unlockTumblRipperToolStripMenuItem.Visible = true;
			}
			BlogDownloader.GetInstance().DownloadStatusChange += new BlogDownloader.DownloadStatusChangeHandler(this.MainWindow_DownloadStatusChange);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new About()).ShowDialog();
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			BlogDownloader.GetInstance().Stop();
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
		}

		private void DatagridCells()
		{
			this.dataGridView1.Columns.Clear();
			this.dataGridView1.RowTemplate.Height = TumblRipper2.classes.Settings.GetSettings().RowHeight;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			foreach (DisplayField displayField in TumblRipper2.classes.Settings.GetSettings().DisplayFields)
			{
				if (displayField.Column == "Textbox")
				{
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
					{
						Tag = displayField,
						DataPropertyName = displayField.Field,
						HeaderText = displayField.Name
					};
					if (displayField.Size > 0)
					{
						dataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
						dataGridViewTextBoxColumn.Width = displayField.Size;
					}
					this.dataGridView1.Columns.Add(dataGridViewTextBoxColumn);
				}
				else if (displayField.Column != "Checkbox")
				{
					if (displayField.Column != "Image")
					{
						continue;
					}
					DataGridViewImageColumn dataGridViewImageColumn = new DataGridViewImageColumn()
					{
						Tag = displayField,
						DataPropertyName = displayField.Field,
						HeaderText = displayField.Name
					};
					if (displayField.Size > 0)
					{
						dataGridViewImageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
						dataGridViewImageColumn.Width = displayField.Size;
						dataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
					}
					this.dataGridView1.Columns.Add(dataGridViewImageColumn);
				}
				else
				{
					DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn()
					{
						Tag = displayField,
						DataPropertyName = displayField.Field,
						HeaderText = displayField.Name
					};
					if (displayField.Size > 0)
					{
						dataGridViewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
						dataGridViewCheckBoxColumn.Width = displayField.Size;
					}
					this.dataGridView1.Columns.Add(dataGridViewCheckBoxColumn);
				}
			}
			this.dataGridView1.DataSource = TumblRipper2.classes.Settings.GetSettings().Sites;
			this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
			this.dataGridView1.MouseDown += new MouseEventHandler(this.MyDataGridView_MouseDown);
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			(new AddSite((Website)this.dataGridView1.Rows[e.RowIndex].DataBoundItem)).ShowDialog();
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (this.dataGridView1.Columns[e.ColumnIndex].HeaderText == "Enabled")
			{
				DataGridViewRow item = this.dataGridView1.Rows[e.RowIndex];
				if (!(item.DataBoundItem as Website).Viewchecked)
				{
					item.DefaultCellStyle.ForeColor = Color.Gray;
					return;
				}
				item.DefaultCellStyle.ForeColor = Color.Black;
			}
		}

		private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			DisplayField tag = e.Column.Tag as DisplayField;
			if (tag != null)
			{
				tag.Size = e.Column.Width;
			}
			TumblRipper2.classes.Settings.GetSettings().SaveSettings();
		}

		private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.DeleteSelectedBlogs();
			}
		}

		private void deleteBlogs(List<Website> l)
		{
			if (MessageBox.Show(string.Concat("This will delete the following blogs : \n ", l.Aggregate<Website, string>("", (string current, Website w) => string.Concat(current, w.Name, "\n"))), "Delete", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
			{
				return;
			}
			bool flag = false;
			if (MessageBox.Show("Do you wish to delete local files ?", "Delete files ?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
			{
				flag = true;
			}
			foreach (Website website in l)
			{
				if (flag && MessageBox.Show(string.Concat("This will delete all files in directory ", website.Folder, " \n Are you sure ?"), "WARNING", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				{
					Directory.Delete(website.Folder, true);
				}
				TumblRipper2.classes.Settings.GetSettings().Sites.Remove(website);
			}
			TumblRipper2.classes.Settings.GetSettings().SaveSettings();
		}

		private void DeleteSelectedBlogs()
		{
			List<Website> list = (
				from DataGridViewRow r in this.dataGridView1.SelectedRows
				select (Website)r.DataBoundItem).ToList<Website>();
			this.deleteBlogs(list);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Website> list = (
				from DataGridViewRow r in this.dataGridView1.SelectedRows
				select (Website)r.DataBoundItem).ToList<Website>();
			this.deleteBlogs(list);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void editToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			foreach (AddSite addSite in 
				from DataGridViewRow r in this.dataGridView1.SelectedRows
				select (Website)r.DataBoundItem into w
				select new AddSite(w))
			{
				addSite.ShowDialog();
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TumblRipper2.classes.Settings.GetSettings().SaveSettings();
			base.Close();
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			bool flag = false;
			string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
			for (int i = 0; i < (int)data.Length; i++)
			{
				string str = data[i];
				if (Path.GetExtension(str) != ".tmblr")
				{
					flag = true;
				}
				else
				{
					Website website = ImporterV1.ImportFromTumblr(str);
					TumblRipper2.classes.Settings.GetSettings().AddSite(website);
				}
			}
			TumblRipper2.classes.Settings.GetSettings().SaveSettings();
			this.DatagridCells();
			if (flag)
			{
				MessageBox.Show("Only .tmblr files supported");
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
			{
				e.Effect = DragDropEffects.All;
			}
		}

		public static ToolStrip GetToolStrip()
		{
			return MainWindow.Instance.statusStrip1;
		}

		private void importExportToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void importSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new ImportSettings()).ShowDialog();
			this.DatagridCells();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainWindow));
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.toolStrip1 = new ToolStrip();
			this.toolStripButton1 = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripSplitButton1 = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.btnStart = new ToolStripButton();
			this.btnStop = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.toolStripLabel1 = new ToolStripLabel();
			this.toolStripNumberControl1 = new ToolStripNumberControl();
			this.dataGridView1 = new DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.toolStripMenuItem3 = new ToolStripMenuItem();
			this.runCompleteScanToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.openFolderToolStripMenuItem = new ToolStripMenuItem();
			this.openSiteToolStripMenuItem = new ToolStripMenuItem();
			this.statusStrip1 = new StatusStrip();
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.newToolStripMenuItem = new ToolStripMenuItem();
			this.settingsToolStripMenuItem = new ToolStripMenuItem();
			this.exitToolStripMenuItem = new ToolStripMenuItem();
			this.editToolStripMenuItem = new ToolStripMenuItem();
			this.editToolStripMenuItem1 = new ToolStripMenuItem();
			this.deleteToolStripMenuItem = new ToolStripMenuItem();
			this.helpToolStripMenuItem = new ToolStripMenuItem();
			this.sendDebugInfoToolStripMenuItem = new ToolStripMenuItem();
			this.aboutToolStripMenuItem = new ToolStripMenuItem();
			this.importSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.unlockTumblRipperToolStripMenuItem = new ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 340);
			this.tableLayoutPanel1.TabIndex = 0;
			this.toolStrip1.Dock = DockStyle.Fill;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton1, this.toolStripSeparator1, this.toolStripSplitButton1, this.toolStripSeparator3, this.btnStart, this.btnStop, this.toolStripSeparator2, this.toolStripLabel1, this.toolStripNumberControl1 });
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(773, 40);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
			this.toolStripButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(93, 37);
			this.toolStripButton1.Text = "New Tumblr";
			this.toolStripButton1.Click += new EventHandler(this.newToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
			this.toolStripSplitButton1.Image = (Image)resources.GetObject("toolStripSplitButton1.Image");
			this.toolStripSplitButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(92, 37);
			this.toolStripSplitButton1.Text = "Open Folder";
			this.toolStripSplitButton1.Click += new EventHandler(this.toolStripSplitButton1_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
			this.btnStart.Image = (Image)resources.GetObject("btnStart.Image");
			this.btnStart.ImageScaling = ToolStripItemImageScaling.None;
			this.btnStart.ImageTransparentColor = Color.Magenta;
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(48, 37);
			this.btnStart.Text = "Run";
			this.btnStart.Click += new EventHandler(this.toolStripButton2_Click);
			this.btnStop.Image = (Image)resources.GetObject("btnStop.Image");
			this.btnStop.ImageTransparentColor = Color.Magenta;
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(51, 37);
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new EventHandler(this.btnStop_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(106, 37);
			this.toolStripLabel1.Text = "Download Threads";
			this.toolStripNumberControl1.Name = "toolStripNumberControl1";
			this.toolStripNumberControl1.Size = new System.Drawing.Size(41, 37);
			this.toolStripNumberControl1.Text = "0";
			this.toolStripNumberControl1.ValueChanged += new EventHandler(this.toolStripNumberControl1_ValueChanged);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new Point(3, 43);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(767, 274);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
			this.dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
			this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
			this.dataGridView1.KeyDown += new KeyEventHandler(this.dataGridView1_KeyDown);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripSeparator4, this.toolStripMenuItem3, this.runCompleteScanToolStripMenuItem, this.toolStripSeparator5, this.openFolderToolStripMenuItem, this.openSiteToolStripMenuItem });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(178, 148);
			this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
			this.toolStripMenuItem1.Text = "Edit";
			this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 22);
			this.toolStripMenuItem2.Text = "Delete";
			this.toolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(174, 6);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 22);
			this.toolStripMenuItem3.Text = "Run Update";
			this.toolStripMenuItem3.Click += new EventHandler(this.toolStripMenuItem3_Click);
			this.runCompleteScanToolStripMenuItem.Name = "runCompleteScanToolStripMenuItem";
			this.runCompleteScanToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.runCompleteScanToolStripMenuItem.Text = "Run Complete scan";
			this.runCompleteScanToolStripMenuItem.Click += new EventHandler(this.runCompleteScanToolStripMenuItem_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(174, 6);
			this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
			this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.openFolderToolStripMenuItem.Text = "Open Folder";
			this.openFolderToolStripMenuItem.Click += new EventHandler(this.openFolderToolStripMenuItem_Click);
			this.openSiteToolStripMenuItem.Name = "openSiteToolStripMenuItem";
			this.openSiteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.openSiteToolStripMenuItem.Text = "Open Site";
			this.openSiteToolStripMenuItem.Click += new EventHandler(this.openSiteToolStripMenuItem_Click);
			this.statusStrip1.Location = new Point(0, 320);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(773, 20);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.fileToolStripMenuItem, this.editToolStripMenuItem, this.helpToolStripMenuItem, this.unlockTumblRipperToolStripMenuItem });
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(773, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.newToolStripMenuItem, this.settingsToolStripMenuItem, this.exitToolStripMenuItem });
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new EventHandler(this.newToolStripMenuItem_Click);
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.settingsToolStripMenuItem.Click += new EventHandler(this.settingsToolStripMenuItem_Click);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
			this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.editToolStripMenuItem1, this.deleteToolStripMenuItem });
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
			this.editToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
			this.editToolStripMenuItem1.Text = "Edit";
			this.editToolStripMenuItem1.Click += new EventHandler(this.editToolStripMenuItem1_Click);
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new EventHandler(this.deleteToolStripMenuItem_Click);
			this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.sendDebugInfoToolStripMenuItem, this.aboutToolStripMenuItem, this.importSettingsToolStripMenuItem });
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			this.sendDebugInfoToolStripMenuItem.Name = "sendDebugInfoToolStripMenuItem";
			this.sendDebugInfoToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.sendDebugInfoToolStripMenuItem.Text = "Send Debug Info";
			this.sendDebugInfoToolStripMenuItem.Click += new EventHandler(this.sendDebugInfoToolStripMenuItem_Click);
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
			this.importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
			this.importSettingsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.importSettingsToolStripMenuItem.Text = "ImportSettings";
			this.importSettingsToolStripMenuItem.Click += new EventHandler(this.importSettingsToolStripMenuItem_Click);
			this.unlockTumblRipperToolStripMenuItem.Name = "unlockTumblRipperToolStripMenuItem";
			this.unlockTumblRipperToolStripMenuItem.Size = new System.Drawing.Size(128, 20);
			this.unlockTumblRipperToolStripMenuItem.Text = "Unlock TumblRipper";
			this.unlockTumblRipperToolStripMenuItem.Click += new EventHandler(this.unlockTumblRipperToolStripMenuItem_Click);
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(773, 364);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "MainWindow";
			this.Text = "TumblRipper";
			base.FormClosing += new FormClosingEventHandler(this.MainWindow_FormClosing);
			base.Load += new EventHandler(this.MainWindow_Load);
			base.DragDrop += new DragEventHandler(this.Form1_DragDrop);
			base.DragEnter += new DragEventHandler(this.Form1_DragEnter);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void MainWindow_DownloadStatusChange(object sender, BlogDownloader.DownloadStatusChangeArgs e)
		{
			MainWindow.Instance.Invoke(new MethodInvoker(() => {
				switch (e.Status)
				{
					case BlogDownloader.DownloaderStatus.Running:
					{
						this.btnStop.Visible = true;
						this.btnStart.Visible = false;
						return;
					}
					case BlogDownloader.DownloaderStatus.Stopped:
					{
						this.btnStop.Visible = false;
						this.btnStart.Visible = true;
						return;
					}
					case BlogDownloader.DownloaderStatus.Paused:
					{
						return;
					}
					default:
					{
						return;
					}
				}
			}));
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			BlogDownloader.GetInstance().Stop();
			TumblRipper2.classes.Settings.GetSettings().SaveSettings();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			HelpMessages.HelpMessage("WelcomeMessage");
		}

		private void MyDataGridView_MouseDown(object sender, MouseEventArgs e)
		{
			DataGridView.HitTestInfo hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (hitTestInfo.RowIndex > -1 && !this.dataGridView1.Rows[hitTestInfo.RowIndex].Selected)
				{
					this.dataGridView1.ClearSelection();
					this.dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true;
					return;
				}
			}
			else if (hitTestInfo.ColumnIndex > -1 && this.dataGridView1.Columns[hitTestInfo.ColumnIndex].HeaderText == "Enabled" && hitTestInfo.RowIndex > -1)
			{
				Website dataBoundItem = this.dataGridView1.Rows[hitTestInfo.RowIndex].DataBoundItem as Website;
				dataBoundItem.Viewchecked = !dataBoundItem.Viewchecked;
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (TumblRipper2.classes.Settings.GetSettings().Licence == "free" && TumblRipper2.classes.Settings.GetSettings().Sites.Count > 4)
			{
				MessageBox.Show("Free version limitation, only 5 blogs supported");
				return;
			}
			(new AddWizard()).ShowDialog();
		}

		private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
			{
				Process.Start(((Website)this.dataGridView1.SelectedRows[i].DataBoundItem).Folder);
			}
		}

		private void openSiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
				{
					Website dataBoundItem = (Website)this.dataGridView1.SelectedRows[i].DataBoundItem;
					Process.Start(string.Concat("http://", dataBoundItem.Url));
				}
			}
			catch (Win32Exception win32Exception1)
			{
				Win32Exception win32Exception = win32Exception1;
				if (win32Exception.ErrorCode == -2147467259)
				{
					MessageBox.Show(win32Exception.Message);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void runCompleteScanToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Website> websites = new List<Website>();
			for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
			{
				Website dataBoundItem = (Website)this.dataGridView1.SelectedRows[i].DataBoundItem;
				dataBoundItem.ResetWebsite();
				websites.Add(dataBoundItem);
			}
			this.runLoadThread(websites);
		}

		private void runLoadThread(IEnumerable<Website> wl)
		{
			BlogDownloader instance = BlogDownloader.GetInstance();
			instance.Websites.Clear();
			foreach (Website website in wl)
			{
				if (!website.Viewchecked)
				{
					continue;
				}
				instance.Websites.Add(website);
			}
			instance.Start();
		}

		private void sendDebugInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(Resources.DebugThisWillSend, Resources.DebugThisWillSendTITLE, MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
			{
				return;
			}
			MessageBox.Show(Resources.DebugSuccess);
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new SettingsView()).ShowDialog();
			this.DatagridCells();
		}

		private void Sites_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.Invoke(new MethodInvoker(() => this.dataGridView1.Refresh()));
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			List<Website> list = TumblRipper2.classes.Settings.GetSettings().Sites.ToList<Website>();
			(new Thread(() => this.runLoadThread(list))).Start();
			return;
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			foreach (AddSite addSite in 
				from DataGridViewRow r in this.dataGridView1.SelectedRows
				select (Website)r.DataBoundItem into w
				select new AddSite(w))
			{
				addSite.ShowDialog();
			}
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.DeleteSelectedBlogs();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			List<Website> list = (
				from DataGridViewRow r in this.dataGridView1.SelectedRows
				select (Website)r.DataBoundItem).ToList<Website>();
			(new Thread(() => this.runLoadThread(list))).Start();
		}

		private void toolStripNumberControl1_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = sender as NumericUpDown;
			if (numericUpDown != null)
			{
				Downloader.GetInstance().MaxConcurrentDownloads = (int)numericUpDown.Value;
			}
		}

		private void toolStripSplitButton1_Click(object sender, EventArgs e)
		{
			foreach (Website website in 
				from DataGridViewRow r in this.dataGridView1.SelectedRows
				select (Website)r.DataBoundItem)
			{
				Process.Start(website.Folder);
			}
		}

		private void unlockTumblRipperToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new Licence()).ShowDialog();
		}
	}
}