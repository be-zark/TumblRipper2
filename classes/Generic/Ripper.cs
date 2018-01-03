using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using TumblRipper2;
using TumblRipper2.classes;
using TumblRipper2.classes.Flickr;
using TumblRipper2.classes.Instagram;
using TumblRipper2.classes.PX500;
using TumblRipper2.classes.Tumblr;
using TumblRipper2.classes.Youtube;

namespace TumblRipper2.classes.Generic
{
	public abstract class Ripper
	{
		public Website w;

		protected Downloader d;

		public bool Finished;

		public bool Running;

		protected Ripper()
		{
			this.d = Downloader.GetInstance();
		}

		protected Ripper(Website w)
		{
			this.d = Downloader.GetInstance();
			this.w = w;
		}

		protected abstract string _getTitle(string url);

		public static string Cleanurl(string url)
		{
			return url.Replace("http://", "").TrimEnd(new char[] { '/' });
		}

		protected bool FileAlreadySaved(string filename)
		{
			if (!File.Exists(filename))
			{
				return false;
			}
			if ((new FileInfo(filename)).Length == 0)
			{
				return false;
			}
			return true;
		}

		protected string FilenameFromUrl(string url)
		{
			return Path.GetFileName((new Uri(url)).LocalPath).Replace("_250", "").Replace("_300", "").Replace("_400", "").Replace("_500", "").Replace("_1280", "");
		}

		public static Ripper GetRipper(Website w)
		{
			if (w.LastPostID > (long)0)
			{
				w.LastPost = w.LastPostID.ToString(CultureInfo.InvariantCulture);
				w.LastPostID = (long)0;
			}
			if (w.Url.Contains("youtube"))
			{
				return new YouTubeBlog(w);
			}
			if (w.Url.Contains("flickr.com"))
			{
				return new FlickrBlog(w);
			}
			if (w.Url.Contains("instagram.com"))
			{
				return new InstagramBlog(w);
			}
			if (w.Url.Contains("500px.com"))
			{
				return new Px500Blog(w);
			}
			if (w.Url.Contains("/tagged/"))
			{
				return new TumblrTags(w);
			}
			if (w.Url.Contains("/liked/by/"))
			{
				return new TumblrLikes(w);
			}
			return new TumblrBlog(w);
		}

		public static string GetTitle(string url)
		{
			PleaseWait pleaseWait = new PleaseWait("Please wait");
			pleaseWait.Show();
			string str = Ripper.GetRipper(new Website()
			{
				Url = url
			})._getTitle(url);
			pleaseWait.Close();
			return str;
		}

		protected void IndexFinished()
		{
			this.Finished = true;
			if (this.IndexingFinished == null)
			{
				return;
			}
			this.IndexingFinished(this, EventArgs.Empty);
		}

		public void Load()
		{
			this.Running = true;
			this.Finished = false;
			this.w.Viewstatus = "starting ...";
			this.w.LastUpdate = DateTime.Now;
			this.w.ViewnewItems = 0;
			if (this.w.Folder == null)
			{
				this.w.Viewstatus = "ERROR : folder is undefined !!!";
				this.IndexFinished();
				return;
			}
			if (!Directory.Exists(this.w.Folder))
			{
				Directory.CreateDirectory(this.w.Folder);
			}
			this.Start();
		}

		protected abstract void Start();

		public event EventHandler IndexingFinished;

		public enum Type
		{
			Blog,
			Likes,
			Tags
		}
	}
}