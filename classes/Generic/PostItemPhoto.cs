using System;
using System.Runtime.CompilerServices;

namespace TumblRipper2.classes.Generic
{
	public class PostItemPhoto : PostItem
	{
		public string Thumb
		{
			get;
			set;
		}

		public PostItemPhoto()
		{
		}

		public PostItemPhoto(TumblRipper2.classes.Generic.Post post, string p, string thumb) : base(post, p)
		{
			this.Thumb = thumb;
		}
	}
}