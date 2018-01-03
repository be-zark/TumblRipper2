using System;
using System.Collections.Generic;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Instagram
{
	public abstract class InstagramRip : Ripper
	{
		protected List<Post> AllPosts = new List<Post>();

		protected InstagramRip(Website w) : base(w)
		{
		}
	}
}