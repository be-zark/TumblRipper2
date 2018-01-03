using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TumblRipper2.classes.Youtube
{
	public class YouTubeDownloader
	{
		public YouTubeDownloader()
		{
		}

		private static List<string> ExtractUrls(string html)
		{
			List<string> strs = new List<string>();
			string str = "\"url_encoded_fmt_stream_map\":\\s+\"(.+?)&";
			html = Uri.UnescapeDataString(Regex.Match(html, str, RegexOptions.Singleline).Groups[1].ToString());
			string str1 = html.Substring(0, html.IndexOf('=') + 1);
			string[] strArrays = Regex.Split(html, str1);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				strArrays[i] = string.Concat(str1, strArrays[i]);
			}
			string[] strArrays1 = strArrays;
			for (int j = 0; j < (int)strArrays1.Length; j++)
			{
				string str2 = strArrays1[j];
				if (str2.Contains("url="))
				{
					string txtBtwn = YouTubeDownloader.GetTxtBtwn(str2, "url=", "\\u0026", 0);
					if (txtBtwn == "")
					{
						txtBtwn = YouTubeDownloader.GetTxtBtwn(str2, "url=", ",url", 0);
					}
					if (txtBtwn == "")
					{
						txtBtwn = YouTubeDownloader.GetTxtBtwn(str2, "url=", "\",", 0);
					}
					string txtBtwn1 = YouTubeDownloader.GetTxtBtwn(str2, "sig=", "\\u0026", 0);
					if (txtBtwn1 == "")
					{
						txtBtwn1 = YouTubeDownloader.GetTxtBtwn(str2, "sig=", ",sig", 0);
					}
					if (txtBtwn1 == "")
					{
						txtBtwn1 = YouTubeDownloader.GetTxtBtwn(str2, "sig=", "\",", 0);
					}
					while (true)
					{
						if (!txtBtwn.EndsWith(","))
						{
							if (!txtBtwn.EndsWith(".") && !txtBtwn.EndsWith("\""))
							{
								break;
							}
						}
						txtBtwn = txtBtwn.Remove(txtBtwn.Length - 1, 1);
					}
					while (true)
					{
						if (!txtBtwn1.EndsWith(","))
						{
							if (!txtBtwn1.EndsWith(".") && !txtBtwn1.EndsWith("\""))
							{
								break;
							}
						}
						txtBtwn1 = txtBtwn1.Remove(txtBtwn1.Length - 1, 1);
					}
					if (!string.IsNullOrEmpty(txtBtwn))
					{
						if (!string.IsNullOrEmpty(txtBtwn1))
						{
							txtBtwn = string.Concat(txtBtwn, "&signature=", txtBtwn1);
						}
						strs.Add(txtBtwn);
					}
				}
			}
			return strs;
		}

		public static string GetLastTxtBtwn(string input, string start, string end, int startIndex)
		{
			return YouTubeDownloader.GetTxtBtwn(input, start, end, startIndex, true);
		}

		private static bool getQuality(YouTubeVideoQuality q, bool _Wide)
		{
			int num;
			string str = Regex.Match(q.DownloadUrl, "itag=([1-9]?[0-9]?[0-9])", RegexOptions.Singleline).Groups[1].ToString();
			if (str == "")
			{
				return false;
			}
			if (!int.TryParse(str, out num))
			{
				num = 0;
			}
			if (num <= 18)
			{
				if (num <= 6)
				{
					if (num == 5)
					{
						q.SetQuality("flv", new Size(320, (_Wide ? 180 : 240)));
					}
					else
					{
						if (num != 6)
						{
							q.SetQuality(string.Concat("itag-", str), new Size(0, 0));
							return true;
						}
						q.SetQuality("flv", new Size(480, (_Wide ? 270 : 360)));
					}
				}
				else if (num == 17)
				{
					q.SetQuality("3gp", new Size(176, (_Wide ? 99 : 144)));
				}
				else
				{
					if (num != 18)
					{
						q.SetQuality(string.Concat("itag-", str), new Size(0, 0));
						return true;
					}
					q.SetQuality("mp4", new Size(640, (_Wide ? 360 : 480)));
				}
			}
			else if (num > 46)
			{
				switch (num)
				{
					case 82:
					{
						q.SetQuality("3D.mp4", new Size(480, (_Wide ? 270 : 360)));
						break;
					}
					case 83:
					{
						q.SetQuality("3D.mp4", new Size(640, (_Wide ? 360 : 480)));
						break;
					}
					case 84:
					{
						q.SetQuality("3D.mp4", new Size(1280, (_Wide ? 720 : 960)));
						break;
					}
					case 85:
					{
						q.SetQuality("3D.mp4", new Size(1920, (_Wide ? 1080 : 1440)));
						break;
					}
					default:
					{
						switch (num)
						{
							case 100:
							{
								q.SetQuality("3D.webm", new Size(640, (_Wide ? 360 : 480)));
								break;
							}
							case 101:
							{
								q.SetQuality("3D.webm", new Size(640, (_Wide ? 360 : 480)));
								break;
							}
							case 102:
							{
								q.SetQuality("3D.webm", new Size(1280, (_Wide ? 720 : 960)));
								break;
							}
							default:
							{
								if (num != 120)
								{
									q.SetQuality(string.Concat("itag-", str), new Size(0, 0));
									return true;
								}
								q.SetQuality("live.flv", new Size(1280, (_Wide ? 720 : 960)));
								break;
							}
						}
						break;
					}
				}
			}
			else if (num == 22)
			{
				q.SetQuality("mp4", new Size(1280, (_Wide ? 720 : 960)));
			}
			else
			{
				switch (num)
				{
					case 34:
					{
						q.SetQuality("flv", new Size(640, (_Wide ? 360 : 480)));
						break;
					}
					case 35:
					{
						q.SetQuality("flv", new Size(854, (_Wide ? 480 : 640)));
						break;
					}
					case 36:
					{
						q.SetQuality("3gp", new Size(320, (_Wide ? 180 : 240)));
						break;
					}
					case 37:
					{
						q.SetQuality("mp4", new Size(1920, (_Wide ? 1080 : 1440)));
						break;
					}
					case 38:
					{
						q.SetQuality("mp4", new Size(2048, (_Wide ? 1152 : 1536)));
						break;
					}
					case 39:
					case 40:
					case 41:
					case 42:
					{
						q.SetQuality(string.Concat("itag-", str), new Size(0, 0));
						return true;
					}
					case 43:
					{
						q.SetQuality("webm", new Size(640, (_Wide ? 360 : 480)));
						break;
					}
					case 44:
					{
						q.SetQuality("webm", new Size(854, (_Wide ? 480 : 640)));
						break;
					}
					case 45:
					{
						q.SetQuality("webm", new Size(1280, (_Wide ? 720 : 960)));
						break;
					}
					case 46:
					{
						q.SetQuality("webm", new Size(1920, (_Wide ? 1080 : 1440)));
						break;
					}
					default:
					{
						q.SetQuality(string.Concat("itag-", str), new Size(0, 0));
						return true;
					}
				}
			}
			return true;
		}

		private static string GetTitle(string RssDoc)
		{
			string txtBtwn = YouTubeDownloader.GetTxtBtwn(RssDoc, "'VIDEO_TITLE': '", "'", 0);
			if (txtBtwn == "")
			{
				txtBtwn = YouTubeDownloader.GetTxtBtwn(RssDoc, "\"title\" content=\"", "\"", 0);
			}
			if (txtBtwn == "")
			{
				txtBtwn = YouTubeDownloader.GetTxtBtwn(RssDoc, "&title=", "&", 0);
			}
			txtBtwn = txtBtwn.Replace("\\", "").Replace("'", "&#39;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("+", " ");
			return txtBtwn;
		}

		public static string GetTxtBtwn(string input, string start, string end, int startIndex)
		{
			return YouTubeDownloader.GetTxtBtwn(input, start, end, startIndex, false);
		}

		private static string GetTxtBtwn(string input, string start, string end, int startIndex, bool UseLastIndexOf)
		{
			int length = (UseLastIndexOf ? input.LastIndexOf(start, startIndex) : input.IndexOf(start, startIndex));
			if (length == -1)
			{
				return "";
			}
			length = length + start.Length;
			int num = input.IndexOf(end, length);
			if (num == -1)
			{
				return input.Substring(length);
			}
			return input.Substring(length, num - length);
		}

		public static List<YouTubeVideoQuality> GetYouTubeVideoUrls(string html)
		{
			List<YouTubeVideoQuality> youTubeVideoQualities = new List<YouTubeVideoQuality>();
			string title = YouTubeDownloader.GetTitle(html);
			foreach (string str in YouTubeDownloader.ExtractUrls(html))
			{
				YouTubeVideoQuality youTubeVideoQuality = new YouTubeVideoQuality()
				{
					VideoTitle = title,
					DownloadUrl = string.Concat(str, "&title=", title)
				};
				if (!YouTubeDownloader.getQuality(youTubeVideoQuality, YouTubeDownloader.IsWideScreen(html)))
				{
					continue;
				}
				youTubeVideoQualities.Add(youTubeVideoQuality);
			}
			return youTubeVideoQualities;
		}

		public static bool IsWideScreen(string html)
		{
			string str = Regex.Match(html, "'IS_WIDESCREEN':\\s+(.+?)\\s+", RegexOptions.Singleline).Groups[1].ToString().ToLower().Trim();
			if (str == "true")
			{
				return true;
			}
			return str == "true,";
		}

		public static string[] Split(string input, string pattren)
		{
			return Regex.Split(input, pattren);
		}
	}
}