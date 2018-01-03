using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes
{
	internal class BlogDownloader
	{
		private readonly List<Ripper> _rippers;

		private static BlogDownloader _instance;

		public BlogDownloader.DownloaderStatus Status
		{
			get;
			set;
		}

		public List<Website> Websites
		{
			get;
			set;
		}

		public BlogDownloader()
		{
			this.Websites = new List<Website>();
			this._rippers = new List<Ripper>();
			Downloader.GetInstance().DownloadFinished += new EventHandler(this.BlogDownloader_DownloadFinished);
		}

		private void BlogDownloader_DownloadFinished(object sender, EventArgs e)
		{
			if (this._rippers.Count < this.Websites.Count)
			{
				return;
			}
			bool flag = true;
			for (int i = 0; i < this._rippers.Count; i++)
			{
				Ripper item = this._rippers[i];
				if (!item.Finished)
				{
					flag = false;
				}
				else
				{
					item.w.Viewstatus = "finished";
				}
			}
			if (!flag)
			{
				return;
			}
			this.Websites.Clear();
			this._rippers.Clear();
			this.OnStop();
		}

		private void CheckforDownloadErrors()
		{
			lock (Downloader.GetInstance().ErrorList)
			{
				int count = Downloader.GetInstance().ErrorList.Count;
				if (count > 0)
				{
					MessageBox.Show(string.Concat(count, " dead links found"), "Download Errors Found");
					Downloader.GetInstance().ErrorList.Clear();
				}
			}
		}

		public static BlogDownloader GetInstance()
		{
			BlogDownloader blogDownloader = BlogDownloader._instance;
			if (blogDownloader == null)
			{
				blogDownloader = new BlogDownloader();
				BlogDownloader._instance = blogDownloader;
			}
			return blogDownloader;
		}

		private int getNumberOfRunningBlogs()
		{
			return this._rippers.Count<Ripper>((Ripper r) => !r.Finished);
		}

		private void OnPause()
		{
			this.OnStatusChange(BlogDownloader.DownloaderStatus.Paused);
		}

		private void OnStart()
		{
			this.OnStatusChange(BlogDownloader.DownloaderStatus.Running);
		}

		private void OnStatusChange(BlogDownloader.DownloaderStatus s)
		{
			if (this.DownloadStatusChange != null)
			{
				this.DownloadStatusChange(this, new BlogDownloader.DownloadStatusChangeArgs(s));
			}
		}

		private void OnStop()
		{
			this._rippers.Clear();
			this.Websites.Clear();
			this.OnStatusChange(BlogDownloader.DownloaderStatus.Stopped);
			Settings.GetSettings().SaveSettings();
			this.CheckforDownloadErrors();
		}

		public void Pause()
		{
			BlogDownloader.DownloaderStatus status = this.Status;
			if (status == BlogDownloader.DownloaderStatus.Running)
			{
				this.Status = BlogDownloader.DownloaderStatus.Paused;
			}
			else if (status == BlogDownloader.DownloaderStatus.Paused)
			{
				this.Status = BlogDownloader.DownloaderStatus.Running;
			}
			this.OnPause();
		}

		public void Start()
		{
			(new ParallelOptions()).MaxDegreeOfParallelism = 4;
			this.Status = BlogDownloader.DownloaderStatus.Running;
			this.OnStart();
			for (int i = 0; i < this.Websites.Count; i++)
			{
				Ripper ripper = Ripper.GetRipper(this.Websites[i]);
				this._rippers.Add(ripper);
				ripper.Load();
				while (this.getNumberOfRunningBlogs() > 4)
				{
					Thread.Sleep(1000);
				}
			}
			Downloader.GetInstance().Start();
		}

		public void Stop()
		{
			foreach (Ripper _ripper in this._rippers)
			{
				_ripper.Running = false;
			}
			Downloader.GetInstance().Stop();
			Downloader.GetInstance().Files.Clear();
			this.OnStop();
		}

		public event BlogDownloader.DownloadStatusChangeHandler DownloadStatusChange;

		public enum DownloaderStatus
		{
			Running,
			Stopped,
			Paused
		}

		public class DownloadStatusChangeArgs : EventArgs
		{
			public BlogDownloader.DownloaderStatus Status
			{
				get;
				set;
			}

			public DownloadStatusChangeArgs(BlogDownloader.DownloaderStatus s)
			{
				this.Status = s;
			}
		}

		public delegate void DownloadStatusChangeHandler(object sender, BlogDownloader.DownloadStatusChangeArgs e);
	}
}