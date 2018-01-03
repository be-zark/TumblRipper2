using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Tumblr
{
	internal class TumblrBlog : TumblrRip
	{
		private int _currPage;

		private bool _foundDuplicate;

		public long LastPost
		{
			get;
			set;
		}

		public long PreviousLastPostID
		{
			get;
			set;
		}

		public TumblrBlog(Website w) : base(w)
		{
		}

		protected override string _getTitle(string url)
		{
			string str;
			string str1 = url.Replace("www.", "");
			str1 = str1.Split(new char[] { '.' })[0];
			try
			{
				string str2 = (new MyWebClient()).DownloadString(string.Concat("http://", url, "/archive"));
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(str2);
				foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//h1"))
				{
					if (!htmlNode.Attributes.Contains("class") || !(htmlNode.Attributes["class"].Value == "blog_title"))
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
				str = null;
			}
			return str;
		}

		private int getNextTS(string html)
		{
			int num;
			try
			{
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(html);
				num = int.Parse(htmlDocument.DocumentNode.SelectSingleNode("//a[@id='next_page_link']").Attributes["href"].Value.Replace("/archive?before_time=", ""));
			}
			catch (Exception exception)
			{
				num = 0;
			}
			return num;
		}

		protected void GetPage(int ts)
		{
			string str = string.Concat("http://", this.w.Url, "/archive");
			if (ts > 0)
			{
				str = string.Concat(str, "?before_time=", ts);
			}
			int num = this._currPage + 1;
			this._currPage = num;
			this.w.Viewstatus = string.Concat("Parsing page ", num);
			MyWebClient myWebClient = new MyWebClient();
			myWebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler((object sender, DownloadStringCompletedEventArgs e) => this.ParsePage(e.Result));
			myWebClient.DownloadStringAsync(new Uri(str));
		}

		private void GetPageCallback(PostItem obj)
		{
			this.ParsePage(obj.Post.HTML);
		}

		private void Parse(string html)
		{
			if (html == null)
			{
				return;
			}
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//div"))
			{
				if (!htmlNode.Attributes.Contains("class"))
				{
					continue;
				}
				string value = htmlNode.Attributes["class"].Value;
				if (!value.Contains("post_micro"))
				{
					continue;
				}
				string str = "null";
				if (!value.Contains("is_photo"))
				{
					if (!value.Contains("is_video"))
					{
						continue;
					}
					str = "video";
				}
				else
				{
					str = "photo";
				}
				string value1 = "";
				foreach (HtmlNode htmlNode1 in (IEnumerable<HtmlNode>)htmlNode.SelectNodes(".//a"))
				{
					if (!htmlNode1.Attributes.Contains("class") || htmlNode1.Attributes["class"].Value != "hover")
					{
						continue;
					}
					value1 = htmlNode1.Attributes["href"].Value;
					goto Label1;
				}
			Label1:
				long num = long.Parse(htmlNode.Attributes["id"].Value.Replace("post_micro_", ""));
				this.LastPost = Math.Max(num, this.LastPost);
				if (num <= this.PreviousLastPostID)
				{
					this._foundDuplicate = true;
					return;
				}
				else
				{
					Post post = new Post()
					{
						Website = this.w,
						Id = num.ToString(CultureInfo.InvariantCulture),
						Url = value1
					};
					post.SetNameFromURL();
					if (str == "video" && this.w.DoVideos)
					{
						if (!htmlNode.Attributes["class"].Value.Contains("is_direct_video"))
						{
							continue;
						}
						foreach (HtmlNode htmlNode2 in (IEnumerable<HtmlNode>)htmlNode.SelectNodes(".//div"))
						{
							if (!htmlNode2.Attributes["class"].Value.Contains("has_imageurl"))
							{
								continue;
							}
							string str1 = htmlNode2.Attributes["data-imageurl"].Value;
							if (str1.Length < 1)
							{
								continue;
							}
							string[] strArrays = str1.Split(new char[] { '/' });
							string str2 = strArrays[(int)strArrays.Length - 1].Split(new char[] { '\u005F' })[1];
							string str3 = string.Concat(new object[] { "http://www.tumblr.com/video_file/", num, "/tumblr_", str2 });
							post.Items.Add(new PostItemVideo(post, str3));
							this.AllPosts.Add(post);
						}
					}
					if (str != "photo")
					{
						continue;
					}
					foreach (HtmlNode htmlNode3 in (IEnumerable<HtmlNode>)htmlNode.SelectNodes(".//div"))
					{
						if (!htmlNode3.Attributes["class"].Value.Contains("has_imageurl"))
						{
							continue;
						}
						string value2 = htmlNode3.Attributes["data-imageurl"].Value;
						if (!this.w.DoPhotoSets)
						{
							string str4 = value2.Replace("src=", "").Replace("\"", "").Trim();
							str4 = str4.Replace("_250.", "_1280.");
							post.Items.Add(new PostItemPhoto(post, str4, value2));
						}
						this.AllPosts.Add(post);
					}
				}
			}
		}

		private void ParsePage(string s)
		{
			long lastPost;
			int nextTS = this.getNextTS(s);
			this.Parse(s);
			if (nextTS == 0)
			{
				lock (this.w)
				{
					this.w.ResetWebsite();
					Website str = this.w;
					lastPost = this.LastPost;
					str.LastPost = lastPost.ToString(CultureInfo.InvariantCulture);
					this.w.Viewstatus = "Finished indexing";
				}
			}
			if (this._foundDuplicate)
			{
				lock (this.w)
				{
					Website website = this.w;
					lastPost = this.LastPost;
					website.LastPost = lastPost.ToString(CultureInfo.InvariantCulture);
					this.w.Viewstatus = "Finished updating";
				}
			}
			if (nextTS > 0 && !this._foundDuplicate)
			{
				if (this.Running)
				{
					this.GetPage(nextTS);
					return;
				}
				this.w.Viewstatus = "Stopped";
				return;
			}
			this.w.Viewstatus = "Preparing ...";
			foreach (Post allPost in this.AllPosts)
			{
				base.AddToDownloadQueue(allPost);
			}
			this.w.Viewstatus = "";
			Website str1 = this.w;
			lastPost = this.LastPost;
			str1.LastPost = lastPost.ToString(CultureInfo.InvariantCulture);
			base.IndexFinished();
		}

		protected override void Start()
		{
			try
			{
				this.PreviousLastPostID = long.Parse(this.w.LastPost);
			}
			catch (Exception exception)
			{
			}
			try
			{
				if (this._getTitle(this.w.Url) != null)
				{
					this.GetPage(0);
				}
				else
				{
					this.w.Viewstatus = "Blog not found !";
					this.w.Viewchecked = false;
					base.IndexFinished();
				}
			}
			catch (Exception exception1)
			{
				this.w.Viewstatus = "Blog not found !";
				this.w.Viewchecked = false;
				base.IndexFinished();
			}
		}
	}
}