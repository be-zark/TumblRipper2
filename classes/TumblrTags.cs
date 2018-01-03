using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TumblRipper2.classes.Generic;
using TumblRipper2.classes.Tumblr;

namespace TumblRipper2.classes
{
	internal class TumblrTags : TumblrRip
	{
		private int currentCount;

		private bool foundDuplicate;

		public string LastPost
		{
			get;
			set;
		}

		public TumblrTags(Website w) : base(w)
		{
		}

		protected override string _getTitle(string url)
		{
			int num = url.IndexOf("/tagged/");
			num = num + 8;
			return string.Concat("Tag - ", url.Substring(num, url.Length - num).Replace("+", " "));
		}

		private int getCount(string html)
		{
			int num;
			try
			{
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(html);
				HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//ul");
				num = (htmlNode.Attributes["class"].Value.Contains("posts") ? htmlNode.SelectNodes(".//li").Count : 0);
			}
			catch (Exception exception)
			{
				num = 0;
			}
			return num;
		}

		protected void GetPage(int offset)
		{
			this.currentCount = offset;
			string str = string.Concat("http://", this.w.Url);
			MyWebClient myWebClient = new MyWebClient();
			if (offset > 0)
			{
				str = string.Concat(str, "?before=", offset);
			}
			this.w.Viewstatus = string.Concat("Loading page ", offset);
			Post post = new Post()
			{
				Url = str,
				Website = this.w
			};
			Downloader.Callback<PostItem> callback = new Downloader.Callback<PostItem>(this.getPageCallback);
			Downloader.GetInstance().DownloadPostHTML(post, callback);
		}

		private void getPageCallback(PostItem obj)
		{
			this.parsePage(obj.Post.HTML);
		}

		private void parse(string html)
		{
			if (html == null)
			{
				return;
			}
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//li"))
			{
				if (!htmlNode.Attributes.Contains("class") || !htmlNode.Attributes["class"].Value.Contains("post") || !htmlNode.Attributes.Contains("data-tumblelog-name") || !htmlNode.Attributes.Contains("data-id"))
				{
					continue;
				}
				this.LastPost = htmlNode.Attributes["data-id"].Value;
				if (this.LastPost == this.w.LastPost)
				{
					this.foundDuplicate = true;
					return;
				}
				else
				{
					string value = htmlNode.Attributes["data-type"].Value;
					Post post = new Post()
					{
						Website = this.w,
						Id = this.LastPost,
						TumblelogName = htmlNode.Attributes["data-tumblelog-name"].Value
					};
					foreach (HtmlNode htmlNode1 in (IEnumerable<HtmlNode>)htmlNode.SelectNodes(".//a"))
					{
						if (!htmlNode1.Attributes.Contains("class") || !(htmlNode1.Attributes["class"].Value == "click_glass"))
						{
							continue;
						}
						post.Url = htmlNode1.Attributes["href"].Value;
					}
					post.SetNameFromURL();
					if (value == "video" && this.w.DoVideos)
					{
						this.AllPosts.Add(post);
					}
					if (value != "photo")
					{
						continue;
					}
					foreach (HtmlNode htmlNode2 in (IEnumerable<HtmlNode>)htmlNode.SelectNodes(".//div[@class]"))
					{
						if (htmlNode2.Attributes.Contains("class") && htmlNode2.Attributes["class"].Value != "photo_stage_img")
						{
							continue;
						}
						string str = htmlNode2.Attributes["style"].Value;
						str = str.Replace("background-image:", "");
						str = str.Replace("'", "");
						str = str.Replace(")", "");
						str = str.Replace(";", "");
						str = str.Replace("(", "");
						str = str.Replace("url", "");
						str = str.Trim();
						if (!str.Contains("media.tumblr.com"))
						{
							continue;
						}
						string str1 = str;
						string str2 = str;
						string[] strArrays = str2.Replace("http://", "").Split(new char[] { '/' });
						str2 = str2.Replace("_500", "_").Replace("_400", "_").Replace("_250", "_");
						string[] strArrays1 = strArrays[(int)strArrays.Length - 1].Split(new char[] { '\u005F' });
						string str3 = "";
						str3 = string.Join("_", strArrays1, 0, (int)strArrays1.Length - 1);
						str3 = str3.Remove(str3.Length - 2);
						str1 = string.Concat(new string[] { "http://www.tumblr.com/photo/1280/", post.TumblelogName, "/", post.Id, "/1/", str3 });
						if (!this.w.DoPhotoSets)
						{
							post.Items.Add(new PostItemPhoto(post, str1, str));
						}
						this.AllPosts.Add(post);
					}
				}
			}
		}

		private void parsePage(string s)
		{
			int count = this.getCount(s);
			this.currentCount = this.currentCount + count;
			this.parse(s);
			if (count == 0)
			{
				lock (this.w)
				{
					this.w.LastPost = this.LastPost;
					this.w.Viewstatus = "Finished indexing";
				}
			}
			if (this.foundDuplicate)
			{
				lock (this.w)
				{
					this.w.LastPost = this.LastPost;
					this.w.Viewstatus = "Finished updating";
				}
			}
			if (count > 0 && !this.foundDuplicate)
			{
				if (this.Running)
				{
					this.GetPage(this.currentCount);
					return;
				}
				this.w.Viewstatus = "Stopped";
				return;
			}
			foreach (Post allPost in this.AllPosts)
			{
				base.AddToDownloadQueue(allPost);
			}
			base.IndexFinished();
		}

		protected override void Start()
		{
			this.GetPage(0);
		}
	}
}