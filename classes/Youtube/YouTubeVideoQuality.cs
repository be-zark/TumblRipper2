using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TumblRipper2.classes.Youtube
{
	public class YouTubeVideoQuality
	{
		public Size Dimension
		{
			get;
			set;
		}

		public string DownloadUrl
		{
			get;
			set;
		}

		public string Extention
		{
			get;
			set;
		}

		public long Length
		{
			get;
			set;
		}

		public long VideoSize
		{
			get;
			set;
		}

		public string VideoTitle
		{
			get;
			set;
		}

		public string VideoUrl
		{
			get;
			set;
		}

		public YouTubeVideoQuality()
		{
		}

		public void SetQuality(string Extention, Size Dimension)
		{
			this.Extention = Extention;
			this.Dimension = Dimension;
		}

		public void SetSize(long size)
		{
			this.VideoSize = size;
		}

		public override string ToString()
		{
			object[] extention = new object[] { this.Extention, " File ", null, null, null };
			Size dimension = this.Dimension;
			extention[2] = dimension.Width;
			extention[3] = "x";
			dimension = this.Dimension;
			extention[4] = dimension.Height;
			return string.Concat(extention);
		}
	}
}