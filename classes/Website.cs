using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes
{
	public class Website : IEquatable<Website>, INotifyPropertyChanged
	{
		public bool DoEXIF;

		public bool DoPhotoSets;

		public bool DoVideos;

		public bool RenameFiles = true;

		public bool SkipExisting = true;

		private string _lastLoadedImage;

		private DateTime _lastUpdate;

		private int _photoCount;

		private int _photoCountLoaded;

		private int _postCount;

		private Ripper.Type _tumblrType;

		private string _url;

		private int _videoCount;

		private bool _viewchecked;

		private int _viewnewItems;

		private string _viewstatus;

		[XmlIgnore]
		public CookieContainer Cookies
		{
			get;
			set;
		}

		public DateTime FirstAdded
		{
			get;
			set;
		}

		public string Folder
		{
			get;
			set;
		}

		public string LastLoadedImage
		{
			get
			{
				return this._lastLoadedImage;
			}
			set
			{
				if (value != this._lastLoadedImage)
				{
					this._lastLoadedImage = value;
					this.NotifyPropertyChanged("LastLoadedImage");
					this.NotifyPropertyChanged("LastLoadedImageImage");
				}
			}
		}

		public Image LastLoadedImageImage
		{
			get
			{
				Image bitmap = new Bitmap(16, 16, PixelFormat.Format24bppRgb);
				try
				{
					if (this._lastLoadedImage != null && File.Exists(this._lastLoadedImage) && !this._lastLoadedImage.EndsWith(".mp4"))
					{
						bitmap = Image.FromFile(this._lastLoadedImage);
					}
				}
				catch (Exception exception)
				{
				}
				return bitmap;
			}
		}

		public string LastPost
		{
			get;
			set;
		}

		public long LastPostID
		{
			get;
			set;
		}

		public DateTime LastUpdate
		{
			get
			{
				return this._lastUpdate;
			}
			set
			{
				if (value != this._lastUpdate)
				{
					this._lastUpdate = value;
					this.NotifyPropertyChanged("LastUpdate");
				}
			}
		}

		public string Name
		{
			get;
			set;
		}

		public int PhotoCount
		{
			get
			{
				return this._photoCount;
			}
			set
			{
				if (value != this._photoCount)
				{
					this._photoCount = value;
					this.NotifyPropertyChanged("PhotoCount");
				}
			}
		}

		public int PhotoCountLoaded
		{
			get
			{
				return this._photoCountLoaded;
			}
			set
			{
				if (value != this._photoCountLoaded)
				{
					this._photoCountLoaded = value;
					this.NotifyPropertyChanged("PhotoCountLoaded");
				}
			}
		}

		public int PostCount
		{
			get
			{
				return this._postCount;
			}
			set
			{
				if (value != this._postCount)
				{
					this._postCount = value;
					this.NotifyPropertyChanged("PostCount");
				}
			}
		}

		public Ripper.Type TumblrType
		{
			get
			{
				return this._tumblrType;
			}
			set
			{
				if (value != this._tumblrType)
				{
					this._tumblrType = value;
					this.NotifyPropertyChanged("TumblrType");
				}
			}
		}

		public string Url
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value.Trim();
			}
		}

		public int VideoCount
		{
			get
			{
				return this._videoCount;
			}
			set
			{
				if (value != this._videoCount)
				{
					this._videoCount = value;
					this.NotifyPropertyChanged("VideoCount");
				}
			}
		}

		public bool Viewchecked
		{
			get
			{
				return this._viewchecked;
			}
			set
			{
				if (value != this._viewchecked)
				{
					this._viewchecked = value;
					this.NotifyPropertyChanged("Viewchecked");
				}
			}
		}

		public int ViewnewItems
		{
			get
			{
				return this._viewnewItems;
			}
			set
			{
				if (value != this._viewnewItems)
				{
					this._viewnewItems = value;
					this.NotifyPropertyChanged("ViewnewItems");
				}
			}
		}

		public string Viewstatus
		{
			get
			{
				return this._viewstatus;
			}
			set
			{
				if (value != this._viewstatus)
				{
					this._viewstatus = value;
					this.NotifyPropertyChanged("Viewstatus");
				}
			}
		}

		public Website()
		{
		}

		public bool Equals(Website a)
		{
			if (a == null)
			{
				return false;
			}
			return this._url.Equals(a._url);
		}

		public override int GetHashCode()
		{
			return this._url.GetHashCode();
		}

		private void NotifyPropertyChanged(string info)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public static string RemoveInvalidFilePathCharacters(string filename, string replaceChar)
		{
			if (filename == null)
			{
				return null;
			}
			string str = string.Concat(new string(Path.GetInvalidFileNameChars()), new string(Path.GetInvalidPathChars()));
			return (new Regex(string.Format("[{0}]", Regex.Escape(str)))).Replace(filename, replaceChar);
		}

		public void ResetWebsite()
		{
			this.LastPost = "0";
			this._photoCount = 0;
			this._photoCountLoaded = 0;
			this._postCount = 0;
			this._videoCount = 0;
			this._viewnewItems = 0;
			this._viewstatus = "";
		}

		public override string ToString()
		{
			return this.Name.ToString();
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}