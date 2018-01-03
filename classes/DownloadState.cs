using System;
using System.Net;
using System.Xml.Serialization;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes
{
	public class DownloadState
	{
		[XmlIgnore]
		public System.Uri Uri;

		public string Url;

		public Downloader.DownloadType type;

		public string Filename;

		[XmlIgnore]
		public WebClient Client;

		public PostItem postItem;

		[XmlIgnore]
		public Downloader.Callback<PostItem> callback;

		public DownloadState()
		{
		}

		public DownloadState(Downloader.DownloadType ty, PostItem p, string u, string f, WebClient c, Downloader.Callback<PostItem> call)
		{
			this.type = ty;
			this.Uri = new System.Uri(u);
			this.Url = u;
			this.Filename = f;
			this.Client = c;
			this.postItem = p;
			this.callback = call;
		}
	}
}