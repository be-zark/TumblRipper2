using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Timers;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes
{
	public class Downloader
	{
		public DownloaderPriorityQueue Files = new DownloaderPriorityQueue();

		public System.Timers.Timer Timer;

		private static Downloader _instance;

		private int maxConcurrentDownloads = 1;

		private int status = 2;

		private List<MyWebClient> WC = new List<MyWebClient>();

		public List<DownloadState> ErrorList
		{
			get;
			set;
		}

		public int MaxConcurrentDownloads
		{
			get
			{
				return this.maxConcurrentDownloads;
			}
			set
			{
				this.maxConcurrentDownloads = Math.Max(1, Math.Min(value, 20));
				if (Settings.GetSettings().Licence == "free")
				{
					this.maxConcurrentDownloads = 1;
				}
			}
		}

		public int RunningThreads
		{
			get
			{
				return this.WC.Count;
			}
		}

		public double Speed
		{
			get;
			set;
		}

		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		public Downloader()
		{
			this.MaxConcurrentDownloads = Settings.GetSettings().MaxThreads;
			this.ErrorList = new List<DownloadState>();
		}

		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("timer");
			this.StartDownload();
		}

		public void AddFile(PostItem p, string url, string filename, Downloader.Callback<PostItem> callback)
		{
			lock (this.Files)
			{
				DownloadState downloadState = new DownloadState(Downloader.DownloadType.File, p, url, filename, null, callback);
				this.Files.Enqueue(downloadState, 5);
			}
			this.StartDownload();
		}

		private void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			DownloadState userState = (DownloadState)e.UserState;
			if (e.Error == null)
			{
				try
				{
					userState.postItem.Post.Website.Cookies = (sender as MyWebClient).CookieContainer;
					if (userState.type == Downloader.DownloadType.String)
					{
						byte[] result = e.Result;
						string str = (new UTF8Encoding()).GetString(result);
						if (str.Length >= 3 || userState.postItem.DownloadErrorCount != 0)
						{
							userState.postItem.Post.HTML = str;
						}
						else
						{
							PostItem downloadErrorCount = userState.postItem;
							downloadErrorCount.DownloadErrorCount = downloadErrorCount.DownloadErrorCount + 1;
							this.AddFile(userState.postItem, userState.postItem.URL, userState.Filename, userState.callback);
							return;
						}
					}
					else if (userState.type == Downloader.DownloadType.File)
					{
						using (FileStream fileStream = new FileStream(userState.Filename, FileMode.Create, FileAccess.Write))
						{
							fileStream.Write(e.Result, 0, (int)e.Result.Length);
						}
						lock (userState.postItem.Post.Website)
						{
							Website website = userState.postItem.Post.Website;
							website.ViewnewItems = website.ViewnewItems - 1;
							userState.postItem.Post.Website.Viewstatus = string.Concat("Downloaded ", Path.GetFileName(userState.Filename));
							userState.postItem.Post.Website.LastLoadedImage = userState.Filename;
							if (userState.postItem is PostItemPhoto)
							{
								Website photoCount = userState.postItem.Post.Website;
								photoCount.PhotoCount = photoCount.PhotoCount + 1;
							}
							else if (userState.postItem is PostItemVideo)
							{
								Website videoCount = userState.postItem.Post.Website;
								videoCount.VideoCount = videoCount.VideoCount + 1;
							}
						}
						if (userState.Client.ResponseHeaders != null)
						{
							try
							{
								string str1 = userState.Client.ResponseHeaders.Get("Last-Modified");
								if (str1 != null)
								{
									File.SetCreationTime(userState.Filename, DateTime.Parse(str1));
								}
							}
							catch (Exception exception)
							{
								Console.WriteLine(exception.Message);
							}
						}
					}
				}
				catch (Exception exception1)
				{
					Console.WriteLine(exception1.Message);
				}
			}
			else
			{
				WebException error = e.Error as WebException;
				if (error != null && error.Response != null)
				{
					if (userState.postItem.DownloadErrorCount != 0)
					{
						this.ErrorList.Add(userState);
						lock (userState.postItem.Post.Website)
						{
							Website viewnewItems = userState.postItem.Post.Website;
							viewnewItems.ViewnewItems = viewnewItems.ViewnewItems - 1;
						}
					}
					else if (userState.postItem is PostItemPhoto)
					{
						string thumb = (userState.postItem as PostItemPhoto).Thumb;
						PostItem postItem = userState.postItem;
						postItem.DownloadErrorCount = postItem.DownloadErrorCount + 1;
						this.AddFile(userState.postItem, thumb, userState.Filename, userState.callback);
					}
				}
			}
			if (userState.callback != null)
			{
				userState.callback(userState.postItem);
			}
			this.StartDownload();
		}

		public void DownloadPostHTML(Post post, Downloader.Callback<PostItem> callback)
		{
			int num = 3;
			if (post.Name == null)
			{
				num = 1;
			}
			PostItemHtml postItemHtml = new PostItemHtml(post, post.Url);
			DownloadState downloadState = new DownloadState(Downloader.DownloadType.String, postItemHtml, post.Url, null, null, callback);
			lock (this.Files)
			{
				this.Files.Enqueue(downloadState, num);
			}
			this.StartDownload();
		}

		public void DownloadPostItem(PostItem p, string url, string filename, Downloader.Callback<PostItem> callback)
		{
			DownloadState downloadState = new DownloadState(Downloader.DownloadType.File, p, url, filename, null, callback);
			lock (this.Files)
			{
				this.Files.Enqueue(downloadState, 5);
			}
			this.StartDownload();
		}

		public void DumpQueue()
		{
			Console.WriteLine("******************");
			foreach (DownloadState file in this.Files.flatlist)
			{
				Console.WriteLine(file.Url);
			}
			Console.WriteLine("******************");
		}

		public static Downloader GetInstance()
		{
			if (Downloader._instance == null)
			{
				Downloader._instance = new Downloader();
			}
			return Downloader._instance;
		}

		protected virtual void OnDownloadFinished(EventArgs e)
		{
			if (this.DownloadFinished != null)
			{
				Console.WriteLine("queue finished");
				this.DownloadFinished(this, e);
			}
		}

		protected virtual void OnDownloadStarted(EventArgs e)
		{
			if (this.DownloadStarted != null)
			{
				this.DownloadStarted(this, e);
			}
		}

		internal void Pause()
		{
		}

		public static void ResetInstance()
		{
			Downloader._instance = null;
		}

		public void Start()
		{
			this.status = 2;
			if (this.Timer != null)
			{
				this.Timer = new System.Timers.Timer(5000);
				this.Timer.Elapsed += new ElapsedEventHandler(this._timer_Elapsed);
				this.Timer.Enabled = true;
				this.Timer.Start();
			}
			this.StartDownload();
		}

		private void StartDownload()
		{
			if (this.status == 0)
			{
				return;
			}
			if (this.WC.Count > this.MaxConcurrentDownloads)
			{
				for (int i = 0; i < (int)this.WC.ToArray().Length; i++)
				{
					MyWebClient array = this.WC.ToArray()[i];
					if (!array.IsBusy)
					{
						array.Dispose();
						this.WC.Remove(array);
						Console.WriteLine("Deleted webclient");
					}
				}
			}
			if (this.WC.Count < this.MaxConcurrentDownloads)
			{
				MyWebClient myWebClient = new MyWebClient();
				myWebClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(this.client_DownloadDataCompleted);
				this.WC.Add(myWebClient);
				Console.WriteLine("New webclient");
			}
			MyWebClient[] myWebClientArray = this.WC.ToArray();
			for (int j = 0; j < (int)myWebClientArray.Length; j++)
			{
				MyWebClient cookies = myWebClientArray[j];
				lock (cookies)
				{
					if (!cookies.IsBusy)
					{
						lock (this.Files)
						{
							if (this.Files.Count <= 0)
							{
								bool flag = true;
								for (int k = 0; k < (int)this.WC.ToArray().Length; k++)
								{
									if (this.WC.ToArray()[k].IsBusy)
									{
										flag = false;
									}
								}
								if (flag)
								{
									this.OnDownloadFinished(EventArgs.Empty);
								}
							}
							else
							{
								DownloadState cookieContainer = this.Files.Dequeue();
								cookieContainer.Client = cookies;
								if (cookieContainer.postItem.Post.Website.Cookies == null)
								{
									cookieContainer.postItem.Post.Website.Cookies = new CookieContainer();
								}
								cookies.CookieContainer = cookieContainer.postItem.Post.Website.Cookies;
								try
								{
									cookies.DownloadDataAsync(cookieContainer.Uri, cookieContainer);
								}
								catch (Exception exception)
								{
									Thread.Sleep(1000);
									this.Files.Enqueue(cookieContainer, 1);
									Console.WriteLine(string.Concat("Requeued ", cookieContainer.Uri));
								}
							}
						}
					}
				}
			}
		}

		public void Stop()
		{
			this.status = 0;
			this.Timer = null;
			MyWebClient[] array = this.WC.ToArray();
			for (int i = 0; i < (int)array.Length; i++)
			{
				MyWebClient myWebClient = array[i];
				if (myWebClient != null)
				{
					myWebClient.CancelAsync();
					myWebClient.Dispose();
				}
			}
		}

		public event EventHandler DownloadFinished;

		public event EventHandler DownloadStarted;

		public event EventHandler FileDownloaded;

		public delegate void Callback<T>(T obj);

		public enum DownloadType
		{
			String,
			File
		}
	}
}