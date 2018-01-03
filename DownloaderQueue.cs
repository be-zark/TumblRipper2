using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TumblRipper2.classes;

namespace TumblRipper2
{
	public class DownloaderQueue : Form
	{
		private BindingList<DownloadState> bd = new BindingList<DownloadState>();

		private IContainer components;

		private DataGridView dataGridView1;

		public DownloaderQueue()
		{
			this.InitializeComponent();
			DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
			{
				DataPropertyName = "Url",
				HeaderText = "URL"
			};
			Timer timer = new Timer();
			timer.Tick += new EventHandler(this.t_Tick);
			timer.Interval = 1000;
			timer.Enabled = true;
			timer.Start();
			this.dataGridView1.Columns.Add(dataGridViewTextBoxColumn);
			this.dataGridView1.DataSource = this.bd;
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
			this.dataGridView1 = new DataGridView();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = DockStyle.Fill;
			this.dataGridView1.Location = new Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(339, 371);
			this.dataGridView1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(339, 371);
			base.Controls.Add(this.dataGridView1);
			base.Name = "DownloaderQueue";
			this.Text = "DownloaderQueue";
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}

		private void t_Tick(object sender, EventArgs e)
		{
			this.bd.Clear();
			lock (Downloader.GetInstance().Files.flatlist)
			{
				foreach (DownloadState file in Downloader.GetInstance().Files.flatlist)
				{
					this.bd.Add(file);
				}
			}
		}
	}
}