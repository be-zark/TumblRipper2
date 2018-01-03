using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Flickr
{
	internal class FlickrBlog : FlickrRip
	{
		private int _ts;

		private long _lastID;

		private long _lastPostID;

		public FlickrBlog(Website w) : base(w)
		{
		}

		protected override string _getTitle(string url)
		{
			string str;
			string str1 = url.Replace("www.", "").Replace("flickr.com", "");
			str1 = str1.Split(new char[] { '/' })[2];
			try
			{
				string str2 = (new MyWebClient()).DownloadString(string.Concat("http://", url));
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(str2);
				foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//span"))
				{
					if (!htmlNode.Attributes.Contains("class") || !(htmlNode.Attributes["class"].Value == "character-name-holder"))
					{
						continue;
					}
					str = WebUtility.HtmlDecode(htmlNode.InnerText.Replace("&nbsp;", "").Trim());
					return str;
				}
				str = str1;
			}
			catch (Exception exception)
			{
				str = str1;
			}
			return str;
		}

		protected void AddToDownloadQueue(Post post)
		{
			int num = 0;
			lock (this.w)
			{
				Website postCount = this.w;
				postCount.PostCount = postCount.PostCount + 1;
			}
			foreach (PostItem item in post.Items)
			{
				num++;
				string name = base.FilenameFromUrl(item.URL);
				if (item is PostItemPhoto)
				{
					name = base.FilenameFromUrl((item as PostItemPhoto).Thumb);
				}
				if (this.w.RenameFiles)
				{
					Uri uri = new Uri(item.URL);
					string extension = Path.GetExtension(name);
					name = post.Name;
					name = (post.Items.Count <= 1 ? string.Concat(name, extension) : string.Concat(new object[] { name, "_", num, extension }));
				}
				name = string.Join("", name.Split(Path.GetInvalidFileNameChars()));
				name = Path.Combine(this.w.Folder, name);
				if (!this.w.SkipExisting || !base.FileAlreadySaved(name))
				{
					lock (this.w)
					{
						Website viewnewItems = this.w;
						viewnewItems.ViewnewItems = viewnewItems.ViewnewItems + 1;
						this.w.Viewstatus = string.Concat("Queued ", Path.GetFileName(name));
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

		private void FinishedIndexing()
		{
			this.w.LastPost = this._lastID.ToString(CultureInfo.InvariantCulture);
			foreach (Post allPost in this.AllPosts)
			{
				this.AddToDownloadQueue(allPost);
			}
			base.IndexFinished();
		}

		protected void GetPage(int ts)
		{
			this._ts = ts;
			string str = string.Concat(new object[] { "http://", this.w.Url, "/?data=1&page=", ts });
			this.w.Viewstatus = string.Concat("Parsing page ", ts);
			Post post = new Post()
			{
				Url = str,
				Website = this.w
			};
			Downloader.Callback<PostItem> callback = new Downloader.Callback<PostItem>(this.GetPageCallback);
			Downloader.GetInstance().DownloadPostHTML(post, callback);
		}

		private void GetPageCallback(PostItem obj)
		{
			this.ParsePage(obj.Post.HTML);
		}

		private void ParsePage(string s)
		{
			s = s.Replace("<!DOCTYPE html>\\n", "");
			XmlNodeList xmlNodeLists = JsonConvert.DeserializeXmlNode(s, "ROOT").SelectNodes("//photos");
			if (xmlNodeLists != null)
			{
				if (xmlNodeLists.Count == 0)
				{
					this.FinishedIndexing();
					return;
				}
				foreach (XmlNode xmlNodes in xmlNodeLists)
				{
					try
					{
						string innerText = xmlNodes.SelectSingleNode(".//id").InnerText;
						string str = string.Concat(innerText, " - ", xmlNodes.SelectSingleNode(".//name").InnerText);
						XmlNode xmlNodes1 = xmlNodes.SelectSingleNode(".//sizes");
						string innerText1 = xmlNodes1.SelectSingleNode(".//o").SelectSingleNode("url").InnerText;
						string str1 = xmlNodes1.SelectSingleNode(".//t").SelectSingleNode("url").InnerText;
						string str2 = string.Concat("http://www.flickr.com", xmlNodes.SelectSingleNode(".//photo_url").InnerText);
						Post post = new Post()
						{
							Website = this.w,
							Id = innerText,
							Name = str,
							Url = str2
						};
						if (long.Parse(innerText) >= this._lastPostID)
						{
							this._lastID = Math.Max(long.Parse(innerText), this._lastID);
							post.Items.Add(new PostItemPhoto(post, innerText1, str1));
							this.AllPosts.Add(post);
						}
						else
						{
							this.FinishedIndexing();
							return;
						}
					}
					catch (Exception exception)
					{
					}
				}
			}
			if (this.Running)
			{
				this.GetPage(this._ts + 1);
				return;
			}
			this.w.Viewstatus = "Stopped";
		}

		protected override void Start()
		{
			this.GetPage(1);
			try
			{
				this._lastPostID = long.Parse(this.w.LastPost);
			}
			catch (Exception exception)
			{
				this._lastPostID = (long)0;
			}
		}
	}
}