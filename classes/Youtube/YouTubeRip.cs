using System;
using System.Collections.Generic;
using TumblRipper2.classes;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes.Youtube
{
	public abstract class YouTubeRip : Ripper
	{
		protected List<Post> AllPosts = new List<Post>();

		protected YouTubeRip(Website w) : base(w)
		{
		}
	}
}