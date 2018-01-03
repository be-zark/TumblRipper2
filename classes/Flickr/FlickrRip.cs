using System;
using System.Collections.Generic;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Flickr
{
	public abstract class FlickrRip : Ripper
	{
		protected List<Post> AllPosts = new List<Post>();

		protected FlickrRip(Website w) : base(w)
		{
		}
	}
}