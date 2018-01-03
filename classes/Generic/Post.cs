using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TumblRipper2.classes;

namespace TumblRipper2.classes.Generic
{
	public class Post
	{
		public string TumblelogName;

		public CookieCollection Cookies
		{
			get;
			set;
		}

		public string HTML
		{
			get;
			set;
		}

		public string Id
		{
			get;
			set;
		}

		[XmlIgnore]
		public List<PostItem> Items
		{
			get;
			set;
		}

		public List<string> Keywords
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public bool ScanDone
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public TumblRipper2.classes.Website w
		{
			get;
			set;
		}

		public TumblRipper2.classes.Website Website
		{
			get;
			set;
		}

		public Post()
		{
			this.Items = new List<PostItem>();
		}

		public void SetNameFromURL()
		{
			int num = this.Url.IndexOf("/post/") + 6;
			string str = this.Url.Substring(num, this.Url.Length - num);
			str = str.Replace("-", " ");
			this.Name = str.Replace("/", " - ");
		}
	}
}