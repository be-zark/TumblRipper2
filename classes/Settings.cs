using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TumblRipper2.classes
{
	public class Settings
	{
		public static List<DisplayField> AllDisplayFields;

		public static Settings MySettings;

		private static string _directorySettings;

		private readonly static object LockObject;

		private int _rowHeight = 40;

		public string DefaultPath
		{
			get;
			set;
		}

		public static string DirectorySettings
		{
			get
			{
				return Settings._directorySettings;
			}
		}

		public BindingList<DisplayField> DisplayFields
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public List<string> HiddenPopups
		{
			get;
			set;
		}

		public string Licence
		{
			get;
			set;
		}

		public int MaxThreads
		{
			get;
			set;
		}

		public int RowHeight
		{
			get
			{
				return this._rowHeight;
			}
			set
			{
				this._rowHeight = Math.Max(value, 15);
			}
		}

		public string Serial
		{
			get;
			set;
		}

		public BindingList<Website> Sites
		{
			get;
			set;
		}

		static Settings()
		{
			Settings.AllDisplayFields = new List<DisplayField>();
			Settings.LockObject = new object();
		}

		public Settings()
		{
			Settings.AllDisplayFields.Clear();
			Settings.AllDisplayFields.Add(new DisplayField("Viewchecked", "Enabled", "Checkbox"));
			Settings.AllDisplayFields.Add(new DisplayField("LastLoadedImageImage", "LastImage", "Image"));
			Settings.AllDisplayFields.Add(new DisplayField("Name", "Name"));
			Settings.AllDisplayFields.Add(new DisplayField("Viewstatus", "Status"));
			Settings.AllDisplayFields.Add(new DisplayField("ViewnewItems", "Pending"));
			Settings.AllDisplayFields.Add(new DisplayField("LastUpdate", "Last Update"));
			Settings.AllDisplayFields.Add(new DisplayField("LastPost", "Last Post"));
			Settings.AllDisplayFields.Add(new DisplayField("PostCount", "Total Posts"));
			Settings.AllDisplayFields.Add(new DisplayField("PhotoCount", "Total Photos"));
			Settings.AllDisplayFields.Add(new DisplayField("VideoCount", "Total Videos"));
			Settings.AllDisplayFields.Add(new DisplayField("FirstAdded", "Date Added"));
			Settings.AllDisplayFields.Add(new DisplayField("Url", "Url"));
			Settings.AllDisplayFields.Add(new DisplayField("Folder", "Folder"));
			Settings.AllDisplayFields.Add(new DisplayField("TumblrType", "Type"));
			Settings._directorySettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TumblRipper2");
			this.LoadDefaults();
		}

		public void AddSite(Website w)
		{
			if (!this.Sites.Contains(w))
			{
				this.Sites.Add(w);
			}
		}

		public static string Decrypt(string cipherText)
		{
			byte[] bytes = Encoding.ASCII.GetBytes("teto1620@#$%asdf");
			byte[] numArray = Convert.FromBase64String(cipherText);
			byte[] bytes1 = Encoding.Unicode.GetBytes("_+)&qwer9512popo");
			ICryptoTransform cryptoTransform = (new RijndaelManaged()
			{
				Mode = CipherMode.CBC
			}).CreateDecryptor(bytes1, bytes);
			MemoryStream memoryStream = new MemoryStream(numArray);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
			byte[] numArray1 = new byte[(int)numArray.Length];
			int num = cryptoStream.Read(numArray1, 0, (int)numArray1.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(numArray1, 0, num);
		}

		public static string Encrypt(string plainText)
		{
			byte[] bytes = Encoding.ASCII.GetBytes("teto1620@#$%asdf");
			byte[] numArray = Encoding.UTF8.GetBytes(plainText);
			byte[] bytes1 = Encoding.Unicode.GetBytes("_+)&qwer9512popo");
			ICryptoTransform cryptoTransform = (new RijndaelManaged()
			{
				Mode = CipherMode.CBC
			}).CreateEncryptor(bytes1, bytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
			cryptoStream.Write(numArray, 0, (int)numArray.Length);
			cryptoStream.FlushFinalBlock();
			byte[] array = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(array);
		}

		public static Settings GetSettings()
		{
			Settings mySettings = Settings.MySettings;
			if (mySettings == null)
			{
				mySettings = Settings.LoadSettings();
				Settings.MySettings = mySettings;
			}
			return mySettings;
		}

		public void LoadDefaults()
		{
			this.Licence = "registered";
            this.Serial = "";
            this.Email = "";
            this.Sites = new BindingList<Website>();
			this.DefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			this.DisplayFields = new BindingList<DisplayField>();
			this.MaxThreads = 2;
			this.HiddenPopups = new List<string>();
			this._rowHeight = 40;
		}

		public static Settings LoadSettings()
		{
			Settings setting = new Settings();
			lock (Settings.LockObject)
			{
				string str = Path.Combine(Settings.DirectorySettings, "settings.xml");
				if (!File.Exists(str))
				{
					setting.LoadDefaults();
				}
				else
				{
					try
					{
						setting = Settings.LoadSettingsString(Settings.Decrypt(File.ReadAllText(str)));
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						MessageBox.Show(string.Concat("Unable to load settings :", exception.Message));
					}
				}
				if (setting.DisplayFields.Count == 0)
				{
					for (int i = 0; i < 6; i++)
					{
						setting.DisplayFields.Add(Settings.AllDisplayFields[i]);
					}
				}
			}
			return setting;
		}

		public static Settings LoadSettingsString(string str)
		{
			str = str.Trim();
			Settings setting = new Settings();
			setting = (Settings)(new XmlSerializer(setting.GetType())).Deserialize(new StringReader(str));
			foreach (Website site in setting.Sites)
			{
				site.Viewstatus = "";
			}
			return setting;
		}

		public string SaveSettings()
		{
			string str = "";
			lock (Settings.LockObject)
			{
				try
				{
					if (!Directory.Exists(Settings.DirectorySettings))
					{
						Directory.CreateDirectory(Settings.DirectorySettings);
					}
					if (!File.Exists(Path.Combine(Settings.DirectorySettings, "settings.xml")))
					{
						StreamWriter streamWriter = File.CreateText(Path.Combine(Settings.DirectorySettings, "settings.xml"));
						streamWriter.Write("");
						streamWriter.Close();
						streamWriter.Dispose();
					}
					string str1 = Path.Combine(Settings.DirectorySettings, "settings.xml");
					StringWriter stringWriter = new StringWriter();
					(new XmlSerializer(this.GetType())).Serialize(stringWriter, this);
					string str2 = stringWriter.ToString();
					str = str2;
					str2 = Settings.Encrypt(str2);
					File.WriteAllText(str1, str2);
				}
				catch (Exception exception)
				{
				}
			}
			return str;
		}
	}
}