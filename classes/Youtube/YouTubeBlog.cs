using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Youtube
{
	internal class YouTubeBlog : YouTubeRip
	{
		private int _currPage = 1;

		private string _channelID;

		private long _lastID;

		private List<Post> AllPostsParse = new List<Post>();

		public YouTubeBlog(Website w) : base(w)
		{
			try
			{
				this._lastID = long.Parse(w.LastPost);
			}
			catch (Exception exception)
			{
			}
		}

		protected override string _getTitle(string url)
		{
			string str;
			try
			{
				string str1 = (new MyWebClient()).DownloadString(string.Concat("http://", url) ?? "");
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(str1);
				foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//meta"))
				{
					if (!htmlNode.Attributes.Contains("property") || htmlNode.Attributes["property"].Value != "og:title")
					{
						continue;
					}
					str = htmlNode.Attributes["content"].Value.Trim();
					return str;
				}
				str = null;
			}
			catch (Exception exception)
			{
				str = null;
			}
			return str;
		}

		private void AddToDownloadQueue(Post post)
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

		private void Callback(PostItem postItem)
		{
			string hTML = postItem.Post.HTML;
			List<YouTubeVideoQuality> youTubeVideoUrls = YouTubeDownloader.GetYouTubeVideoUrls(hTML);
			if (youTubeVideoUrls.Count == 0)
			{
				Thread.Sleep(1000);
				youTubeVideoUrls = YouTubeDownloader.GetYouTubeVideoUrls(hTML);
			}
			if (youTubeVideoUrls.Count > 0)
			{
				int num = 0;
				num = 0;
				while (num < youTubeVideoUrls.Count && !youTubeVideoUrls[num].Extention.Equals("mp4"))
				{
					num++;
				}
				PostItemVideo postItemVideo = new PostItemVideo(postItem.Post, youTubeVideoUrls[num].DownloadUrl);
				Post post = postItem.Post;
				post.Name = string.Concat(post.Name, ".", youTubeVideoUrls[num].Extention);
				postItem.Post.Items.Add(postItemVideo);
				this.AddToDownloadQueue(postItem.Post);
			}
		}

		private void CallbackChannelID(PostItem postItem)
		{
			string hTML = postItem.Post.HTML;
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(hTML);
			foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>)htmlDocument.DocumentNode.SelectNodes("//meta"))
			{
				if (!htmlNode.Attributes.Contains("itemprop") || !htmlNode.Attributes["itemprop"].Value.Contains("channelId") || !htmlNode.Attributes.Contains("content"))
				{
					continue;
				}
				this._channelID = htmlNode.Attributes["content"].Value;
			}
			this.GetPage(1);
		}

		private void getChannel(string url)
		{
			this.w.Viewstatus = "Getting ChannelID ";
			Post post = new Post()
			{
				Url = url,
				Website = this.w
			};
			Downloader.Callback<PostItem> callback = new Downloader.Callback<PostItem>(this.CallbackChannelID);
			Downloader.GetInstance().DownloadPostHTML(post, callback);
		}

		protected void GetPage(int page)
		{
			if (this._channelID == null)
			{
				this.getChannel(string.Concat("http://", this.w.Url));
				return;
			}
			this._currPage = page;
			string str = string.Concat(new object[] { "http://www.youtube.com/c4_browse_ajax?action_load_more_videos=1&paging=", page, "&sort=dd&live_view=500&flow=grid&view=0&channel_id=", this._channelID, "&fluid=True" });
			this.w.Viewstatus = string.Concat("Parsing page ", this._currPage);
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

		private void ParsePage(string html)
		{
			// 
			// Current member / type: System.Void TumblRipper2.classes.Youtube.YouTubeBlog::ParsePage(System.String)
			// File path: V:\TumblRipper\TumblRipper.exe
			// 
			// Product version: 2016.1.112.1
			// Exception in: System.Void ParsePage(System.String)
			// 
			// Invalid argument: argumentInfo.
			//    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\ResolveDynamicVariablesStep.cs:line 329
			//    at ..( , FieldDefinition ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\ResolveDynamicVariablesStep.cs:line 134
			//    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\ResolveDynamicVariablesStep.cs:line 50
			//    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeVisitor.cs:line 90
			//    at ..Visit(IEnumerable ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeVisitor.cs:line 383
			//    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeVisitor.cs:line 388
			//    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeVisitor.cs:line 81
			//    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\ResolveDynamicVariablesStep.cs:line 30
			//    at ..(MethodBody ,  , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 88
			//    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 70
			//    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			//    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			//    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			// 
			// mailto: JustDecompilePublicFeedback@telerik.com

		}

		protected override void Start()
		{
			this.GetPage(1);
		}
	}
}