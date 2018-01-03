using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Tumblr
{
	public abstract class TumblrRip : Ripper
	{
		protected List<Post> AllPosts = new List<Post>();

		protected List<Url> FilesToDownload = new List<Url>();

		private int _photoSetScan;

		protected TumblrRip(Website w) : base(w)
		{
		}

		protected void AddToDownloadQueue(Post post)
		{
			if (!post.ScanDone)
			{
				lock (this.w)
				{
					Website postCount = this.w;
					postCount.PostCount = postCount.PostCount + 1;
				}
			}
			if (post.Items.Count == 0 && !post.ScanDone && this.Running)
			{
				post.ScanDone = true;
				this.processPost(post);
				return;
			}
			post.HTML = null;
			int num = 0;
			foreach (PostItem item in post.Items)
			{
				num++;
				string name = base.FilenameFromUrl(item.URL);
				if (item is PostItemPhoto)
				{
					name = base.FilenameFromUrl((item as PostItemPhoto).Thumb);
				}
				if (item is PostItemVideo)
				{
					name = string.Concat(Path.GetFileName((new Uri(item.URL)).LocalPath), ".mp4");
				}
				if (this.w.RenameFiles)
				{
					Uri uri = new Uri(item.URL);
					string extension = Path.GetExtension(name);
					name = post.Name;
					name = (post.Items.Count <= 1 ? string.Concat(name, extension) : string.Concat(new object[] { name, "_", num, extension }));
				}
				name = Path.Combine(this.w.Folder, name);
				if (!this.w.SkipExisting || !base.FileAlreadySaved(name))
				{
					lock (this.w)
					{
						Website viewnewItems = this.w;
						viewnewItems.ViewnewItems = viewnewItems.ViewnewItems + 1;
						this.w.LastUpdate = DateTime.Now;
					}
					this.d.AddFile(item, item.URL, name, null);
				}
				else
				{
					return;
				}
			}
		}

		private void processPost(Post post)
		{
			Downloader.Callback<PostItem> callback = new Downloader.Callback<PostItem>(this.processPostCallback);
			Downloader.GetInstance().DownloadPostHTML(post, callback);
		}

		protected void processPostCallback(PostItem postItem)
		{
			Post post = postItem.Post;
			HtmlDocument htmlDocument = new HtmlDocument();
			if (post.HTML == null)
			{
				return;
			}
			htmlDocument.LoadHtml(post.HTML);
			lock (this.w)
			{
				Website website = this.w;
				object[] postCount = new object[] { "PhotoSet Scan: Page ", null, null, null };
				int num = this._photoSetScan + 1;
				this._photoSetScan = num;
				postCount[1] = num;
				postCount[2] = "/";
				postCount[3] = this.w.PostCount;
				website.Viewstatus = string.Concat(postCount);
			}
			string hTML = post.HTML;
			string str = "image";
			foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//meta"))
			{
				if (!htmlNode.Attributes.Contains("property") || !htmlNode.Attributes["property"].Value.Contains("og:type"))
				{
					continue;
				}
				str = htmlNode.Attributes["content"].Value.Replace("tumblr-feed:", "");
			}
			string[] strArrays = post.Url.Replace("http://", "").Split(new char[] { '/' });
			string str1 = string.Concat("http://", strArrays[0], "/video_file/", strArrays[2]);
			int num1 = hTML.IndexOf(str1);
			if (num1 > -1)
			{
				int num2 = hTML.IndexOf("\\x22", num1);
				string str2 = hTML.Substring(num1, num2 - num1);
				str2 = string.Concat(str2.Replace(str1, "http://vt.tumblr.com"), ".mp4");
				PostItemVideo postItemVideo = new PostItemVideo(post, str2);
				post.Items.Add(postItemVideo);
			}
			HtmlNodeCollection htmlNodeCollections = htmlDocument.DocumentNode.SelectNodes("//iframe");
			bool flag = false;
			foreach (HtmlNode htmlNode1 in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//meta"))
			{
				if (!htmlNode1.Attributes.Contains("property") || !htmlNode1.Attributes["property"].Value.Equals("og:image"))
				{
					continue;
				}
				string value = htmlNode1.Attributes["content"].Value;
				if (str == "video")
				{
					if (value.IndexOf("tumblr_") <= 0)
					{
						continue;
					}
					string str3 = value.Substring(value.IndexOf("tumblr_")).Replace("_frame1.jpg", "");
					value = string.Concat("http://vt.tumblr.com/", str3, ".mp4");
					PostItemVideo postItemVideo1 = new PostItemVideo(post, value);
					post.Items.Add(postItemVideo1);
					flag = true;
				}
				else
				{
					PostItemPhoto postItemPhoto = new PostItemPhoto(post, value, value)
					{
						Post = post
					};
					post.Items.Add(postItemPhoto);
					flag = true;
				}
			}
			htmlNodeCollections = htmlDocument.DocumentNode.SelectNodes("//img");
			if (htmlNodeCollections != null && !flag)
			{
				foreach (HtmlNode htmlNode2 in (IEnumerable<HtmlNode>)htmlNodeCollections)
				{
					if (!htmlNode2.Attributes.Contains("src"))
					{
						continue;
					}
					string value1 = htmlNode2.Attributes["src"].Value;
					if (!value1.Contains("_500.") && !value1.Contains("_1280.") && !value1.Contains("_400.") && !value1.Contains("_300.") && !value1.Contains("_250.") && !value1.Contains("_150."))
					{
						continue;
					}
					string str4 = value1.Replace("_500", "_1280");
					PostItemPhoto postItemPhoto1 = new PostItemPhoto(post, str4, value1)
					{
						Post = post
					};
					post.Items.Add(postItemPhoto1);
				}
			}
			Downloader.GetInstance().Start();
			if (post.Items.Count > 0)
			{
				this.AddToDownloadQueue(post);
			}
			htmlDocument = null;
		}
	}
}