using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace TumblRipper2.classes.Generic
{
	[Serializable]
	[XmlInclude(typeof(PostItemHtml))]
	[XmlInclude(typeof(PostItemMusic))]
	[XmlInclude(typeof(PostItemPhoto))]
	[XmlInclude(typeof(PostItemText))]
	[XmlInclude(typeof(PostItemVideo))]
	public class PostItem
	{
		public int DownloadErrorCount;

		public TumblRipper2.classes.Generic.Post Post
		{
			get;
			set;
		}

		public string URL
		{
			get;
			set;
		}

		public PostItem()
		{
		}

		public PostItem(TumblRipper2.classes.Generic.Post post, string p)
		{
			this.Post = post;
			this.URL = p;
		}
	}
}