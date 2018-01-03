using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TumblRipper2.classes.Generic;
using TumblRipper2.classes.Tumblr;

namespace TumblRipper2.classes
{
	internal class TumblrLikes : TumblrRip
	{
		private int currentPage = 1;

		private bool foundDuplicate;

		public string LastPost
		{
			get;
			set;
		}

		public TumblrLikes(Website w) : base(w)
		{
		}

		protected override string _getTitle(string url)
		{
			string str = "/liked/by/";
			int length = url.IndexOf(str);
			length = length + str.Length;
			return string.Concat("Liked by ", url.Substring(length, url.Length - length).Replace("+", " "));
		}

		protected void GetPage(int page)
		{
			if (page == 0)
			{
				page = 1;
			}
			Console.WriteLine(string.Concat("GetPage ", page));
			this.currentPage = page;
			string str = string.Concat("http://", this.w.Url);
			if (page > 1)
			{
				str = string.Concat(str, "/page/", page);
			}
			this.w.Viewstatus = string.Concat("Parsing page ", page);
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
			int num = 0;
			num = this.parse(obj.Post.HTML);
			if (num == 0)
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
			Console.WriteLine(string.Concat("Count : ", num));
			if (num > 0 && !this.foundDuplicate)
			{
				if (!this.Running)
				{
					this.w.Viewstatus = "Stopped";
					return;
				}
				this.GetPage(this.currentPage + 1);
				return;
			}
			foreach (Post allPost in this.AllPosts)
			{
				base.AddToDownloadQueue(allPost);
			}
			base.IndexFinished();
		}

		private int parse(string html)
		{
			int num;
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			int num1 = 0;
			using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//div")).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HtmlNode current = enumerator.Current;
					if (!current.Attributes.Contains("class") || !current.Attributes["class"].Value.Contains("post_full"))
					{
						continue;
					}
					string value = current.Attributes["data-type"].Value;
					this.LastPost = current.Attributes["data-post-id"].Value;
					num1++;
					if (this.LastPost == this.w.LastPost)
					{
						this.foundDuplicate = true;
						num = num1;
						return num;
					}
					else
					{
						Post post = new Post()
						{
							Website = this.w,
							Id = this.LastPost,
							Url = "",
							TumblelogName = current.Attributes["data-tumblelog-name"].Value
						};
						using (IEnumerator<HtmlNode> enumerator1 = ((IEnumerable<HtmlNode>)current.SelectNodes(".//a")).GetEnumerator())
						{
							while (enumerator1.MoveNext())
							{
								HtmlNode htmlNode = enumerator1.Current;
								if (htmlNode.Attributes["href"] == null || !htmlNode.Attributes["href"].Value.Contains("/post/"))
								{
									continue;
								}
								post.Url = htmlNode.Attributes["href"].Value;
							}
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
						HtmlNodeCollection htmlNodeCollections = current.SelectNodes(".//img[@src]");
						if (htmlNodeCollections != null)
						{
							using (var enumerator1 = ((IEnumerable<HtmlNode>)htmlNodeCollections).GetEnumerator())
							{
								while (enumerator1.MoveNext())
								{
									HtmlNode current1 = enumerator1.Current;
									string str = current1.Attributes["src"].Value;
									if (!str.Contains("media.tumblr.com") || current1.Attributes.Contains("class") && current1.Attributes["class"].Value == "post_avatar_image")
									{
										continue;
									}
									string str1 = str;
									string str2 = str1;
									string[] strArrays = str1.Replace("http://", "").Split(new char[] { '/' });
									str1 = str1.Replace("_500", "_").Replace("_400", "_").Replace("_250", "_");
									string[] strArrays1 = strArrays[(int)strArrays.Length - 1].Split(new char[] { '\u005F' });
									string str3 = "";
									str3 = string.Join("_", strArrays1, 0, (int)strArrays1.Length - 1);
									str3 = str3.Remove(str3.Length - 2);
									str2 = string.Concat(new string[] { "http://www.tumblr.com/photo/1280/", post.TumblelogName, "/", post.Id, "/1/", str3 });
									if (this.w.DoPhotoSets)
									{
										continue;
									}
									post.Items.Add(new PostItemPhoto(post, str2, current1.Attributes["src"].Value));
								}
							}
						}
						if (post.Items.Count <= 0)
						{
							continue;
						}
						this.AllPosts.Add(post);
					}
				}
				return num1;
			}
			return num;
		}

		private void parsePage(string s)
		{
		}

		protected override void Start()
		{
			this.GetPage(0);
		}
	}
}