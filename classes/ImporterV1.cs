using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using TumblRipper2;
using TumblRipper2.classes.Generic;

namespace TumblRipper2.classes
{
	internal class ImporterV1
	{
		public ImporterV1()
		{
		}

		public static bool HasPreviousConfig()
		{
			if (File.Exists(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TumblRipper"), "Settings.xml")))
			{
				return true;
			}
			return false;
		}

		public static Website ImportFromTumblr(string path)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			Website website = new Website();
			string str = path.Replace("tmblr", "bin");
			if (File.Exists(str))
			{
				Stream stream = File.Open(str, FileMode.Open);
				Dictionary<string, string> strs = (Dictionary<string, string>)(new BinaryFormatter()).Deserialize(stream);
				stream.Close();
				stream.Dispose();
				string array = strs.Values.ToArray<string>()[0];
				MatchCollection matchCollections = (new Regex("/[\\d]+/")).Matches(array);
				string str1 = matchCollections[matchCollections.Count - 1].ToString();
				str1 = str1.Replace("/", "");
				try
				{
					website.LastPost = str1;
					website.PhotoCount = strs.Values.Count;
					website.PostCount = strs.Values.Count;
				}
				catch (Exception exception)
				{
				}
			}
			string str2 = File.ReadAllText(path);
			str2 = Ripper.Cleanurl(str2);
			website.FirstAdded = File.GetCreationTime(path);
			website.LastUpdate = File.GetLastAccessTime(str);
			website.Name = Ripper.GetTitle(str2);
			website.TumblrType = Ripper.Type.Blog;
			website.Url = str2;
			website.Folder = Path.GetDirectoryName(path);
			website.Viewchecked = true;
			return website;
		}

		public static Settings ImportSettings()
		{
			string str = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TumblRipper"), "Settings.xml");
			ImporterLoading importerLoading = new ImporterLoading();
			Settings setting = new Settings();
			XmlDataDocument xmlDataDocument = new XmlDataDocument();
			int i = 0;
			xmlDataDocument.Load(new FileStream(str, FileMode.Open, FileAccess.Read));
			XmlNodeList elementsByTagName = xmlDataDocument.GetElementsByTagName("string");
			importerLoading.setTotal(elementsByTagName.Count);
			importerLoading.Show();
			for (i = 0; i < elementsByTagName.Count; i++)
			{
				importerLoading.setLoading(elementsByTagName[i].InnerText);
				Website website = ImporterV1.ImportFromTumblr(elementsByTagName[i].InnerText);
				if (website != null)
				{
					setting.Sites.Add(website);
				}
			}
			importerLoading.Close();
			importerLoading.Dispose();
			setting.SaveSettings();
			return setting;
		}
	}
}