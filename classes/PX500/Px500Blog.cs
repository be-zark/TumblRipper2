using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.PX500
{
	internal class Px500Blog : Px500Rip
	{
		protected List<Post> AllPosts = new List<Post>();

		private int _currPage;

		private string _lastDate = "";

		private DateTime _lastDateDT;

		private string _lastIndexedDate = "";

		private DateTime _lastIndexedDateDT;

		private string _token;

		private int _ts = 10;

		private string _username;

		public bool DoCommented
		{
			get;
			set;
		}

		public bool DoFavs
		{
			get;
			set;
		}

		public bool DoLikes
		{
			get;
			set;
		}

		public bool DoUploads
		{
			get;
			set;
		}

		public Px500Blog(Website w) : base(w)
		{
		}

		protected override string _getTitle(string url)
		{
			string[] strArrays = url.Split(new char[] { '/' });
			return strArrays[(int)strArrays.Length - 1];
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
			this.w.LastPost = this._lastDateDT.ToString(CultureInfo.InvariantCulture);
			foreach (Post allPost in this.AllPosts)
			{
				this.AddToDownloadQueue(allPost);
			}
			base.IndexFinished();
		}

		protected void GetPage(int ts)
		{
			this._ts = ts;
			if (this._token == null)
			{
				this.GetToken();
				return;
			}
			if (this._username == null)
			{
				this._username = this.getURL(this.w.Url);
			}
			string str = string.Concat(new object[] { "https://api.500px.com/v1/activities/", this._username, "?rpp=", ts, "&to=", this._lastDate, "&items_max=10&authenticity_token=", this._token });
			int num = this._currPage + 1;
			this._currPage = num;
			this.w.Viewstatus = string.Concat("Parsing page ", num);
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
			if (string.IsNullOrEmpty(obj.Post.HTML))
			{
				this.GetPage(this._ts);
				return;
			}
			this.ParsePage(obj.Post.HTML);
		}

		private void GetPageCallbackToken(PostItem obj)
		{
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(obj.Post.HTML);
			foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//meta"))
			{
				if (!htmlNode.Attributes.Contains("name") || !(htmlNode.Attributes["name"].Value == "csrf-token"))
				{
					continue;
				}
				this._token = htmlNode.Attributes["content"].Value;
				this._token = Uri.EscapeDataString(this._token);
			}
			this.GetPage(10);
		}

		private void GetToken()
		{
			string str = string.Concat("http://", this.w.Url, "/flow");
			this.w.Viewstatus = "Getting token ";
			Post post = new Post()
			{
				Url = str,
				Website = this.w
			};
			Downloader.Callback<PostItem> callback = new Downloader.Callback<PostItem>(this.GetPageCallbackToken);
			Downloader.GetInstance().DownloadPostHTML(post, callback);
		}

		private string getURL(string url)
		{
			string[] strArrays = url.Split(new char[] { '/' });
			return strArrays[(int)strArrays.Length - 1];
		}

		private void ParsePage(string s)
		{
			XmlDocument xmlDocument = JsonConvert.DeserializeXmlNode(s, "ROOT");
			XmlNode xmlNodes = xmlDocument.SelectSingleNode("//to");
			if (xmlNodes != null)
			{
				this._lastDate = xmlNodes.InnerText;
			}
			try
			{
				this._lastDateDT = DateTime.Parse(xmlNodes.InnerText);
			}
			catch (Exception exception)
			{
				this._lastDateDT = DateTime.Now;
			}
			if (xmlNodes == null || this._lastDateDT > this._lastIndexedDateDT)
			{
				this.FinishedIndexing();
				return;
			}
			XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//items");
			if (xmlNodeLists != null)
			{
				foreach (XmlNode xmlNodes1 in xmlNodeLists)
				{
					try
					{
						string innerText = xmlNodes1.SelectSingleNode(".//id").InnerText;
						string str = string.Concat(innerText, " - ", xmlNodes1.SelectSingleNode(".//name").InnerText);
						string innerText1 = xmlNodes1.SelectSingleNode(".//image_url").InnerText;
						innerText1 = innerText1.Replace("/2.", "/5.");
						Post post = new Post()
						{
							Website = this.w,
							Id = innerText,
							Name = str
						};
						post.Items.Add(new PostItemPhoto(post, innerText1, innerText1));
						this.AllPosts.Add(post);
					}
					catch (Exception exception1)
					{
					}
				}
			}
			if (!this.Running)
			{
				this.w.Viewstatus = "Stopped";
				return;
			}
			this.GetPage(this._ts + 10);
		}

		protected override void Start()
		{
			try
			{
				this._lastIndexedDateDT = DateTime.Parse(this.w.LastPost);
			}
			catch (Exception exception)
			{
				this._lastIndexedDateDT = DateTime.Now;
			}
			this.GetPage(10);
		}
	}
}